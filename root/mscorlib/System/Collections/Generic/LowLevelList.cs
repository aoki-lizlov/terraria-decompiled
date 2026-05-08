using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000B10 RID: 2832
	[DebuggerDisplay("Count = {Count}")]
	internal class LowLevelList<T>
	{
		// Token: 0x06006823 RID: 26659 RVA: 0x00161104 File Offset: 0x0015F304
		public LowLevelList()
		{
			this._items = LowLevelList<T>.s_emptyArray;
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00161117 File Offset: 0x0015F317
		public LowLevelList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity == 0)
			{
				this._items = LowLevelList<T>.s_emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x0016114C File Offset: 0x0015F34C
		public LowLevelList(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = LowLevelList<T>.s_emptyArray;
				foreach (T t in collection)
				{
					this.Add(t);
				}
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = LowLevelList<T>.s_emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06006826 RID: 26662 RVA: 0x001611FC File Offset: 0x0015F3FC
		// (set) Token: 0x06006827 RID: 26663 RVA: 0x00161208 File Offset: 0x0015F408
		public int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						Array.Copy(this._items, 0, array, 0, this._size);
						this._items = array;
						return;
					}
					this._items = LowLevelList<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06006828 RID: 26664 RVA: 0x00161266 File Offset: 0x0015F466
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700123F RID: 4671
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x001612B8 File Offset: 0x0015F4B8
		public void Add(T item)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			T[] items = this._items;
			int size = this._size;
			this._size = size + 1;
			items[size] = item;
			this._version++;
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x00161310 File Offset: 0x0015F510
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x0016134C File Offset: 0x0015F54C
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0016135B File Offset: 0x0015F55B
		public void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00161390 File Offset: 0x0015F590
		public bool Contains(T item)
		{
			if (item == null)
			{
				for (int i = 0; i < this._size; i++)
				{
					if (this._items[i] == null)
					{
						return true;
					}
				}
				return false;
			}
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x001613DA File Offset: 0x0015F5DA
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException();
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x001613FE File Offset: 0x0015F5FE
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x00161414 File Offset: 0x0015F614
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00161429 File Offset: 0x0015F629
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x00161454 File Offset: 0x0015F654
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = item;
			this._size++;
			this._version++;
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x001614E4 File Offset: 0x0015F6E4
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 != null)
			{
				int count = collection2.Count;
				if (count > 0)
				{
					this.EnsureCapacity(this._size + count);
					if (index < this._size)
					{
						Array.Copy(this._items, index, this._items, index + count, this._size - index);
					}
					if (this == collection2)
					{
						Array.Copy(this._items, 0, this._items, index, index);
						Array.Copy(this._items, index + count, this._items, index * 2, this._size - index);
					}
					else
					{
						T[] array = new T[count];
						collection2.CopyTo(array, 0);
						Array.Copy(array, 0, this._items, index, count);
					}
					this._size += count;
				}
			}
			else
			{
				foreach (T t in collection)
				{
					this.Insert(index++, t);
				}
			}
			this._version++;
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x00161618 File Offset: 0x0015F818
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x0016163C File Offset: 0x0015F83C
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			while (num < this._size && !match(this._items[num]))
			{
				num++;
			}
			if (num >= this._size)
			{
				return 0;
			}
			int i = num + 1;
			while (i < this._size)
			{
				while (i < this._size && match(this._items[i]))
				{
					i++;
				}
				if (i < this._size)
				{
					this._items[num++] = this._items[i++];
				}
			}
			Array.Clear(this._items, num, this._size - num);
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x00161714 File Offset: 0x0015F914
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = default(T);
			this._version++;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x00161794 File Offset: 0x0015F994
		public T[] ToArray()
		{
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x001617C2 File Offset: 0x0015F9C2
		// Note: this type is marked as 'beforefieldinit'.
		static LowLevelList()
		{
		}

		// Token: 0x04003C55 RID: 15445
		private const int _defaultCapacity = 4;

		// Token: 0x04003C56 RID: 15446
		protected T[] _items;

		// Token: 0x04003C57 RID: 15447
		protected int _size;

		// Token: 0x04003C58 RID: 15448
		protected int _version;

		// Token: 0x04003C59 RID: 15449
		private static readonly T[] s_emptyArray = new T[0];
	}
}
