using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000B08 RID: 2824
	[DebuggerTypeProxy(typeof(QueueDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x060067E0 RID: 26592 RVA: 0x0016013D File Offset: 0x0015E33D
		public Queue()
		{
			this._array = Array.Empty<T>();
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x00160150 File Offset: 0x0015E350
		public Queue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x00160180 File Offset: 0x0015E380
		public Queue(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._array = EnumerableHelpers.ToArray<T>(collection, out this._size);
			if (this._size != this._array.Length)
			{
				this._tail = this._size;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060067E3 RID: 26595 RVA: 0x001601CF File Offset: 0x0015E3CF
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060067E4 RID: 26596 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060067E5 RID: 26597 RVA: 0x001601D7 File Offset: 0x0015E3D7
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

		// Token: 0x060067E6 RID: 26598 RVA: 0x001601FC File Offset: 0x0015E3FC
		public void Clear()
		{
			if (this._size != 0)
			{
				if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
				{
					if (this._head < this._tail)
					{
						Array.Clear(this._array, this._head, this._size);
					}
					else
					{
						Array.Clear(this._array, this._head, this._array.Length - this._head);
						Array.Clear(this._array, 0, this._tail);
					}
				}
				this._size = 0;
			}
			this._head = 0;
			this._tail = 0;
			this._version++;
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x00160294 File Offset: 0x0015E494
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
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = Math.Min(this._array.Length - this._head, num);
			Array.Copy(this._array, this._head, array, arrayIndex, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, arrayIndex + this._array.Length - this._head, num);
			}
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x00160344 File Offset: 0x0015E544
		void ICollection.CopyTo(Array array, int index)
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
			int length = array.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			try
			{
				int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
				Array.Copy(this._array, this._head, array, index, num2);
				num -= num2;
				if (num > 0)
				{
					Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x0016045C File Offset: 0x0015E65C
		public void Enqueue(T item)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * 200L / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = item;
			this.MoveNext(ref this._tail);
			this._size++;
			this._version++;
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x001604E8 File Offset: 0x0015E6E8
		public Queue<T>.Enumerator GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x001604F0 File Offset: 0x0015E6F0
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x001604F0 File Offset: 0x0015E6F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Queue<T>.Enumerator(this);
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x00160500 File Offset: 0x0015E700
		public T Dequeue()
		{
			int head = this._head;
			T[] array = this._array;
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			T t = array[head];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[head] = default(T);
			}
			this.MoveNext(ref this._head);
			this._size--;
			this._version++;
			return t;
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x00160570 File Offset: 0x0015E770
		public bool TryDequeue(out T result)
		{
			int head = this._head;
			T[] array = this._array;
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = array[head];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[head] = default(T);
			}
			this.MoveNext(ref this._head);
			this._size--;
			this._version++;
			return true;
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x001605E9 File Offset: 0x0015E7E9
		public T Peek()
		{
			if (this._size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			return this._array[this._head];
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x0016060A File Offset: 0x0015E80A
		public bool TryPeek(out T result)
		{
			if (this._size == 0)
			{
				result = default(T);
				return false;
			}
			result = this._array[this._head];
			return true;
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x00160638 File Offset: 0x0015E838
		public bool Contains(T item)
		{
			if (this._size == 0)
			{
				return false;
			}
			if (this._head < this._tail)
			{
				return Array.IndexOf<T>(this._array, item, this._head, this._size) >= 0;
			}
			return Array.IndexOf<T>(this._array, item, this._head, this._array.Length - this._head) >= 0 || Array.IndexOf<T>(this._array, item, 0, this._tail) >= 0;
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x001606BC File Offset: 0x0015E8BC
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			if (this._head < this._tail)
			{
				Array.Copy(this._array, this._head, array, 0, this._size);
			}
			else
			{
				Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
				Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
			}
			return array;
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x00160754 File Offset: 0x0015E954
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
			if (this._size > 0)
			{
				if (this._head < this._tail)
				{
					Array.Copy(this._array, this._head, array, 0, this._size);
				}
				else
				{
					Array.Copy(this._array, this._head, array, 0, this._array.Length - this._head);
					Array.Copy(this._array, 0, array, this._array.Length - this._head, this._tail);
				}
			}
			this._array = array;
			this._head = 0;
			this._tail = ((this._size == capacity) ? 0 : this._size);
			this._version++;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x00160814 File Offset: 0x0015EA14
		private void MoveNext(ref int index)
		{
			int num = index + 1;
			if (num == this._array.Length)
			{
				num = 0;
			}
			index = num;
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x00160836 File Offset: 0x0015EA36
		private void ThrowForEmptyQueue()
		{
			throw new InvalidOperationException("Queue empty.");
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00160844 File Offset: 0x0015EA44
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				this.SetCapacity(this._size);
			}
		}

		// Token: 0x04003C3C RID: 15420
		private T[] _array;

		// Token: 0x04003C3D RID: 15421
		private int _head;

		// Token: 0x04003C3E RID: 15422
		private int _tail;

		// Token: 0x04003C3F RID: 15423
		private int _size;

		// Token: 0x04003C40 RID: 15424
		private int _version;

		// Token: 0x04003C41 RID: 15425
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003C42 RID: 15426
		private const int MinimumGrow = 4;

		// Token: 0x04003C43 RID: 15427
		private const int GrowFactor = 200;

		// Token: 0x02000B09 RID: 2825
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060067F7 RID: 26615 RVA: 0x0016087B File Offset: 0x0015EA7B
			internal Enumerator(Queue<T> q)
			{
				this._q = q;
				this._version = q._version;
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x060067F8 RID: 26616 RVA: 0x001608A3 File Offset: 0x0015EAA3
			public void Dispose()
			{
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x060067F9 RID: 26617 RVA: 0x001608BC File Offset: 0x0015EABC
			public bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					return false;
				}
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -2;
					this._currentElement = default(T);
					return false;
				}
				T[] array = this._q._array;
				int num = array.Length;
				int num2 = this._q._head + this._index;
				if (num2 >= num)
				{
					num2 -= num;
				}
				this._currentElement = array[num2];
				return true;
			}

			// Token: 0x17001232 RID: 4658
			// (get) Token: 0x060067FA RID: 26618 RVA: 0x00160963 File Offset: 0x0015EB63
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

			// Token: 0x060067FB RID: 26619 RVA: 0x0016097A File Offset: 0x0015EB7A
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -1) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			// Token: 0x17001233 RID: 4659
			// (get) Token: 0x060067FC RID: 26620 RVA: 0x00160996 File Offset: 0x0015EB96
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060067FD RID: 26621 RVA: 0x001609A3 File Offset: 0x0015EBA3
			void IEnumerator.Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -1;
				this._currentElement = default(T);
			}

			// Token: 0x04003C44 RID: 15428
			private readonly Queue<T> _q;

			// Token: 0x04003C45 RID: 15429
			private readonly int _version;

			// Token: 0x04003C46 RID: 15430
			private int _index;

			// Token: 0x04003C47 RID: 15431
			private T _currentElement;
		}
	}
}
