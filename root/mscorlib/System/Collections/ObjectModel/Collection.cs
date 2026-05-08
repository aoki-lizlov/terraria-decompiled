using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000AD8 RID: 2776
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06006607 RID: 26119 RVA: 0x0015B1BF File Offset: 0x001593BF
		public Collection()
		{
			this.items = new List<T>();
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x0015B1D2 File Offset: 0x001593D2
		public Collection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.items = list;
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x0015B1EA File Offset: 0x001593EA
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600660A RID: 26122 RVA: 0x0015B1F7 File Offset: 0x001593F7
		protected IList<T> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170011B0 RID: 4528
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (this.items.IsReadOnly)
				{
					ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				}
				if (index >= this.items.Count)
				{
					ThrowHelper.ThrowArgumentOutOfRange_IndexException();
				}
				this.SetItem(index, value);
			}
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0015B240 File Offset: 0x00159440
		public void Add(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int count = this.items.Count;
			this.InsertItem(count, item);
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0015B275 File Offset: 0x00159475
		public void Clear()
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			this.ClearItems();
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x0015B291 File Offset: 0x00159491
		public void CopyTo(T[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0015B2A0 File Offset: 0x001594A0
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x0015B2AE File Offset: 0x001594AE
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x0015B2BB File Offset: 0x001594BB
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x0015B2C9 File Offset: 0x001594C9
		public void Insert(int index, T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index > this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.InsertItem(index, item);
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x0015B2FC File Offset: 0x001594FC
		public bool Remove(T item)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			int num = this.items.IndexOf(item);
			if (num < 0)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x0015B338 File Offset: 0x00159538
		public void RemoveAt(int index)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (index >= this.items.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			this.RemoveItem(index);
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x0015B368 File Offset: 0x00159568
		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x0015B375 File Offset: 0x00159575
		protected virtual void InsertItem(int index, T item)
		{
			this.items.Insert(index, item);
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x0015B384 File Offset: 0x00159584
		protected virtual void RemoveItem(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x0015B392 File Offset: 0x00159592
		protected virtual void SetItem(int index, T item)
		{
			this.items[index] = item;
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x0015B3A1 File Offset: 0x001595A1
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x0015B3AE File Offset: 0x001595AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x0600661D RID: 26141 RVA: 0x0015B3BC File Offset: 0x001595BC
		object ICollection.SyncRoot
		{
			get
			{
				ICollection collection = this.items as ICollection;
				if (collection == null)
				{
					return this;
				}
				return collection.SyncRoot;
			}
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x0015B3E0 File Offset: 0x001595E0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.items.CopyTo(array2, index);
				return;
			}
			Type elementType = array.GetType().GetElementType();
			Type typeFromHandle = typeof(T);
			if (!elementType.IsAssignableFrom(typeFromHandle) && !typeFromHandle.IsAssignableFrom(elementType))
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			int count = this.items.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.items[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x170011B4 RID: 4532
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
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

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x0015B3A1 File Offset: 0x001595A1
		bool IList.IsReadOnly
		{
			get
			{
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06006622 RID: 26146 RVA: 0x0015B538 File Offset: 0x00159738
		bool IList.IsFixedSize
		{
			get
			{
				IList list = this.items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return this.items.IsReadOnly;
			}
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x0015B568 File Offset: 0x00159768
		int IList.Add(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Add((T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
			return this.Count - 1;
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x0015B5CC File Offset: 0x001597CC
		bool IList.Contains(object value)
		{
			return Collection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x0015B5E4 File Offset: 0x001597E4
		int IList.IndexOf(object value)
		{
			if (Collection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0015B5FC File Offset: 0x001597FC
		void IList.Insert(int index, object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			try
			{
				this.Insert(index, (T)((object)value));
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(T));
			}
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0015B658 File Offset: 0x00159858
		void IList.Remove(object value)
		{
			if (this.items.IsReadOnly)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
			if (Collection<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x0015B684 File Offset: 0x00159884
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x04003BD6 RID: 15318
		private IList<T> items;
	}
}
