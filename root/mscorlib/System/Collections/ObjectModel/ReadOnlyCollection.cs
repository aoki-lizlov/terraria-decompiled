using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000AD9 RID: 2777
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
	{
		// Token: 0x06006629 RID: 26153 RVA: 0x0015B6B1 File Offset: 0x001598B1
		public ReadOnlyCollection(IList<T> list)
		{
			if (list == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.list);
			}
			this.list = list;
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x0015B6C9 File Offset: 0x001598C9
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170011B8 RID: 4536
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x0600662C RID: 26156 RVA: 0x0015B6E4 File Offset: 0x001598E4
		public bool Contains(T value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x0015B6F2 File Offset: 0x001598F2
		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x0015B701 File Offset: 0x00159901
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x0015B70E File Offset: 0x0015990E
		public int IndexOf(T value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06006630 RID: 26160 RVA: 0x0015B71C File Offset: 0x0015991C
		protected IList<T> Items
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011BB RID: 4539
		T IList<T>.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x06006634 RID: 26164 RVA: 0x0015B724 File Offset: 0x00159924
		void ICollection<T>.Add(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x0015B724 File Offset: 0x00159924
		void ICollection<T>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x0015B724 File Offset: 0x00159924
		void IList<T>.Insert(int index, T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x0015B72D File Offset: 0x0015992D
		bool ICollection<T>.Remove(T value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x0015B724 File Offset: 0x00159924
		void IList<T>.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x0015B737 File Offset: 0x00159937
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600663A RID: 26170 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600663B RID: 26171 RVA: 0x0015B744 File Offset: 0x00159944
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.list as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x0015B790 File Offset: 0x00159990
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
				this.list.CopyTo(array2, index);
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
			int count = this.list.Count;
			try
			{
				for (int i = 0; i < count; i++)
				{
					array3[index++] = this.list[i];
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x0600663E RID: 26174 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011C0 RID: 4544
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x0015B89F File Offset: 0x00159A9F
		int IList.Add(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return -1;
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x0015B724 File Offset: 0x00159924
		void IList.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x0015B8AC File Offset: 0x00159AAC
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && default(T) == null);
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x0015B8D9 File Offset: 0x00159AD9
		bool IList.Contains(object value)
		{
			return ReadOnlyCollection<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x0015B8F1 File Offset: 0x00159AF1
		int IList.IndexOf(object value)
		{
			if (ReadOnlyCollection<T>.IsCompatibleObject(value))
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x0015B724 File Offset: 0x00159924
		void IList.Insert(int index, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x0015B724 File Offset: 0x00159924
		void IList.Remove(object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x0015B724 File Offset: 0x00159924
		void IList.RemoveAt(int index)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x04003BD7 RID: 15319
		private IList<T> list;

		// Token: 0x04003BD8 RID: 15320
		[NonSerialized]
		private object _syncRoot;
	}
}
