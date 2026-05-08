using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
	// Token: 0x02000B01 RID: 2817
	internal struct LargeArrayBuilder<T>
	{
		// Token: 0x060067B6 RID: 26550 RVA: 0x0015F894 File Offset: 0x0015DA94
		public LargeArrayBuilder(bool initialize)
		{
			this = new LargeArrayBuilder<T>(int.MaxValue);
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x0015F8A4 File Offset: 0x0015DAA4
		public LargeArrayBuilder(int maxCapacity)
		{
			this = default(LargeArrayBuilder<T>);
			this._first = (this._current = Array.Empty<T>());
			this._maxCapacity = maxCapacity;
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060067B8 RID: 26552 RVA: 0x0015F8D3 File Offset: 0x0015DAD3
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x0015F8DC File Offset: 0x0015DADC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			int index = this._index;
			T[] current = this._current;
			if (index >= current.Length)
			{
				this.AddWithBufferAllocation(item);
			}
			else
			{
				current[index] = item;
				this._index = index + 1;
			}
			this._count++;
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x0015F928 File Offset: 0x0015DB28
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item)
		{
			this.AllocateBuffer();
			T[] current = this._current;
			int index = this._index;
			this._index = index + 1;
			current[index] = item;
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x0015F958 File Offset: 0x0015DB58
		public void AddRange(IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				T[] current = this._current;
				int num = this._index;
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					if (num >= current.Length)
					{
						this.AddWithBufferAllocation(t, ref current, ref num);
					}
					else
					{
						current[num] = t;
					}
					num++;
				}
				this._count += num - this._index;
				this._index = num;
			}
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x0015F9E4 File Offset: 0x0015DBE4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithBufferAllocation(T item, ref T[] destination, ref int index)
		{
			this._count += index - this._index;
			this._index = index;
			this.AllocateBuffer();
			destination = this._current;
			index = this._index;
			this._current[index] = item;
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x0015FA34 File Offset: 0x0015DC34
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			int num = 0;
			while (count > 0)
			{
				T[] buffer = this.GetBuffer(num);
				int num2 = Math.Min(count, buffer.Length);
				Array.Copy(buffer, 0, array, arrayIndex, num2);
				count -= num2;
				arrayIndex += num2;
				num++;
			}
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x0015FA74 File Offset: 0x0015DC74
		public CopyPosition CopyTo(CopyPosition position, T[] array, int arrayIndex, int count)
		{
			LargeArrayBuilder<T>.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.count = count;
			CS$<>8__locals1.array = array;
			CS$<>8__locals1.arrayIndex = arrayIndex;
			int num = position.Row;
			int column = position.Column;
			T[] array2 = this.GetBuffer(num);
			int num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(array2, column, ref CS$<>8__locals1);
			if (CS$<>8__locals1.count == 0)
			{
				return new CopyPosition(num, column + num2).Normalize(array2.Length);
			}
			do
			{
				array2 = this.GetBuffer(++num);
				num2 = LargeArrayBuilder<T>.<CopyTo>g__CopyToCore|17_0(array2, 0, ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.count > 0);
			return new CopyPosition(num, num2).Normalize(array2.Length);
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x0015FB10 File Offset: 0x0015DD10
		public T[] GetBuffer(int index)
		{
			if (index == 0)
			{
				return this._first;
			}
			if (index > this._buffers.Count)
			{
				return this._current;
			}
			return this._buffers[index - 1];
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x0015FB3F File Offset: 0x0015DD3F
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SlowAdd(T item)
		{
			this.Add(item);
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x0015FB48 File Offset: 0x0015DD48
		public T[] ToArray()
		{
			T[] array;
			if (this.TryMove(out array))
			{
				return array;
			}
			array = new T[this._count];
			this.CopyTo(array, 0, this._count);
			return array;
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x0015FB7C File Offset: 0x0015DD7C
		public bool TryMove(out T[] array)
		{
			array = this._first;
			return this._count == this._first.Length;
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x0015FB98 File Offset: 0x0015DD98
		private void AllocateBuffer()
		{
			if (this._count < 8)
			{
				int num = Math.Min((this._count == 0) ? 4 : (this._count * 2), this._maxCapacity);
				this._current = new T[num];
				Array.Copy(this._first, 0, this._current, 0, this._count);
				this._first = this._current;
				return;
			}
			int num2;
			if (this._count == 8)
			{
				num2 = 8;
			}
			else
			{
				this._buffers.Add(this._current);
				num2 = Math.Min(this._count, this._maxCapacity - this._count);
			}
			this._current = new T[num2];
			this._index = 0;
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x0015FC4C File Offset: 0x0015DE4C
		[CompilerGenerated]
		internal static int <CopyTo>g__CopyToCore|17_0(T[] sourceBuffer, int sourceIndex, ref LargeArrayBuilder<T>.<>c__DisplayClass17_0 A_2)
		{
			int num = Math.Min(sourceBuffer.Length - sourceIndex, A_2.count);
			Array.Copy(sourceBuffer, sourceIndex, A_2.array, A_2.arrayIndex, num);
			A_2.arrayIndex += num;
			A_2.count -= num;
			return num;
		}

		// Token: 0x04003C28 RID: 15400
		private const int StartingCapacity = 4;

		// Token: 0x04003C29 RID: 15401
		private const int ResizeLimit = 8;

		// Token: 0x04003C2A RID: 15402
		private readonly int _maxCapacity;

		// Token: 0x04003C2B RID: 15403
		private T[] _first;

		// Token: 0x04003C2C RID: 15404
		private ArrayBuilder<T[]> _buffers;

		// Token: 0x04003C2D RID: 15405
		private T[] _current;

		// Token: 0x04003C2E RID: 15406
		private int _index;

		// Token: 0x04003C2F RID: 15407
		private int _count;

		// Token: 0x02000B02 RID: 2818
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass17_0
		{
			// Token: 0x04003C30 RID: 15408
			public int count;

			// Token: 0x04003C31 RID: 15409
			public T[] array;

			// Token: 0x04003C32 RID: 15410
			public int arrayIndex;
		}
	}
}
