using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A93 RID: 2707
	[DebuggerTypeProxy(typeof(ArrayList.ArrayListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ArrayList : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600630A RID: 25354 RVA: 0x000025BE File Offset: 0x000007BE
		internal ArrayList(bool trash)
		{
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00151E2F File Offset: 0x0015002F
		public ArrayList()
		{
			this._items = Array.Empty<object>();
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00151E44 File Offset: 0x00150044
		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format("'{0}' must be non-negative.", "capacity"));
			}
			if (capacity == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[capacity];
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x00151E90 File Offset: 0x00150090
		public ArrayList(ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			int count = c.Count;
			if (count == 0)
			{
				this._items = Array.Empty<object>();
				return;
			}
			this._items = new object[count];
			this.AddRange(c);
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x00151EDF File Offset: 0x001500DF
		// (set) Token: 0x0600630F RID: 25359 RVA: 0x00151EEC File Offset: 0x001500EC
		public virtual int Capacity
		{
			get
			{
				return this._items.Length;
			}
			set
			{
				if (value < this._size)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = new object[4];
				}
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x00151F59 File Offset: 0x00150159
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06006311 RID: 25361 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x00151F61 File Offset: 0x00150161
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17001105 RID: 4357
		public virtual object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this._items[index];
			}
			set
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00151FE0 File Offset: 0x001501E0
		public static ArrayList Adapter(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.IListWrapper(list);
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x00151FF8 File Offset: 0x001501F8
		public virtual int Add(object value)
		{
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			this._items[this._size] = value;
			this._version++;
			int size = this._size;
			this._size = size + 1;
			return size;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x00152050 File Offset: 0x00150250
		public virtual void AddRange(ICollection c)
		{
			this.InsertRange(this._size, c);
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x00152060 File Offset: 0x00150260
		public virtual int BinarySearch(int index, int count, object value, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return Array.BinarySearch(this._items, index, count, value, comparer);
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x001520BB File Offset: 0x001502BB
		public virtual int BinarySearch(object value)
		{
			return this.BinarySearch(0, this.Count, value, null);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x001520CC File Offset: 0x001502CC
		public virtual int BinarySearch(object value, IComparer comparer)
		{
			return this.BinarySearch(0, this.Count, value, comparer);
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001520DD File Offset: 0x001502DD
		public virtual void Clear()
		{
			if (this._size > 0)
			{
				Array.Clear(this._items, 0, this._size);
				this._size = 0;
			}
			this._version++;
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00152110 File Offset: 0x00150310
		public virtual object Clone()
		{
			ArrayList arrayList = new ArrayList(this._size);
			arrayList._size = this._size;
			arrayList._version = this._version;
			Array.Copy(this._items, 0, arrayList._items, 0, this._size);
			return arrayList;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x0015215C File Offset: 0x0015035C
		public virtual bool Contains(object item)
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
			for (int j = 0; j < this._size; j++)
			{
				if (this._items[j] != null && this._items[j].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x001521B9 File Offset: 0x001503B9
		public virtual void CopyTo(Array array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x001521C3 File Offset: 0x001503C3
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x001521F8 File Offset: 0x001503F8
		public virtual void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x00152248 File Offset: 0x00150448
		private void EnsureCapacity(int min)
		{
			if (this._items.Length < min)
			{
				int num = ((this._items.Length == 0) ? 4 : (this._items.Length * 2));
				if (num > 2146435071)
				{
					num = 2146435071;
				}
				if (num < min)
				{
					num = min;
				}
				this.Capacity = num;
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00152292 File Offset: 0x00150492
		public static IList FixedSize(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeList(list);
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x001522A8 File Offset: 0x001504A8
		public static ArrayList FixedSize(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.FixedSizeArrayList(list);
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x001522BE File Offset: 0x001504BE
		public virtual IEnumerator GetEnumerator()
		{
			return new ArrayList.ArrayListEnumeratorSimple(this);
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x001522C8 File Offset: 0x001504C8
		public virtual IEnumerator GetEnumerator(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return new ArrayList.ArrayListEnumerator(this, index, count);
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x0015231B File Offset: 0x0015051B
		public virtual int IndexOf(object value)
		{
			return Array.IndexOf(this._items, value, 0, this._size);
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x00152330 File Offset: 0x00150530
		public virtual int IndexOf(object value, int startIndex)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return Array.IndexOf(this._items, value, startIndex, this._size - startIndex);
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00152360 File Offset: 0x00150560
		public virtual int IndexOf(object value, int startIndex, int count)
		{
			if (startIndex > this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > this._size - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return Array.IndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001523B4 File Offset: 0x001505B4
		public virtual void Insert(int index, object value)
		{
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Insertion index was out of range. Must be non-negative and less than or equal to size.");
			}
			if (this._size == this._items.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this._items, index, this._items, index + 1, this._size - index);
			}
			this._items[index] = value;
			this._size++;
			this._version++;
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x00152448 File Offset: 0x00150648
		public virtual void InsertRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int count = c.Count;
			if (count > 0)
			{
				this.EnsureCapacity(this._size + count);
				if (index < this._size)
				{
					Array.Copy(this._items, index, this._items, index + count, this._size - index);
				}
				object[] array = new object[count];
				c.CopyTo(array, 0);
				array.CopyTo(this._items, index);
				this._size += count;
				this._version++;
			}
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001524FC File Offset: 0x001506FC
		public virtual int LastIndexOf(object value)
		{
			return this.LastIndexOf(value, this._size - 1, this._size);
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x00152513 File Offset: 0x00150713
		public virtual int LastIndexOf(object value, int startIndex)
		{
			if (startIndex >= this._size)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0015253C File Offset: 0x0015073C
		public virtual int LastIndexOf(object value, int startIndex, int count)
		{
			if (this.Count != 0 && (startIndex < 0 || count < 0))
			{
				throw new ArgumentOutOfRangeException((startIndex < 0) ? "startIndex" : "count", "Non-negative number required.");
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (startIndex >= this._size || count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException((startIndex >= this._size) ? "startIndex" : "count", "Must be less than or equal to the size of the collection.");
			}
			return Array.LastIndexOf(this._items, value, startIndex, count);
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x001525BB File Offset: 0x001507BB
		public static IList ReadOnly(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyList(list);
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001525D1 File Offset: 0x001507D1
		public static ArrayList ReadOnly(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.ReadOnlyArrayList(list);
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001525E8 File Offset: 0x001507E8
		public virtual void Remove(object obj)
		{
			int num = this.IndexOf(obj);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x00152608 File Offset: 0x00150808
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			this._items[this._size] = null;
			this._version++;
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x00152684 File Offset: 0x00150884
		public virtual void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (count > 0)
			{
				int i = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				while (i > this._size)
				{
					this._items[--i] = null;
				}
				this._version++;
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x00152734 File Offset: 0x00150934
		public static ArrayList Repeat(object value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			ArrayList arrayList = new ArrayList((count > 4) ? count : 4);
			for (int i = 0; i < count; i++)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00152778 File Offset: 0x00150978
		public virtual void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00152788 File Offset: 0x00150988
		public virtual void Reverse(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Reverse<object>(this._items, index, count);
			this._version++;
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001527F0 File Offset: 0x001509F0
		public virtual void SetRange(int index, ICollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c", "Collection cannot be null.");
			}
			int count = c.Count;
			if (index < 0 || index > this._size - count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count > 0)
			{
				c.CopyTo(this._items, index);
				this._version++;
			}
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00152858 File Offset: 0x00150A58
		public virtual ArrayList GetRange(int index, int count)
		{
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return new ArrayList.Range(this, index, count);
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x001528A6 File Offset: 0x00150AA6
		public virtual void Sort()
		{
			this.Sort(0, this.Count, Comparer.Default);
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x001528BA File Offset: 0x00150ABA
		public virtual void Sort(IComparer comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x001528CC File Offset: 0x00150ACC
		public virtual void Sort(int index, int count, IComparer comparer)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (this._size - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			Array.Sort(this._items, index, count, comparer);
			this._version++;
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x00152933 File Offset: 0x00150B33
		public static IList Synchronized(IList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncIList(list);
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x00152949 File Offset: 0x00150B49
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x00152960 File Offset: 0x00150B60
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0015299C File Offset: 0x00150B9C
		public virtual Array ToArray(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, this._size);
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x001529DF File Offset: 0x00150BDF
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04003AFD RID: 15101
		private object[] _items;

		// Token: 0x04003AFE RID: 15102
		private int _size;

		// Token: 0x04003AFF RID: 15103
		private int _version;

		// Token: 0x04003B00 RID: 15104
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003B01 RID: 15105
		private const int _defaultCapacity = 4;

		// Token: 0x04003B02 RID: 15106
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x02000A94 RID: 2708
		[Serializable]
		private class IListWrapper : ArrayList
		{
			// Token: 0x06006342 RID: 25410 RVA: 0x001529ED File Offset: 0x00150BED
			internal IListWrapper(IList list)
			{
				this._list = list;
				this._version = 0;
			}

			// Token: 0x17001106 RID: 4358
			// (get) Token: 0x06006343 RID: 25411 RVA: 0x00152A03 File Offset: 0x00150C03
			// (set) Token: 0x06006344 RID: 25412 RVA: 0x00152A10 File Offset: 0x00150C10
			public override int Capacity
			{
				get
				{
					return this._list.Count;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
					}
				}
			}

			// Token: 0x17001107 RID: 4359
			// (get) Token: 0x06006345 RID: 25413 RVA: 0x00152A03 File Offset: 0x00150C03
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001108 RID: 4360
			// (get) Token: 0x06006346 RID: 25414 RVA: 0x00152A2B File Offset: 0x00150C2B
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001109 RID: 4361
			// (get) Token: 0x06006347 RID: 25415 RVA: 0x00152A38 File Offset: 0x00150C38
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x1700110A RID: 4362
			// (get) Token: 0x06006348 RID: 25416 RVA: 0x00152A45 File Offset: 0x00150C45
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700110B RID: 4363
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version++;
				}
			}

			// Token: 0x1700110C RID: 4364
			// (get) Token: 0x0600634B RID: 25419 RVA: 0x00152A7D File Offset: 0x00150C7D
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x0600634C RID: 25420 RVA: 0x00152A8A File Offset: 0x00150C8A
			public override int Add(object obj)
			{
				int num = this._list.Add(obj);
				this._version++;
				return num;
			}

			// Token: 0x0600634D RID: 25421 RVA: 0x00152AA6 File Offset: 0x00150CA6
			public override void AddRange(ICollection c)
			{
				this.InsertRange(this.Count, c);
			}

			// Token: 0x0600634E RID: 25422 RVA: 0x00152AB8 File Offset: 0x00150CB8
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				int i = index;
				int num = index + count - 1;
				while (i <= num)
				{
					int num2 = (i + num) / 2;
					int num3 = comparer.Compare(value, this._list[num2]);
					if (num3 == 0)
					{
						return num2;
					}
					if (num3 < 0)
					{
						num = num2 - 1;
					}
					else
					{
						i = num2 + 1;
					}
				}
				return ~i;
			}

			// Token: 0x0600634F RID: 25423 RVA: 0x00152B47 File Offset: 0x00150D47
			public override void Clear()
			{
				if (this._list.IsFixedSize)
				{
					throw new NotSupportedException("Collection was of a fixed size.");
				}
				this._list.Clear();
				this._version++;
			}

			// Token: 0x06006350 RID: 25424 RVA: 0x00152B7A File Offset: 0x00150D7A
			public override object Clone()
			{
				return new ArrayList.IListWrapper(this._list);
			}

			// Token: 0x06006351 RID: 25425 RVA: 0x00152B87 File Offset: 0x00150D87
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006352 RID: 25426 RVA: 0x00152B95 File Offset: 0x00150D95
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006353 RID: 25427 RVA: 0x00152BA4 File Offset: 0x00150DA4
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "arrayIndex", "Non-negative number required.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				for (int i = index; i < index + count; i++)
				{
					array.SetValue(this._list[i], arrayIndex++);
				}
			}

			// Token: 0x06006354 RID: 25428 RVA: 0x00152C6A File Offset: 0x00150E6A
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006355 RID: 25429 RVA: 0x00152C78 File Offset: 0x00150E78
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.IListWrapper.IListWrapperEnumWrapper(this, index, count);
			}

			// Token: 0x06006356 RID: 25430 RVA: 0x00152CCB File Offset: 0x00150ECB
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006357 RID: 25431 RVA: 0x00152CD9 File Offset: 0x00150ED9
			public override int IndexOf(object value, int startIndex)
			{
				return this.IndexOf(value, startIndex, this._list.Count - startIndex);
			}

			// Token: 0x06006358 RID: 25432 RVA: 0x00152CF0 File Offset: 0x00150EF0
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > this.Count - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				int num = startIndex + count;
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j < num; j++)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x06006359 RID: 25433 RVA: 0x00152D8F File Offset: 0x00150F8F
			public override void Insert(int index, object obj)
			{
				this._list.Insert(index, obj);
				this._version++;
			}

			// Token: 0x0600635A RID: 25434 RVA: 0x00152DAC File Offset: 0x00150FAC
			public override void InsertRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", "Collection cannot be null.");
				}
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c.Count > 0)
				{
					ArrayList arrayList = this._list as ArrayList;
					if (arrayList != null)
					{
						arrayList.InsertRange(index, c);
					}
					else
					{
						foreach (object obj in c)
						{
							this._list.Insert(index++, obj);
						}
					}
					this._version++;
				}
			}

			// Token: 0x0600635B RID: 25435 RVA: 0x00152E41 File Offset: 0x00151041
			public override int LastIndexOf(object value)
			{
				return this.LastIndexOf(value, this._list.Count - 1, this._list.Count);
			}

			// Token: 0x0600635C RID: 25436 RVA: 0x00152E62 File Offset: 0x00151062
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x0600635D RID: 25437 RVA: 0x00152E70 File Offset: 0x00151070
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				if (this._list.Count == 0)
				{
					return -1;
				}
				if (startIndex < 0 || startIndex >= this._list.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || count > startIndex + 1)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				int num = startIndex - count + 1;
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (this._list[i] == null)
						{
							return i;
						}
					}
					return -1;
				}
				for (int j = startIndex; j >= num; j--)
				{
					if (this._list[j] != null && this._list[j].Equals(value))
					{
						return j;
					}
				}
				return -1;
			}

			// Token: 0x0600635E RID: 25438 RVA: 0x00152F20 File Offset: 0x00151120
			public override void Remove(object value)
			{
				int num = this.IndexOf(value);
				if (num >= 0)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x0600635F RID: 25439 RVA: 0x00152F40 File Offset: 0x00151140
			public override void RemoveAt(int index)
			{
				this._list.RemoveAt(index);
				this._version++;
			}

			// Token: 0x06006360 RID: 25440 RVA: 0x00152F5C File Offset: 0x0015115C
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (count > 0)
				{
					this._version++;
				}
				while (count > 0)
				{
					this._list.RemoveAt(index);
					count--;
				}
			}

			// Token: 0x06006361 RID: 25441 RVA: 0x00152FD0 File Offset: 0x001511D0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				int i = index;
				int num = index + count - 1;
				while (i < num)
				{
					object obj = this._list[i];
					this._list[i++] = this._list[num];
					this._list[num--] = obj;
				}
				this._version++;
			}

			// Token: 0x06006362 RID: 25442 RVA: 0x00153074 File Offset: 0x00151274
			public override void SetRange(int index, ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c", "Collection cannot be null.");
				}
				if (index < 0 || index > this._list.Count - c.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c.Count > 0)
				{
					foreach (object obj in c)
					{
						this._list[index++] = obj;
					}
					this._version++;
				}
			}

			// Token: 0x06006363 RID: 25443 RVA: 0x001530FC File Offset: 0x001512FC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006364 RID: 25444 RVA: 0x00153150 File Offset: 0x00151350
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._list.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				object[] array = new object[count];
				this.CopyTo(index, array, 0, count);
				Array.Sort(array, 0, count, comparer);
				for (int i = 0; i < count; i++)
				{
					this._list[i + index] = array[i];
				}
				this._version++;
			}

			// Token: 0x06006365 RID: 25445 RVA: 0x001531E0 File Offset: 0x001513E0
			public override object[] ToArray()
			{
				if (this.Count == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this.Count];
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006366 RID: 25446 RVA: 0x00153218 File Offset: 0x00151418
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				Array array = Array.CreateInstance(type, this._list.Count);
				this._list.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006367 RID: 25447 RVA: 0x00004088 File Offset: 0x00002288
			public override void TrimToSize()
			{
			}

			// Token: 0x04003B03 RID: 15107
			private IList _list;

			// Token: 0x02000A95 RID: 2709
			[Serializable]
			private sealed class IListWrapperEnumWrapper : IEnumerator, ICloneable
			{
				// Token: 0x06006368 RID: 25448 RVA: 0x0015325C File Offset: 0x0015145C
				internal IListWrapperEnumWrapper(ArrayList.IListWrapper listWrapper, int startIndex, int count)
				{
					this._en = listWrapper.GetEnumerator();
					this._initialStartIndex = startIndex;
					this._initialCount = count;
					while (startIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = count;
					this._firstCall = true;
				}

				// Token: 0x06006369 RID: 25449 RVA: 0x000025BE File Offset: 0x000007BE
				private IListWrapperEnumWrapper()
				{
				}

				// Token: 0x0600636A RID: 25450 RVA: 0x001532B0 File Offset: 0x001514B0
				public object Clone()
				{
					return new ArrayList.IListWrapper.IListWrapperEnumWrapper
					{
						_en = (IEnumerator)((ICloneable)this._en).Clone(),
						_initialStartIndex = this._initialStartIndex,
						_initialCount = this._initialCount,
						_remaining = this._remaining,
						_firstCall = this._firstCall
					};
				}

				// Token: 0x0600636B RID: 25451 RVA: 0x00153310 File Offset: 0x00151510
				public bool MoveNext()
				{
					if (this._firstCall)
					{
						this._firstCall = false;
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0 && this._en.MoveNext();
					}
					if (this._remaining < 0)
					{
						return false;
					}
					if (this._en.MoveNext())
					{
						int num = this._remaining;
						this._remaining = num - 1;
						return num > 0;
					}
					return false;
				}

				// Token: 0x1700110D RID: 4365
				// (get) Token: 0x0600636C RID: 25452 RVA: 0x0015337C File Offset: 0x0015157C
				public object Current
				{
					get
					{
						if (this._firstCall)
						{
							throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
						}
						if (this._remaining < 0)
						{
							throw new InvalidOperationException("Enumeration already finished.");
						}
						return this._en.Current;
					}
				}

				// Token: 0x0600636D RID: 25453 RVA: 0x001533B0 File Offset: 0x001515B0
				public void Reset()
				{
					this._en.Reset();
					int initialStartIndex = this._initialStartIndex;
					while (initialStartIndex-- > 0 && this._en.MoveNext())
					{
					}
					this._remaining = this._initialCount;
					this._firstCall = true;
				}

				// Token: 0x04003B04 RID: 15108
				private IEnumerator _en;

				// Token: 0x04003B05 RID: 15109
				private int _remaining;

				// Token: 0x04003B06 RID: 15110
				private int _initialStartIndex;

				// Token: 0x04003B07 RID: 15111
				private int _initialCount;

				// Token: 0x04003B08 RID: 15112
				private bool _firstCall;
			}
		}

		// Token: 0x02000A96 RID: 2710
		[Serializable]
		private class SyncArrayList : ArrayList
		{
			// Token: 0x0600636E RID: 25454 RVA: 0x001533F7 File Offset: 0x001515F7
			internal SyncArrayList(ArrayList list)
				: base(false)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x1700110E RID: 4366
			// (get) Token: 0x0600636F RID: 25455 RVA: 0x00153414 File Offset: 0x00151614
			// (set) Token: 0x06006370 RID: 25456 RVA: 0x0015345C File Offset: 0x0015165C
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list.Capacity = value;
					}
				}
			}

			// Token: 0x1700110F RID: 4367
			// (get) Token: 0x06006371 RID: 25457 RVA: 0x001534A4 File Offset: 0x001516A4
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17001110 RID: 4368
			// (get) Token: 0x06006372 RID: 25458 RVA: 0x001534EC File Offset: 0x001516EC
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001111 RID: 4369
			// (get) Token: 0x06006373 RID: 25459 RVA: 0x001534F9 File Offset: 0x001516F9
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001112 RID: 4370
			// (get) Token: 0x06006374 RID: 25460 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001113 RID: 4371
			public override object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x06006377 RID: 25463 RVA: 0x00153598 File Offset: 0x00151798
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x06006378 RID: 25464 RVA: 0x001535A0 File Offset: 0x001517A0
			public override int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x06006379 RID: 25465 RVA: 0x001535E8 File Offset: 0x001517E8
			public override void AddRange(ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.AddRange(c);
				}
			}

			// Token: 0x0600637A RID: 25466 RVA: 0x00153630 File Offset: 0x00151830
			public override int BinarySearch(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value);
				}
				return num;
			}

			// Token: 0x0600637B RID: 25467 RVA: 0x00153678 File Offset: 0x00151878
			public override int BinarySearch(object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(value, comparer);
				}
				return num;
			}

			// Token: 0x0600637C RID: 25468 RVA: 0x001536C4 File Offset: 0x001518C4
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.BinarySearch(index, count, value, comparer);
				}
				return num;
			}

			// Token: 0x0600637D RID: 25469 RVA: 0x00153710 File Offset: 0x00151910
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x0600637E RID: 25470 RVA: 0x00153758 File Offset: 0x00151958
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
				}
				return obj;
			}

			// Token: 0x0600637F RID: 25471 RVA: 0x001537AC File Offset: 0x001519AC
			public override bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06006380 RID: 25472 RVA: 0x001537F4 File Offset: 0x001519F4
			public override void CopyTo(Array array)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array);
				}
			}

			// Token: 0x06006381 RID: 25473 RVA: 0x0015383C File Offset: 0x00151A3C
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006382 RID: 25474 RVA: 0x00153884 File Offset: 0x00151A84
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(index, array, arrayIndex, count);
				}
			}

			// Token: 0x06006383 RID: 25475 RVA: 0x001538D0 File Offset: 0x00151AD0
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006384 RID: 25476 RVA: 0x00153918 File Offset: 0x00151B18
			public override IEnumerator GetEnumerator(int index, int count)
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator(index, count);
				}
				return enumerator;
			}

			// Token: 0x06006385 RID: 25477 RVA: 0x00153964 File Offset: 0x00151B64
			public override int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x06006386 RID: 25478 RVA: 0x001539AC File Offset: 0x00151BAC
			public override int IndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x06006387 RID: 25479 RVA: 0x001539F8 File Offset: 0x00151BF8
			public override int IndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x06006388 RID: 25480 RVA: 0x00153A44 File Offset: 0x00151C44
			public override void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x06006389 RID: 25481 RVA: 0x00153A8C File Offset: 0x00151C8C
			public override void InsertRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.InsertRange(index, c);
				}
			}

			// Token: 0x0600638A RID: 25482 RVA: 0x00153AD4 File Offset: 0x00151CD4
			public override int LastIndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value);
				}
				return num;
			}

			// Token: 0x0600638B RID: 25483 RVA: 0x00153B1C File Offset: 0x00151D1C
			public override int LastIndexOf(object value, int startIndex)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex);
				}
				return num;
			}

			// Token: 0x0600638C RID: 25484 RVA: 0x00153B68 File Offset: 0x00151D68
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.LastIndexOf(value, startIndex, count);
				}
				return num;
			}

			// Token: 0x0600638D RID: 25485 RVA: 0x00153BB4 File Offset: 0x00151DB4
			public override void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x0600638E RID: 25486 RVA: 0x00153BFC File Offset: 0x00151DFC
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x0600638F RID: 25487 RVA: 0x00153C44 File Offset: 0x00151E44
			public override void RemoveRange(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveRange(index, count);
				}
			}

			// Token: 0x06006390 RID: 25488 RVA: 0x00153C8C File Offset: 0x00151E8C
			public override void Reverse(int index, int count)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Reverse(index, count);
				}
			}

			// Token: 0x06006391 RID: 25489 RVA: 0x00153CD4 File Offset: 0x00151ED4
			public override void SetRange(int index, ICollection c)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetRange(index, c);
				}
			}

			// Token: 0x06006392 RID: 25490 RVA: 0x00153D1C File Offset: 0x00151F1C
			public override ArrayList GetRange(int index, int count)
			{
				object root = this._root;
				ArrayList range;
				lock (root)
				{
					range = this._list.GetRange(index, count);
				}
				return range;
			}

			// Token: 0x06006393 RID: 25491 RVA: 0x00153D68 File Offset: 0x00151F68
			public override void Sort()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort();
				}
			}

			// Token: 0x06006394 RID: 25492 RVA: 0x00153DB0 File Offset: 0x00151FB0
			public override void Sort(IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(comparer);
				}
			}

			// Token: 0x06006395 RID: 25493 RVA: 0x00153DF8 File Offset: 0x00151FF8
			public override void Sort(int index, int count, IComparer comparer)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Sort(index, count, comparer);
				}
			}

			// Token: 0x06006396 RID: 25494 RVA: 0x00153E40 File Offset: 0x00152040
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._list.ToArray();
				}
				return array;
			}

			// Token: 0x06006397 RID: 25495 RVA: 0x00153E88 File Offset: 0x00152088
			public override Array ToArray(Type type)
			{
				object root = this._root;
				Array array;
				lock (root)
				{
					array = this._list.ToArray(type);
				}
				return array;
			}

			// Token: 0x06006398 RID: 25496 RVA: 0x00153ED0 File Offset: 0x001520D0
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04003B09 RID: 15113
			private ArrayList _list;

			// Token: 0x04003B0A RID: 15114
			private object _root;
		}

		// Token: 0x02000A97 RID: 2711
		[Serializable]
		private class SyncIList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006399 RID: 25497 RVA: 0x00153F18 File Offset: 0x00152118
			internal SyncIList(IList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x17001115 RID: 4373
			// (get) Token: 0x0600639A RID: 25498 RVA: 0x00153F34 File Offset: 0x00152134
			public virtual int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x17001116 RID: 4374
			// (get) Token: 0x0600639B RID: 25499 RVA: 0x00153F7C File Offset: 0x0015217C
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001117 RID: 4375
			// (get) Token: 0x0600639C RID: 25500 RVA: 0x00153F89 File Offset: 0x00152189
			public virtual bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x17001118 RID: 4376
			// (get) Token: 0x0600639D RID: 25501 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001119 RID: 4377
			public virtual object this[int index]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[index];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[index] = value;
					}
				}
			}

			// Token: 0x1700111A RID: 4378
			// (get) Token: 0x060063A0 RID: 25504 RVA: 0x00154028 File Offset: 0x00152228
			public virtual object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x060063A1 RID: 25505 RVA: 0x00154030 File Offset: 0x00152230
			public virtual int Add(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.Add(value);
				}
				return num;
			}

			// Token: 0x060063A2 RID: 25506 RVA: 0x00154078 File Offset: 0x00152278
			public virtual void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x060063A3 RID: 25507 RVA: 0x001540C0 File Offset: 0x001522C0
			public virtual bool Contains(object item)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(item);
				}
				return flag2;
			}

			// Token: 0x060063A4 RID: 25508 RVA: 0x00154108 File Offset: 0x00152308
			public virtual void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x060063A5 RID: 25509 RVA: 0x00154150 File Offset: 0x00152350
			public virtual IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x060063A6 RID: 25510 RVA: 0x00154198 File Offset: 0x00152398
			public virtual int IndexOf(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOf(value);
				}
				return num;
			}

			// Token: 0x060063A7 RID: 25511 RVA: 0x001541E0 File Offset: 0x001523E0
			public virtual void Insert(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Insert(index, value);
				}
			}

			// Token: 0x060063A8 RID: 25512 RVA: 0x00154228 File Offset: 0x00152428
			public virtual void Remove(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(value);
				}
			}

			// Token: 0x060063A9 RID: 25513 RVA: 0x00154270 File Offset: 0x00152470
			public virtual void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x04003B0B RID: 15115
			private IList _list;

			// Token: 0x04003B0C RID: 15116
			private object _root;
		}

		// Token: 0x02000A98 RID: 2712
		[Serializable]
		private class FixedSizeList : IList, ICollection, IEnumerable
		{
			// Token: 0x060063AA RID: 25514 RVA: 0x001542B8 File Offset: 0x001524B8
			internal FixedSizeList(IList l)
			{
				this._list = l;
			}

			// Token: 0x1700111B RID: 4379
			// (get) Token: 0x060063AB RID: 25515 RVA: 0x001542C7 File Offset: 0x001524C7
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700111C RID: 4380
			// (get) Token: 0x060063AC RID: 25516 RVA: 0x001542D4 File Offset: 0x001524D4
			public virtual bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x1700111D RID: 4381
			// (get) Token: 0x060063AD RID: 25517 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700111E RID: 4382
			// (get) Token: 0x060063AE RID: 25518 RVA: 0x001542E1 File Offset: 0x001524E1
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700111F RID: 4383
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
				}
			}

			// Token: 0x17001120 RID: 4384
			// (get) Token: 0x060063B1 RID: 25521 RVA: 0x0015430B File Offset: 0x0015250B
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x060063B2 RID: 25522 RVA: 0x0004F808 File Offset: 0x0004DA08
			public virtual int Add(object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063B3 RID: 25523 RVA: 0x0004F808 File Offset: 0x0004DA08
			public virtual void Clear()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063B4 RID: 25524 RVA: 0x00154318 File Offset: 0x00152518
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x060063B5 RID: 25525 RVA: 0x00154326 File Offset: 0x00152526
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x060063B6 RID: 25526 RVA: 0x00154335 File Offset: 0x00152535
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x060063B7 RID: 25527 RVA: 0x00154342 File Offset: 0x00152542
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x060063B8 RID: 25528 RVA: 0x0004F808 File Offset: 0x0004DA08
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063B9 RID: 25529 RVA: 0x0004F808 File Offset: 0x0004DA08
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063BA RID: 25530 RVA: 0x0004F808 File Offset: 0x0004DA08
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x04003B0D RID: 15117
			private IList _list;
		}

		// Token: 0x02000A99 RID: 2713
		[Serializable]
		private class FixedSizeArrayList : ArrayList
		{
			// Token: 0x060063BB RID: 25531 RVA: 0x00154350 File Offset: 0x00152550
			internal FixedSizeArrayList(ArrayList l)
			{
				this._list = l;
				this._version = this._list._version;
			}

			// Token: 0x17001121 RID: 4385
			// (get) Token: 0x060063BC RID: 25532 RVA: 0x00154370 File Offset: 0x00152570
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001122 RID: 4386
			// (get) Token: 0x060063BD RID: 25533 RVA: 0x0015437D File Offset: 0x0015257D
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x17001123 RID: 4387
			// (get) Token: 0x060063BE RID: 25534 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001124 RID: 4388
			// (get) Token: 0x060063BF RID: 25535 RVA: 0x0015438A File Offset: 0x0015258A
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001125 RID: 4389
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					this._list[index] = value;
					this._version = this._list._version;
				}
			}

			// Token: 0x17001126 RID: 4390
			// (get) Token: 0x060063C2 RID: 25538 RVA: 0x001543C5 File Offset: 0x001525C5
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x060063C3 RID: 25539 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override int Add(object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063C4 RID: 25540 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063C5 RID: 25541 RVA: 0x001543D2 File Offset: 0x001525D2
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17001127 RID: 4391
			// (get) Token: 0x060063C6 RID: 25542 RVA: 0x001543E4 File Offset: 0x001525E4
			// (set) Token: 0x060063C7 RID: 25543 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException("Collection was of a fixed size.");
				}
			}

			// Token: 0x060063C8 RID: 25544 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void Clear()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063C9 RID: 25545 RVA: 0x001543F1 File Offset: 0x001525F1
			public override object Clone()
			{
				return new ArrayList.FixedSizeArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x060063CA RID: 25546 RVA: 0x00154414 File Offset: 0x00152614
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x060063CB RID: 25547 RVA: 0x00154422 File Offset: 0x00152622
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x060063CC RID: 25548 RVA: 0x00154431 File Offset: 0x00152631
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x060063CD RID: 25549 RVA: 0x00154443 File Offset: 0x00152643
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x060063CE RID: 25550 RVA: 0x00154450 File Offset: 0x00152650
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x060063CF RID: 25551 RVA: 0x0015445F File Offset: 0x0015265F
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x060063D0 RID: 25552 RVA: 0x0015446D File Offset: 0x0015266D
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x060063D1 RID: 25553 RVA: 0x0015447C File Offset: 0x0015267C
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x060063D2 RID: 25554 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063D3 RID: 25555 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063D4 RID: 25556 RVA: 0x0015448C File Offset: 0x0015268C
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x060063D5 RID: 25557 RVA: 0x0015449A File Offset: 0x0015269A
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x060063D6 RID: 25558 RVA: 0x001544A9 File Offset: 0x001526A9
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x060063D7 RID: 25559 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void Remove(object value)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063D8 RID: 25560 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063D9 RID: 25561 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x060063DA RID: 25562 RVA: 0x001544B9 File Offset: 0x001526B9
			public override void SetRange(int index, ICollection c)
			{
				this._list.SetRange(index, c);
				this._version = this._list._version;
			}

			// Token: 0x060063DB RID: 25563 RVA: 0x001544DC File Offset: 0x001526DC
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x060063DC RID: 25564 RVA: 0x0015452A File Offset: 0x0015272A
			public override void Reverse(int index, int count)
			{
				this._list.Reverse(index, count);
				this._version = this._list._version;
			}

			// Token: 0x060063DD RID: 25565 RVA: 0x0015454A File Offset: 0x0015274A
			public override void Sort(int index, int count, IComparer comparer)
			{
				this._list.Sort(index, count, comparer);
				this._version = this._list._version;
			}

			// Token: 0x060063DE RID: 25566 RVA: 0x0015456B File Offset: 0x0015276B
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x060063DF RID: 25567 RVA: 0x00154578 File Offset: 0x00152778
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x060063E0 RID: 25568 RVA: 0x0004F808 File Offset: 0x0004DA08
			public override void TrimToSize()
			{
				throw new NotSupportedException("Collection was of a fixed size.");
			}

			// Token: 0x04003B0E RID: 15118
			private ArrayList _list;
		}

		// Token: 0x02000A9A RID: 2714
		[Serializable]
		private class ReadOnlyList : IList, ICollection, IEnumerable
		{
			// Token: 0x060063E1 RID: 25569 RVA: 0x00154586 File Offset: 0x00152786
			internal ReadOnlyList(IList l)
			{
				this._list = l;
			}

			// Token: 0x17001128 RID: 4392
			// (get) Token: 0x060063E2 RID: 25570 RVA: 0x00154595 File Offset: 0x00152795
			public virtual int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x17001129 RID: 4393
			// (get) Token: 0x060063E3 RID: 25571 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700112A RID: 4394
			// (get) Token: 0x060063E4 RID: 25572 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700112B RID: 4395
			// (get) Token: 0x060063E5 RID: 25573 RVA: 0x001545A2 File Offset: 0x001527A2
			public virtual bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x1700112C RID: 4396
			public virtual object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x1700112D RID: 4397
			// (get) Token: 0x060063E8 RID: 25576 RVA: 0x001545C9 File Offset: 0x001527C9
			public virtual object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x060063E9 RID: 25577 RVA: 0x001545BD File Offset: 0x001527BD
			public virtual int Add(object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063EA RID: 25578 RVA: 0x001545BD File Offset: 0x001527BD
			public virtual void Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063EB RID: 25579 RVA: 0x001545D6 File Offset: 0x001527D6
			public virtual bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x060063EC RID: 25580 RVA: 0x001545E4 File Offset: 0x001527E4
			public virtual void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x060063ED RID: 25581 RVA: 0x001545F3 File Offset: 0x001527F3
			public virtual IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x060063EE RID: 25582 RVA: 0x00154600 File Offset: 0x00152800
			public virtual int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x060063EF RID: 25583 RVA: 0x001545BD File Offset: 0x001527BD
			public virtual void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063F0 RID: 25584 RVA: 0x001545BD File Offset: 0x001527BD
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063F1 RID: 25585 RVA: 0x001545BD File Offset: 0x001527BD
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x04003B0F RID: 15119
			private IList _list;
		}

		// Token: 0x02000A9B RID: 2715
		[Serializable]
		private class ReadOnlyArrayList : ArrayList
		{
			// Token: 0x060063F2 RID: 25586 RVA: 0x0015460E File Offset: 0x0015280E
			internal ReadOnlyArrayList(ArrayList l)
			{
				this._list = l;
			}

			// Token: 0x1700112E RID: 4398
			// (get) Token: 0x060063F3 RID: 25587 RVA: 0x0015461D File Offset: 0x0015281D
			public override int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x1700112F RID: 4399
			// (get) Token: 0x060063F4 RID: 25588 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001130 RID: 4400
			// (get) Token: 0x060063F5 RID: 25589 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001131 RID: 4401
			// (get) Token: 0x060063F6 RID: 25590 RVA: 0x0015462A File Offset: 0x0015282A
			public override bool IsSynchronized
			{
				get
				{
					return this._list.IsSynchronized;
				}
			}

			// Token: 0x17001132 RID: 4402
			public override object this[int index]
			{
				get
				{
					return this._list[index];
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x17001133 RID: 4403
			// (get) Token: 0x060063F9 RID: 25593 RVA: 0x00154645 File Offset: 0x00152845
			public override object SyncRoot
			{
				get
				{
					return this._list.SyncRoot;
				}
			}

			// Token: 0x060063FA RID: 25594 RVA: 0x001545BD File Offset: 0x001527BD
			public override int Add(object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063FB RID: 25595 RVA: 0x001545BD File Offset: 0x001527BD
			public override void AddRange(ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060063FC RID: 25596 RVA: 0x00154652 File Offset: 0x00152852
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				return this._list.BinarySearch(index, count, value, comparer);
			}

			// Token: 0x17001134 RID: 4404
			// (get) Token: 0x060063FD RID: 25597 RVA: 0x00154664 File Offset: 0x00152864
			// (set) Token: 0x060063FE RID: 25598 RVA: 0x001545BD File Offset: 0x001527BD
			public override int Capacity
			{
				get
				{
					return this._list.Capacity;
				}
				set
				{
					throw new NotSupportedException("Collection is read-only.");
				}
			}

			// Token: 0x060063FF RID: 25599 RVA: 0x001545BD File Offset: 0x001527BD
			public override void Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006400 RID: 25600 RVA: 0x00154671 File Offset: 0x00152871
			public override object Clone()
			{
				return new ArrayList.ReadOnlyArrayList(this._list)
				{
					_list = (ArrayList)this._list.Clone()
				};
			}

			// Token: 0x06006401 RID: 25601 RVA: 0x00154694 File Offset: 0x00152894
			public override bool Contains(object obj)
			{
				return this._list.Contains(obj);
			}

			// Token: 0x06006402 RID: 25602 RVA: 0x001546A2 File Offset: 0x001528A2
			public override void CopyTo(Array array, int index)
			{
				this._list.CopyTo(array, index);
			}

			// Token: 0x06006403 RID: 25603 RVA: 0x001546B1 File Offset: 0x001528B1
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				this._list.CopyTo(index, array, arrayIndex, count);
			}

			// Token: 0x06006404 RID: 25604 RVA: 0x001546C3 File Offset: 0x001528C3
			public override IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x06006405 RID: 25605 RVA: 0x001546D0 File Offset: 0x001528D0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				return this._list.GetEnumerator(index, count);
			}

			// Token: 0x06006406 RID: 25606 RVA: 0x001546DF File Offset: 0x001528DF
			public override int IndexOf(object value)
			{
				return this._list.IndexOf(value);
			}

			// Token: 0x06006407 RID: 25607 RVA: 0x001546ED File Offset: 0x001528ED
			public override int IndexOf(object value, int startIndex)
			{
				return this._list.IndexOf(value, startIndex);
			}

			// Token: 0x06006408 RID: 25608 RVA: 0x001546FC File Offset: 0x001528FC
			public override int IndexOf(object value, int startIndex, int count)
			{
				return this._list.IndexOf(value, startIndex, count);
			}

			// Token: 0x06006409 RID: 25609 RVA: 0x001545BD File Offset: 0x001527BD
			public override void Insert(int index, object obj)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600640A RID: 25610 RVA: 0x001545BD File Offset: 0x001527BD
			public override void InsertRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600640B RID: 25611 RVA: 0x0015470C File Offset: 0x0015290C
			public override int LastIndexOf(object value)
			{
				return this._list.LastIndexOf(value);
			}

			// Token: 0x0600640C RID: 25612 RVA: 0x0015471A File Offset: 0x0015291A
			public override int LastIndexOf(object value, int startIndex)
			{
				return this._list.LastIndexOf(value, startIndex);
			}

			// Token: 0x0600640D RID: 25613 RVA: 0x00154729 File Offset: 0x00152929
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				return this._list.LastIndexOf(value, startIndex, count);
			}

			// Token: 0x0600640E RID: 25614 RVA: 0x001545BD File Offset: 0x001527BD
			public override void Remove(object value)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600640F RID: 25615 RVA: 0x001545BD File Offset: 0x001527BD
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006410 RID: 25616 RVA: 0x001545BD File Offset: 0x001527BD
			public override void RemoveRange(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006411 RID: 25617 RVA: 0x001545BD File Offset: 0x001527BD
			public override void SetRange(int index, ICollection c)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006412 RID: 25618 RVA: 0x0015473C File Offset: 0x0015293C
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this.Count - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x06006413 RID: 25619 RVA: 0x001545BD File Offset: 0x001527BD
			public override void Reverse(int index, int count)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006414 RID: 25620 RVA: 0x001545BD File Offset: 0x001527BD
			public override void Sort(int index, int count, IComparer comparer)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006415 RID: 25621 RVA: 0x0015478A File Offset: 0x0015298A
			public override object[] ToArray()
			{
				return this._list.ToArray();
			}

			// Token: 0x06006416 RID: 25622 RVA: 0x00154797 File Offset: 0x00152997
			public override Array ToArray(Type type)
			{
				return this._list.ToArray(type);
			}

			// Token: 0x06006417 RID: 25623 RVA: 0x001545BD File Offset: 0x001527BD
			public override void TrimToSize()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x04003B10 RID: 15120
			private ArrayList _list;
		}

		// Token: 0x02000A9C RID: 2716
		[Serializable]
		private sealed class ArrayListEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006418 RID: 25624 RVA: 0x001547A5 File Offset: 0x001529A5
			internal ArrayListEnumerator(ArrayList list, int index, int count)
			{
				this._list = list;
				this._startIndex = index;
				this._index = index - 1;
				this._endIndex = this._index + count;
				this._version = list._version;
				this._currentElement = null;
			}

			// Token: 0x06006419 RID: 25625 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x0600641A RID: 25626 RVA: 0x001547E8 File Offset: 0x001529E8
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._endIndex)
				{
					ArrayList list = this._list;
					int num = this._index + 1;
					this._index = num;
					this._currentElement = list[num];
					return true;
				}
				this._index = this._endIndex + 1;
				return false;
			}

			// Token: 0x17001135 RID: 4405
			// (get) Token: 0x0600641B RID: 25627 RVA: 0x00154854 File Offset: 0x00152A54
			public object Current
			{
				get
				{
					if (this._index < this._startIndex)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index > this._endIndex)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this._currentElement;
				}
			}

			// Token: 0x0600641C RID: 25628 RVA: 0x0015488E File Offset: 0x00152A8E
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = this._startIndex - 1;
			}

			// Token: 0x04003B11 RID: 15121
			private ArrayList _list;

			// Token: 0x04003B12 RID: 15122
			private int _index;

			// Token: 0x04003B13 RID: 15123
			private int _endIndex;

			// Token: 0x04003B14 RID: 15124
			private int _version;

			// Token: 0x04003B15 RID: 15125
			private object _currentElement;

			// Token: 0x04003B16 RID: 15126
			private int _startIndex;
		}

		// Token: 0x02000A9D RID: 2717
		[Serializable]
		private class Range : ArrayList
		{
			// Token: 0x0600641D RID: 25629 RVA: 0x001548BC File Offset: 0x00152ABC
			internal Range(ArrayList list, int index, int count)
				: base(false)
			{
				this._baseList = list;
				this._baseIndex = index;
				this._baseSize = count;
				this._baseVersion = list._version;
				this._version = list._version;
			}

			// Token: 0x0600641E RID: 25630 RVA: 0x001548F2 File Offset: 0x00152AF2
			private void InternalUpdateRange()
			{
				if (this._baseVersion != this._baseList._version)
				{
					throw new InvalidOperationException("This range in the underlying list is invalid. A possible cause is that elements were removed.");
				}
			}

			// Token: 0x0600641F RID: 25631 RVA: 0x00154912 File Offset: 0x00152B12
			private void InternalUpdateVersion()
			{
				this._baseVersion++;
				this._version++;
			}

			// Token: 0x06006420 RID: 25632 RVA: 0x00154930 File Offset: 0x00152B30
			public override int Add(object value)
			{
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + this._baseSize, value);
				this.InternalUpdateVersion();
				int baseSize = this._baseSize;
				this._baseSize = baseSize + 1;
				return baseSize;
			}

			// Token: 0x06006421 RID: 25633 RVA: 0x00154974 File Offset: 0x00152B74
			public override void AddRange(ICollection c)
			{
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + this._baseSize, c);
					this.InternalUpdateVersion();
					this._baseSize += count;
				}
			}

			// Token: 0x06006422 RID: 25634 RVA: 0x001549D0 File Offset: 0x00152BD0
			public override int BinarySearch(int index, int count, object value, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.BinarySearch(this._baseIndex + index, count, value, comparer);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return num + this._baseIndex;
			}

			// Token: 0x17001136 RID: 4406
			// (get) Token: 0x06006423 RID: 25635 RVA: 0x00154A49 File Offset: 0x00152C49
			// (set) Token: 0x06006424 RID: 25636 RVA: 0x00152A10 File Offset: 0x00150C10
			public override int Capacity
			{
				get
				{
					return this._baseList.Capacity;
				}
				set
				{
					if (value < this.Count)
					{
						throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
					}
				}
			}

			// Token: 0x06006425 RID: 25637 RVA: 0x00154A56 File Offset: 0x00152C56
			public override void Clear()
			{
				this.InternalUpdateRange();
				if (this._baseSize != 0)
				{
					this._baseList.RemoveRange(this._baseIndex, this._baseSize);
					this.InternalUpdateVersion();
					this._baseSize = 0;
				}
			}

			// Token: 0x06006426 RID: 25638 RVA: 0x00154A8A File Offset: 0x00152C8A
			public override object Clone()
			{
				this.InternalUpdateRange();
				return new ArrayList.Range(this._baseList, this._baseIndex, this._baseSize)
				{
					_baseList = (ArrayList)this._baseList.Clone()
				};
			}

			// Token: 0x06006427 RID: 25639 RVA: 0x00154AC0 File Offset: 0x00152CC0
			public override bool Contains(object item)
			{
				this.InternalUpdateRange();
				if (item == null)
				{
					for (int i = 0; i < this._baseSize; i++)
					{
						if (this._baseList[this._baseIndex + i] == null)
						{
							return true;
						}
					}
					return false;
				}
				for (int j = 0; j < this._baseSize; j++)
				{
					if (this._baseList[this._baseIndex + j] != null && this._baseList[this._baseIndex + j].Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006428 RID: 25640 RVA: 0x00154B44 File Offset: 0x00152D44
			public override void CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
				}
				if (array.Length - index < this._baseSize)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex, array, index, this._baseSize);
			}

			// Token: 0x06006429 RID: 25641 RVA: 0x00154BC8 File Offset: 0x00152DC8
			public override void CopyTo(int index, Array array, int arrayIndex, int count)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.CopyTo(this._baseIndex + index, array, arrayIndex, count);
			}

			// Token: 0x17001137 RID: 4407
			// (get) Token: 0x0600642A RID: 25642 RVA: 0x00154C6B File Offset: 0x00152E6B
			public override int Count
			{
				get
				{
					this.InternalUpdateRange();
					return this._baseSize;
				}
			}

			// Token: 0x17001138 RID: 4408
			// (get) Token: 0x0600642B RID: 25643 RVA: 0x00154C79 File Offset: 0x00152E79
			public override bool IsReadOnly
			{
				get
				{
					return this._baseList.IsReadOnly;
				}
			}

			// Token: 0x17001139 RID: 4409
			// (get) Token: 0x0600642C RID: 25644 RVA: 0x00154C86 File Offset: 0x00152E86
			public override bool IsFixedSize
			{
				get
				{
					return this._baseList.IsFixedSize;
				}
			}

			// Token: 0x1700113A RID: 4410
			// (get) Token: 0x0600642D RID: 25645 RVA: 0x00154C93 File Offset: 0x00152E93
			public override bool IsSynchronized
			{
				get
				{
					return this._baseList.IsSynchronized;
				}
			}

			// Token: 0x0600642E RID: 25646 RVA: 0x00154CA0 File Offset: 0x00152EA0
			public override IEnumerator GetEnumerator()
			{
				return this.GetEnumerator(0, this._baseSize);
			}

			// Token: 0x0600642F RID: 25647 RVA: 0x00154CB0 File Offset: 0x00152EB0
			public override IEnumerator GetEnumerator(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				return this._baseList.GetEnumerator(this._baseIndex + index, count);
			}

			// Token: 0x06006430 RID: 25648 RVA: 0x00154D10 File Offset: 0x00152F10
			public override ArrayList GetRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				return new ArrayList.Range(this, index, count);
			}

			// Token: 0x1700113B RID: 4411
			// (get) Token: 0x06006431 RID: 25649 RVA: 0x00154D64 File Offset: 0x00152F64
			public override object SyncRoot
			{
				get
				{
					return this._baseList.SyncRoot;
				}
			}

			// Token: 0x06006432 RID: 25650 RVA: 0x00154D74 File Offset: 0x00152F74
			public override int IndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006433 RID: 25651 RVA: 0x00154DB0 File Offset: 0x00152FB0
			public override int IndexOf(object value, int startIndex)
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
				}
				if (startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, this._baseSize - startIndex);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006434 RID: 25652 RVA: 0x00154E1C File Offset: 0x0015301C
			public override int IndexOf(object value, int startIndex, int count)
			{
				if (startIndex < 0 || startIndex > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > this._baseSize - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				this.InternalUpdateRange();
				int num = this._baseList.IndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006435 RID: 25653 RVA: 0x00154E90 File Offset: 0x00153090
			public override void Insert(int index, object value)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Insert(this._baseIndex + index, value);
				this.InternalUpdateVersion();
				this._baseSize++;
			}

			// Token: 0x06006436 RID: 25654 RVA: 0x00154EE8 File Offset: 0x001530E8
			public override void InsertRange(int index, ICollection c)
			{
				if (index < 0 || index > this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (c == null)
				{
					throw new ArgumentNullException("c");
				}
				this.InternalUpdateRange();
				int count = c.Count;
				if (count > 0)
				{
					this._baseList.InsertRange(this._baseIndex + index, c);
					this._baseSize += count;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006437 RID: 25655 RVA: 0x00154F5C File Offset: 0x0015315C
			public override int LastIndexOf(object value)
			{
				this.InternalUpdateRange();
				int num = this._baseList.LastIndexOf(value, this._baseIndex + this._baseSize - 1, this._baseSize);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x06006438 RID: 25656 RVA: 0x00152E62 File Offset: 0x00151062
			public override int LastIndexOf(object value, int startIndex)
			{
				return this.LastIndexOf(value, startIndex, startIndex + 1);
			}

			// Token: 0x06006439 RID: 25657 RVA: 0x00154FA0 File Offset: 0x001531A0
			public override int LastIndexOf(object value, int startIndex, int count)
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return -1;
				}
				if (startIndex >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
				}
				int num = this._baseList.LastIndexOf(value, this._baseIndex + startIndex, count);
				if (num >= 0)
				{
					return num - this._baseIndex;
				}
				return -1;
			}

			// Token: 0x0600643A RID: 25658 RVA: 0x00155010 File Offset: 0x00153210
			public override void RemoveAt(int index)
			{
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.InternalUpdateRange();
				this._baseList.RemoveAt(this._baseIndex + index);
				this.InternalUpdateVersion();
				this._baseSize--;
			}

			// Token: 0x0600643B RID: 25659 RVA: 0x00155068 File Offset: 0x00153268
			public override void RemoveRange(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				if (count > 0)
				{
					this._baseList.RemoveRange(this._baseIndex + index, count);
					this.InternalUpdateVersion();
					this._baseSize -= count;
				}
			}

			// Token: 0x0600643C RID: 25660 RVA: 0x001550E0 File Offset: 0x001532E0
			public override void Reverse(int index, int count)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Reverse(this._baseIndex + index, count);
				this.InternalUpdateVersion();
			}

			// Token: 0x0600643D RID: 25661 RVA: 0x00155148 File Offset: 0x00153348
			public override void SetRange(int index, ICollection c)
			{
				this.InternalUpdateRange();
				if (index < 0 || index >= this._baseSize)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this._baseList.SetRange(this._baseIndex + index, c);
				if (c.Count > 0)
				{
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x0600643E RID: 25662 RVA: 0x0015519C File Offset: 0x0015339C
			public override void Sort(int index, int count, IComparer comparer)
			{
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (this._baseSize - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				this.InternalUpdateRange();
				this._baseList.Sort(this._baseIndex + index, count, comparer);
				this.InternalUpdateVersion();
			}

			// Token: 0x1700113C RID: 4412
			public override object this[int index]
			{
				get
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
					}
					return this._baseList[this._baseIndex + index];
				}
				set
				{
					this.InternalUpdateRange();
					if (index < 0 || index >= this._baseSize)
					{
						throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
					}
					this._baseList[this._baseIndex + index] = value;
					this.InternalUpdateVersion();
				}
			}

			// Token: 0x06006441 RID: 25665 RVA: 0x0015527C File Offset: 0x0015347C
			public override object[] ToArray()
			{
				this.InternalUpdateRange();
				if (this._baseSize == 0)
				{
					return Array.Empty<object>();
				}
				object[] array = new object[this._baseSize];
				Array.Copy(this._baseList._items, this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006442 RID: 25666 RVA: 0x001552C8 File Offset: 0x001534C8
			public override Array ToArray(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				this.InternalUpdateRange();
				Array array = Array.CreateInstance(type, this._baseSize);
				this._baseList.CopyTo(this._baseIndex, array, 0, this._baseSize);
				return array;
			}

			// Token: 0x06006443 RID: 25667 RVA: 0x00155316 File Offset: 0x00153516
			public override void TrimToSize()
			{
				throw new NotSupportedException("The specified operation is not supported on Ranges.");
			}

			// Token: 0x04003B17 RID: 15127
			private ArrayList _baseList;

			// Token: 0x04003B18 RID: 15128
			private int _baseIndex;

			// Token: 0x04003B19 RID: 15129
			private int _baseSize;

			// Token: 0x04003B1A RID: 15130
			private int _baseVersion;
		}

		// Token: 0x02000A9E RID: 2718
		[Serializable]
		private sealed class ArrayListEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06006444 RID: 25668 RVA: 0x00155324 File Offset: 0x00153524
			internal ArrayListEnumeratorSimple(ArrayList list)
			{
				this._list = list;
				this._index = -1;
				this._version = list._version;
				this._isArrayList = list.GetType() == typeof(ArrayList);
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
			}

			// Token: 0x06006445 RID: 25669 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006446 RID: 25670 RVA: 0x00155378 File Offset: 0x00153578
			public bool MoveNext()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._isArrayList)
				{
					if (this._index < this._list._size - 1)
					{
						object[] items = this._list._items;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = items[num];
						return true;
					}
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					this._index = this._list._size;
					return false;
				}
				else
				{
					if (this._index < this._list.Count - 1)
					{
						ArrayList list = this._list;
						int num = this._index + 1;
						this._index = num;
						this._currentElement = list[num];
						return true;
					}
					this._index = this._list.Count;
					this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
					return false;
				}
			}

			// Token: 0x1700113D RID: 4413
			// (get) Token: 0x06006447 RID: 25671 RVA: 0x0015545C File Offset: 0x0015365C
			public object Current
			{
				get
				{
					object currentElement = this._currentElement;
					if (ArrayList.ArrayListEnumeratorSimple.s_dummyObject != currentElement)
					{
						return currentElement;
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x06006448 RID: 25672 RVA: 0x00155498 File Offset: 0x00153698
			public void Reset()
			{
				if (this._version != this._list._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._currentElement = ArrayList.ArrayListEnumeratorSimple.s_dummyObject;
				this._index = -1;
			}

			// Token: 0x06006449 RID: 25673 RVA: 0x001554CA File Offset: 0x001536CA
			// Note: this type is marked as 'beforefieldinit'.
			static ArrayListEnumeratorSimple()
			{
			}

			// Token: 0x04003B1B RID: 15131
			private ArrayList _list;

			// Token: 0x04003B1C RID: 15132
			private int _index;

			// Token: 0x04003B1D RID: 15133
			private int _version;

			// Token: 0x04003B1E RID: 15134
			private object _currentElement;

			// Token: 0x04003B1F RID: 15135
			private bool _isArrayList;

			// Token: 0x04003B20 RID: 15136
			private static object s_dummyObject = new object();
		}

		// Token: 0x02000A9F RID: 2719
		internal class ArrayListDebugView
		{
			// Token: 0x0600644A RID: 25674 RVA: 0x001554D6 File Offset: 0x001536D6
			public ArrayListDebugView(ArrayList arrayList)
			{
				if (arrayList == null)
				{
					throw new ArgumentNullException("arrayList");
				}
				this._arrayList = arrayList;
			}

			// Token: 0x1700113E RID: 4414
			// (get) Token: 0x0600644B RID: 25675 RVA: 0x001554F3 File Offset: 0x001536F3
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._arrayList.ToArray();
				}
			}

			// Token: 0x04003B21 RID: 15137
			private ArrayList _arrayList;
		}
	}
}
