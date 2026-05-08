using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AFD RID: 2813
	internal ref struct ValueListBuilder<T>
	{
		// Token: 0x0600679A RID: 26522 RVA: 0x0015F3DF File Offset: 0x0015D5DF
		public ValueListBuilder(Span<T> initialSpan)
		{
			this._span = initialSpan;
			this._arrayFromPool = null;
			this._pos = 0;
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x0600679B RID: 26523 RVA: 0x0015F3F6 File Offset: 0x0015D5F6
		public int Length
		{
			get
			{
				return this._pos;
			}
		}

		// Token: 0x17001224 RID: 4644
		public ref T this[int index]
		{
			get
			{
				return this._span[index];
			}
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x0015F40C File Offset: 0x0015D60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(T item)
		{
			int pos = this._pos;
			if (pos >= this._span.Length)
			{
				this.Grow();
			}
			*this._span[pos] = item;
			this._pos = pos + 1;
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x0015F44F File Offset: 0x0015D64F
		public ReadOnlySpan<T> AsSpan()
		{
			return this._span.Slice(0, this._pos);
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x0015F468 File Offset: 0x0015D668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this._arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(this._arrayFromPool, false);
				this._arrayFromPool = null;
			}
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x0015F48C File Offset: 0x0015D68C
		private void Grow()
		{
			T[] array = ArrayPool<T>.Shared.Rent(this._span.Length * 2);
			this._span.TryCopyTo(array);
			T[] arrayFromPool = this._arrayFromPool;
			this._span = (this._arrayFromPool = array);
			if (arrayFromPool != null)
			{
				ArrayPool<T>.Shared.Return(arrayFromPool, false);
			}
		}

		// Token: 0x04003C1F RID: 15391
		private Span<T> _span;

		// Token: 0x04003C20 RID: 15392
		private T[] _arrayFromPool;

		// Token: 0x04003C21 RID: 15393
		private int _pos;
	}
}
