using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000B0B RID: 2827
	[DebuggerTypeProxy(typeof(StackDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x06006800 RID: 26624 RVA: 0x00160A00 File Offset: 0x0015EC00
		public Stack()
		{
			this._array = Array.Empty<T>();
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x00160A13 File Offset: 0x0015EC13
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x00160A41 File Offset: 0x0015EC41
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._array = EnumerableHelpers.ToArray<T>(collection, out this._size);
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x00160A69 File Offset: 0x0015EC69
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06006805 RID: 26629 RVA: 0x00160A71 File Offset: 0x0015EC71
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

		// Token: 0x06006806 RID: 26630 RVA: 0x00160A93 File Offset: 0x0015EC93
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._array, 0, this._size);
			}
			this._size = 0;
			this._version++;
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x00160AC3 File Offset: 0x0015ECC3
		public bool Contains(T item)
		{
			return this._size != 0 && Array.LastIndexOf<T>(this._array, item, this._size - 1) != -1;
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x00160AEC File Offset: 0x0015ECEC
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = 0;
			int num = arrayIndex + this._size;
			while (i < this._size)
			{
				array[--num] = this._array[i++];
			}
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x00160B70 File Offset: 0x0015ED70
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x00160C40 File Offset: 0x0015EE40
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x00160C48 File Offset: 0x0015EE48
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x00160C48 File Offset: 0x0015EE48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x00160C58 File Offset: 0x0015EE58
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				Array.Resize<T>(ref this._array, this._size);
				this._version++;
			}
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x00160CA4 File Offset: 0x0015EEA4
		public T Peek()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			return array[num];
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x00160CD4 File Offset: 0x0015EED4
		public bool TryPeek(out T result)
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				result = default(T);
				return false;
			}
			result = array[num];
			return true;
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x00160D10 File Offset: 0x0015EF10
		public T Pop()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			this._version++;
			this._size = num;
			T t = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return t;
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x00160D6C File Offset: 0x0015EF6C
		public bool TryPop(out T result)
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				result = default(T);
				return false;
			}
			this._version++;
			this._size = num;
			result = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return true;
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x00160DD4 File Offset: 0x0015EFD4
		public void Push(T item)
		{
			int size = this._size;
			T[] array = this._array;
			if (size < array.Length)
			{
				array[size] = item;
				this._version++;
				this._size = size + 1;
				return;
			}
			this.PushWithResize(item);
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x00160E1C File Offset: 0x0015F01C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PushWithResize(T item)
		{
			Array.Resize<T>(ref this._array, (this._array.Length == 0) ? 4 : (2 * this._array.Length));
			this._array[this._size] = item;
			this._version++;
			this._size++;
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x00160E78 File Offset: 0x0015F078
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x00160ECD File Offset: 0x0015F0CD
		private void ThrowForEmptyStack()
		{
			throw new InvalidOperationException("Stack empty.");
		}

		// Token: 0x04003C49 RID: 15433
		private T[] _array;

		// Token: 0x04003C4A RID: 15434
		private int _size;

		// Token: 0x04003C4B RID: 15435
		private int _version;

		// Token: 0x04003C4C RID: 15436
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003C4D RID: 15437
		private const int DefaultCapacity = 4;

		// Token: 0x02000B0C RID: 2828
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006816 RID: 26646 RVA: 0x00160ED9 File Offset: 0x0015F0D9
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = stack._version;
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x06006817 RID: 26647 RVA: 0x00160F02 File Offset: 0x0015F102
			public void Dispose()
			{
				this._index = -1;
			}

			// Token: 0x06006818 RID: 26648 RVA: 0x00160F0C File Offset: 0x0015F10C
			public bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					bool flag = this._index >= 0;
					if (flag)
					{
						this._currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				bool flag2 = num >= 0;
				if (flag2)
				{
					this._currentElement = this._stack._array[this._index];
					return flag2;
				}
				this._currentElement = default(T);
				return flag2;
			}

			// Token: 0x17001238 RID: 4664
			// (get) Token: 0x06006819 RID: 26649 RVA: 0x00160FCE File Offset: 0x0015F1CE
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						this.ThrowEnumerationNotStartedOrEnded();
					}
					return this._currentElement;
				}
			}

			// Token: 0x0600681A RID: 26650 RVA: 0x00160FE5 File Offset: 0x0015F1E5
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -2) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			// Token: 0x17001239 RID: 4665
			// (get) Token: 0x0600681B RID: 26651 RVA: 0x00161002 File Offset: 0x0015F202
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600681C RID: 26652 RVA: 0x0016100F File Offset: 0x0015F20F
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x04003C4E RID: 15438
			private readonly Stack<T> _stack;

			// Token: 0x04003C4F RID: 15439
			private readonly int _version;

			// Token: 0x04003C50 RID: 15440
			private int _index;

			// Token: 0x04003C51 RID: 15441
			private T _currentElement;
		}
	}
}
