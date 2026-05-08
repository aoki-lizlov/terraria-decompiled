using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReLogic.Utilities
{
	// Token: 0x02000008 RID: 8
	public sealed class SlotVector<T> : IEnumerable<SlotVector<T>.ItemPair>, IEnumerable
	{
		// Token: 0x17000005 RID: 5
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this._array.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (!this._array[index].Id.IsActive)
				{
					throw new KeyNotFoundException();
				}
				return this._array[index].Value;
			}
			set
			{
				if (index < 0 || index >= this._array.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (!this._array[index].Id.IsActive)
				{
					throw new KeyNotFoundException();
				}
				this._array[index] = new SlotVector<T>.ItemPair(value, this._array[index].Id);
			}
		}

		// Token: 0x17000006 RID: 6
		public T this[SlotId id]
		{
			get
			{
				uint index = id.Index;
				if ((ulong)index >= (ulong)((long)this._array.Length))
				{
					throw new ArgumentOutOfRangeException();
				}
				if (!this._array[(int)index].Id.IsActive || id != this._array[(int)index].Id)
				{
					throw new KeyNotFoundException();
				}
				return this._array[(int)index].Value;
			}
			set
			{
				uint index = id.Index;
				if ((ulong)index >= (ulong)((long)this._array.Length))
				{
					throw new ArgumentOutOfRangeException();
				}
				if (!this._array[(int)index].Id.IsActive || id != this._array[(int)index].Id)
				{
					throw new KeyNotFoundException();
				}
				this._array[(int)index] = new SlotVector<T>.ItemPair(value, id);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000291D File Offset: 0x00000B1D
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002925 File Offset: 0x00000B25
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Count>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000292E File Offset: 0x00000B2E
		public int Capacity
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002938 File Offset: 0x00000B38
		public SlotVector(int capacity)
		{
			this._array = new SlotVector<T>.ItemPair[capacity];
			this.Clear();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002954 File Offset: 0x00000B54
		public SlotId Add(T value)
		{
			if (this._freeHead == 65535U)
			{
				return new SlotId(65535U);
			}
			uint freeHead = this._freeHead;
			SlotVector<T>.ItemPair itemPair = this._array[(int)freeHead];
			if (this._freeHead >= this._usedSpaceLength)
			{
				this._usedSpaceLength = this._freeHead + 1U;
			}
			this._freeHead = itemPair.Id.Index;
			this._array[(int)freeHead] = new SlotVector<T>.ItemPair(value, itemPair.Id.ToActive(freeHead));
			int count = this.Count;
			this.Count = count + 1;
			return this._array[(int)freeHead].Id;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A00 File Offset: 0x00000C00
		public void Clear()
		{
			this._usedSpaceLength = 0U;
			this.Count = 0;
			this._freeHead = 0U;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)(this._array.Length - 1)))
			{
				this._array[(int)num] = new SlotVector<T>.ItemPair(default(T), new SlotId(num + 1U));
				num += 1U;
			}
			this._array[this._array.Length - 1] = new SlotVector<T>.ItemPair(default(T), new SlotId(65535U));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A88 File Offset: 0x00000C88
		public bool Remove(SlotId id)
		{
			if (id.IsActive)
			{
				uint index = id.Index;
				this._array[(int)index] = new SlotVector<T>.ItemPair(default(T), id.ToInactive(this._freeHead));
				this._freeHead = index;
				int count = this.Count;
				this.Count = count - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public bool Has(SlotId id)
		{
			uint index = id.Index;
			return (ulong)index < (ulong)((long)this._array.Length) && this._array[(int)index].Id.IsActive && !(id != this._array[(int)index].Id);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B44 File Offset: 0x00000D44
		public bool Has(int index)
		{
			return index >= 0 && index < this._array.Length && this._array[index].Id.IsActive;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B80 File Offset: 0x00000D80
		public SlotVector<T>.ItemPair GetPair(int index)
		{
			if (this.Has(index))
			{
				return this._array[index];
			}
			return new SlotVector<T>.ItemPair(default(T), SlotId.Invalid);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BB6 File Offset: 0x00000DB6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SlotVector<T>.Enumerator(this);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BB6 File Offset: 0x00000DB6
		IEnumerator<SlotVector<T>.ItemPair> IEnumerable<SlotVector<T>.ItemPair>.GetEnumerator()
		{
			return new SlotVector<T>.Enumerator(this);
		}

		// Token: 0x0400000E RID: 14
		private const uint MAX_INDEX = 65535U;

		// Token: 0x0400000F RID: 15
		private readonly SlotVector<T>.ItemPair[] _array;

		// Token: 0x04000010 RID: 16
		private uint _freeHead;

		// Token: 0x04000011 RID: 17
		private uint _usedSpaceLength;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private int <Count>k__BackingField;

		// Token: 0x020000AE RID: 174
		public sealed class Enumerator : IEnumerator<SlotVector<T>.ItemPair>, IDisposable, IEnumerator
		{
			// Token: 0x06000409 RID: 1033 RVA: 0x0000DE56 File Offset: 0x0000C056
			public Enumerator(SlotVector<T> slotVector)
			{
				this._slotVector = slotVector;
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000DE6C File Offset: 0x0000C06C
			SlotVector<T>.ItemPair IEnumerator<SlotVector<T>.ItemPair>.Current
			{
				get
				{
					return this._slotVector.GetPair(this._index);
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000DE7F File Offset: 0x0000C07F
			object IEnumerator.Current
			{
				get
				{
					return this._slotVector.GetPair(this._index);
				}
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000DE97 File Offset: 0x0000C097
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
			public bool MoveNext()
			{
				do
				{
					int num = this._index + 1;
					this._index = num;
					if ((long)num >= (long)((ulong)this._slotVector._usedSpaceLength))
					{
						return false;
					}
				}
				while (!this._slotVector.Has(this._index));
				return true;
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000DEE5 File Offset: 0x0000C0E5
			public void Dispose()
			{
				this._slotVector = null;
			}

			// Token: 0x04000545 RID: 1349
			private SlotVector<T> _slotVector;

			// Token: 0x04000546 RID: 1350
			private int _index = -1;
		}

		// Token: 0x020000AF RID: 175
		public struct ItemPair
		{
			// Token: 0x0600040F RID: 1039 RVA: 0x0000DEEE File Offset: 0x0000C0EE
			public ItemPair(T value, SlotId id)
			{
				this.Value = value;
				this.Id = id;
			}

			// Token: 0x04000547 RID: 1351
			public readonly T Value;

			// Token: 0x04000548 RID: 1352
			public readonly SlotId Id;
		}
	}
}
