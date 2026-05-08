using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000AFB RID: 2811
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class List<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06006749 RID: 26441 RVA: 0x0015E2EE File Offset: 0x0015C4EE
		public List()
		{
			this._items = List<T>.s_emptyArray;
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x0015E301 File Offset: 0x0015C501
		public List(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (capacity == 0)
			{
				this._items = List<T>.s_emptyArray;
				return;
			}
			this._items = new T[capacity];
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x0015E330 File Offset: 0x0015C530
		public List(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			if (collection2 == null)
			{
				this._size = 0;
				this._items = List<T>.s_emptyArray;
				this.AddEnumerable(collection);
				return;
			}
			int count = collection2.Count;
			if (count == 0)
			{
				this._items = List<T>.s_emptyArray;
				return;
			}
			this._items = new T[count];
			collection2.CopyTo(this._items, 0);
			this._size = count;
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x0015E3A6 File Offset: 0x0015C5A6
		// (set) Token: 0x0600674D RID: 26445 RVA: 0x0015E3B0 File Offset: 0x0015C5B0
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
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
				}
				if (value != this._items.Length)
				{
					if (value > 0)
					{
						T[] array = new T[value];
						if (this._size > 0)
						{
							Array.Copy(this._items, 0, array, 0, this._size);
						}
						this._items = array;
						return;
					}
					this._items = List<T>.s_emptyArray;
				}
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x0015E415 File Offset: 0x0015C615
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x0000408A File Offset: 0x0000228A
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x0000408A File Offset: 0x0000228A
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x0015E41D File Offset: 0x0015C61D
		object ICollection.SyncRoot
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

		// Token: 0x1700121F RID: 4639
		public T this[int index]
		{
			get
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				return this._items[index];
			}
			set
			{
				if (index >= this._size)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this._items[index] = value;
				this._version++;
			}
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x0015E488 File Offset: 0x0015C688
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x17001220 RID: 4640
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
				try
				{
					this[index] = (T)((object)value);
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
				}
			}
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x0015E50C File Offset: 0x0015C70C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(T item)
		{
			this._version++;
			T[] items = this._items;
			int size = this._size;
			if (size < items.Length)
			{
				this._size = size + 1;
				items[size] = item;
				return;
			}
			this.AddWithResize(item);
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x0015E554 File Offset: 0x0015C754
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void AddWithResize(T item)
		{
			int size = this._size;
			this.EnsureCapacity(size + 1);
			this._size = size + 1;
			this._items[size] = item;
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x0015E588 File Offset: 0x0015C788
		int IList.Add(object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Add((T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x0015E5D8 File Offset: 0x0015C7D8
		public void AddRange(IEnumerable<T> collection)
		{
			this.InsertRange(this._size, collection);
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x0015E5E7 File Offset: 0x0015C7E7
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x0015E5EF File Offset: 0x0015C7EF
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			return Array.BinarySearch<T>(this._items, index, count, item, comparer);
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x0015E628 File Offset: 0x0015C828
		public int BinarySearch(T item)
		{
			return this.BinarySearch(0, this.Count, item, null);
		}

		// Token: 0x06006760 RID: 26464 RVA: 0x0015E639 File Offset: 0x0015C839
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return this.BinarySearch(0, this.Count, item, comparer);
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x0015E64C File Offset: 0x0015C84C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			this._version++;
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				int size = this._size;
				this._size = 0;
				if (size > 0)
				{
					Array.Clear(this._items, 0, size);
					return;
				}
			}
			else
			{
				this._size = 0;
			}
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x0015E695 File Offset: 0x0015C895
		public bool Contains(T item)
		{
			return this._size != 0 && this.IndexOf(item) != -1;
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0015E6AE File Offset: 0x0015C8AE
		bool IList.Contains(object item)
		{
			return List<T>.IsCompatibleObject(item) && this.Contains((T)((object)item));
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x0015E6C8 File Offset: 0x0015C8C8
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);
			}
			List<TOutput> list = new List<TOutput>(this._size);
			for (int i = 0; i < this._size; i++)
			{
				list._items[i] = converter(this._items[i]);
			}
			list._size = this._size;
			return list;
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x0015E727 File Offset: 0x0015C927
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x0015E734 File Offset: 0x0015C934
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array != null && array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			try
			{
				Array.Copy(this._items, 0, array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x0015E784 File Offset: 0x0015C984
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			Array.Copy(this._items, index, array, arrayIndex, count);
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x0015E7A9 File Offset: 0x0015C9A9
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this._items, 0, array, arrayIndex, this._size);
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x0015E7C0 File Offset: 0x0015C9C0
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

		// Token: 0x0600676A RID: 26474 RVA: 0x0015E80A File Offset: 0x0015CA0A
		public bool Exists(Predicate<T> match)
		{
			return this.FindIndex(match) != -1;
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x0015E81C File Offset: 0x0015CA1C
		public T Find(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x0015E870 File Offset: 0x0015CA70
		public List<T> FindAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			List<T> list = new List<T>();
			for (int i = 0; i < this._size; i++)
			{
				if (match(this._items[i]))
				{
					list.Add(this._items[i]);
				}
			}
			return list;
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x0015E8C4 File Offset: 0x0015CAC4
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this._size, match);
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x0015E8D4 File Offset: 0x0015CAD4
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this._size - startIndex, match);
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x0015E8E8 File Offset: 0x0015CAE8
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			if (startIndex > this._size)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex > this._size - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x0015E948 File Offset: 0x0015CB48
		public T FindLast(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = this._size - 1; i >= 0; i--)
			{
				if (match(this._items[i]))
				{
					return this._items[i];
				}
			}
			return default(T);
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x0015E99B File Offset: 0x0015CB9B
		public int FindLastIndex(Predicate<T> match)
		{
			return this.FindLastIndex(this._size - 1, this._size, match);
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x0015E9B2 File Offset: 0x0015CBB2
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return this.FindLastIndex(startIndex, startIndex + 1, match);
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0015E9C0 File Offset: 0x0015CBC0
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			if (this._size == 0)
			{
				if (startIndex != -1)
				{
					ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
				}
			}
			else if (startIndex >= this._size)
			{
				ThrowHelper.ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index();
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(this._items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x0015EA30 File Offset: 0x0015CC30
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.action);
			}
			int version = this._version;
			int num = 0;
			while (num < this._size && version == this._version)
			{
				action(this._items[num]);
				num++;
			}
			if (Environment.GetEnvironmentVariable("MONO_FORCE_COMPAT") == null && version != this._version)
			{
				ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
			}
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x0015EA94 File Offset: 0x0015CC94
		public List<T>.Enumerator GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x0015EA9C File Offset: 0x0015CC9C
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x0015EA9C File Offset: 0x0015CC9C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new List<T>.Enumerator(this);
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x0015EAAC File Offset: 0x0015CCAC
		public List<T> GetRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			List<T> list = new List<T>(count);
			Array.Copy(this._items, index, list._items, 0, count);
			list._size = count;
			return list;
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x0015EB03 File Offset: 0x0015CD03
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this._items, item, 0, this._size);
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x0015EB18 File Offset: 0x0015CD18
		int IList.IndexOf(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				return this.IndexOf((T)((object)item));
			}
			return -1;
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x0015EB30 File Offset: 0x0015CD30
		public int IndexOf(T item, int index)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return Array.IndexOf<T>(this._items, item, index, this._size - index);
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x0015EB55 File Offset: 0x0015CD55
		public int IndexOf(T item, int index, int count)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			if (count < 0 || index > this._size - count)
			{
				ThrowHelper.ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count();
			}
			return Array.IndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x0015EB88 File Offset: 0x0015CD88
		public void Insert(int index, T item)
		{
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_ListInsert);
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

		// Token: 0x0600677E RID: 26494 RVA: 0x0015EC14 File Offset: 0x0015CE14
		void IList.Insert(int index, object item)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(item, ExceptionArgument.item);
			try
			{
				this.Insert(index, (T)((object)item));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(item, typeof(T));
			}
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x0015EC5C File Offset: 0x0015CE5C
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			if (index > this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
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
						collection2.CopyTo(this._items, index);
					}
					this._size += count;
				}
			}
			else
			{
				if (index < this._size)
				{
					using (IEnumerator<T> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							this.Insert(index++, t);
						}
						goto IL_00FB;
					}
				}
				this.AddEnumerable(collection);
			}
			IL_00FB:
			this._version++;
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x0015ED84 File Offset: 0x0015CF84
		public int LastIndexOf(T item)
		{
			if (this._size == 0)
			{
				return -1;
			}
			return this.LastIndexOf(item, this._size - 1, this._size);
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x0015EDA5 File Offset: 0x0015CFA5
		public int LastIndexOf(T item, int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			return this.LastIndexOf(item, index, index + 1);
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x0015EDC0 File Offset: 0x0015CFC0
		public int LastIndexOf(T item, int index, int count)
		{
			if (this.Count != 0 && index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (this.Count != 0 && count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size == 0)
			{
				return -1;
			}
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			if (count > index + 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_BiggerThanCollection);
			}
			return Array.LastIndexOf<T>(this._items, item, index, count);
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x0015EE2C File Offset: 0x0015D02C
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

		// Token: 0x06006784 RID: 26500 RVA: 0x0015EE4F File Offset: 0x0015D04F
		void IList.Remove(object item)
		{
			if (List<T>.IsCompatibleObject(item))
			{
				this.Remove((T)((object)item));
			}
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x0015EE68 File Offset: 0x0015D068
		public int RemoveAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
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
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._items, num, this._size - num);
			}
			int num2 = this._size - num;
			this._size = num;
			this._version++;
			return num2;
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x0015EF40 File Offset: 0x0015D140
		public void RemoveAt(int index)
		{
			if (index >= this._size)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._items, index + 1, this._items, index, this._size - index);
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				this._items[this._size] = default(T);
			}
			this._version++;
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0015EFC0 File Offset: 0x0015D1C0
		public void RemoveRange(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 0)
			{
				int size = this._size;
				this._size -= count;
				if (index < this._size)
				{
					Array.Copy(this._items, index + count, this._items, index, this._size - index);
				}
				this._version++;
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					Array.Clear(this._items, this._size, count);
				}
			}
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x0015F05A File Offset: 0x0015D25A
		public void Reverse()
		{
			this.Reverse(0, this.Count);
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x0015F06C File Offset: 0x0015D26C
		public void Reverse(int index, int count)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				Array.Reverse<T>(this._items, index, count);
			}
			this._version++;
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x0015F0BF File Offset: 0x0015D2BF
		public void Sort()
		{
			this.Sort(0, this.Count, null);
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0015F0CF File Offset: 0x0015D2CF
		public void Sort(IComparer<T> comparer)
		{
			this.Sort(0, this.Count, comparer);
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x0015F0E0 File Offset: 0x0015D2E0
		public void Sort(int index, int count, IComparer<T> comparer)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (this._size - index < count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidOffLen);
			}
			if (count > 1)
			{
				Array.Sort<T>(this._items, index, count, comparer);
			}
			this._version++;
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x0015F134 File Offset: 0x0015D334
		public void Sort(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (this._size > 1)
			{
				ArraySortHelper<T>.Sort(this._items, 0, this._size, comparison);
			}
			this._version++;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x0015F16C File Offset: 0x0015D36C
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return List<T>.s_emptyArray;
			}
			T[] array = new T[this._size];
			Array.Copy(this._items, 0, array, 0, this._size);
			return array;
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x0015F1A8 File Offset: 0x0015D3A8
		public void TrimExcess()
		{
			int num = (int)((double)this._items.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x0015F1E0 File Offset: 0x0015D3E0
		public bool TrueForAll(Predicate<T> match)
		{
			if (match == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);
			}
			for (int i = 0; i < this._size; i++)
			{
				if (!match(this._items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x0015F220 File Offset: 0x0015D420
		private void AddEnumerable(IEnumerable<T> enumerable)
		{
			this._version++;
			foreach (T t in enumerable)
			{
				if (this._size == this._items.Length)
				{
					this.EnsureCapacity(this._size + 1);
				}
				T[] items = this._items;
				int size = this._size;
				this._size = size + 1;
				items[size] = t;
			}
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x0015F2AC File Offset: 0x0015D4AC
		// Note: this type is marked as 'beforefieldinit'.
		static List()
		{
		}

		// Token: 0x04003C15 RID: 15381
		private const int DefaultCapacity = 4;

		// Token: 0x04003C16 RID: 15382
		private T[] _items;

		// Token: 0x04003C17 RID: 15383
		private int _size;

		// Token: 0x04003C18 RID: 15384
		private int _version;

		// Token: 0x04003C19 RID: 15385
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003C1A RID: 15386
		private static readonly T[] s_emptyArray = new T[0];

		// Token: 0x02000AFC RID: 2812
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006793 RID: 26515 RVA: 0x0015F2B9 File Offset: 0x0015D4B9
			internal Enumerator(List<T> list)
			{
				this._list = list;
				this._index = 0;
				this._version = list._version;
				this._current = default(T);
			}

			// Token: 0x06006794 RID: 26516 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x06006795 RID: 26517 RVA: 0x0015F2E4 File Offset: 0x0015D4E4
			public bool MoveNext()
			{
				List<T> list = this._list;
				if (this._version == list._version && this._index < list._size)
				{
					this._current = list._items[this._index];
					this._index++;
					return true;
				}
				return this.MoveNextRare();
			}

			// Token: 0x06006796 RID: 26518 RVA: 0x0015F341 File Offset: 0x0015D541
			private bool MoveNextRare()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = this._list._size + 1;
				this._current = default(T);
				return false;
			}

			// Token: 0x17001221 RID: 4641
			// (get) Token: 0x06006797 RID: 26519 RVA: 0x0015F37B File Offset: 0x0015D57B
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x17001222 RID: 4642
			// (get) Token: 0x06006798 RID: 26520 RVA: 0x0015F383 File Offset: 0x0015D583
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._list._size + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this.Current;
				}
			}

			// Token: 0x06006799 RID: 26521 RVA: 0x0015F3B2 File Offset: 0x0015D5B2
			void IEnumerator.Reset()
			{
				if (this._version != this._list._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04003C1B RID: 15387
			private List<T> _list;

			// Token: 0x04003C1C RID: 15388
			private int _index;

			// Token: 0x04003C1D RID: 15389
			private int _version;

			// Token: 0x04003C1E RID: 15390
			private T _current;
		}
	}
}
