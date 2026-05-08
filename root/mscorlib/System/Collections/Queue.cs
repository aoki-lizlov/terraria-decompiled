using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A7F RID: 2687
	[DebuggerTypeProxy(typeof(Queue.QueueDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Queue : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600621E RID: 25118 RVA: 0x0014EB17 File Offset: 0x0014CD17
		public Queue()
			: this(32, 2f)
		{
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x0014EB26 File Offset: 0x0014CD26
		public Queue(int capacity)
			: this(capacity, 2f)
		{
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x0014EB34 File Offset: 0x0014CD34
		public Queue(int capacity, float growFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
			{
				throw new ArgumentOutOfRangeException("growFactor", SR.Format("Queue grow factor must be between {0} and {1}.", 1, 10));
			}
			this._array = new object[capacity];
			this._head = 0;
			this._tail = 0;
			this._size = 0;
			this._growFactor = (int)(growFactor * 100f);
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x0014EBC8 File Offset: 0x0014CDC8
		public Queue(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Enqueue(obj);
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x0014EC13 File Offset: 0x0014CE13
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x0014EC1C File Offset: 0x0014CE1C
		public virtual object Clone()
		{
			Queue queue = new Queue(this._size);
			queue._size = this._size;
			int num = this._size;
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, queue._array, 0, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, queue._array, this._array.Length - this._head, num);
			}
			queue._version = this._version;
			return queue;
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x0014ECBD File Offset: 0x0014CEBD
		public virtual object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x0014ECE0 File Offset: 0x0014CEE0
		public virtual void Clear()
		{
			if (this._size != 0)
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
				this._size = 0;
			}
			this._head = 0;
			this._tail = 0;
			this._version++;
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x0014ED74 File Offset: 0x0014CF74
		public virtual void CopyTo(Array array, int index)
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
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int num = this._size;
			if (num == 0)
			{
				return;
			}
			int num2 = ((this._array.Length - this._head < num) ? (this._array.Length - this._head) : num);
			Array.Copy(this._array, this._head, array, index, num2);
			num -= num2;
			if (num > 0)
			{
				Array.Copy(this._array, 0, array, index + this._array.Length - this._head, num);
			}
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x0014EE44 File Offset: 0x0014D044
		public virtual void Enqueue(object obj)
		{
			if (this._size == this._array.Length)
			{
				int num = (int)((long)this._array.Length * (long)this._growFactor / 100L);
				if (num < this._array.Length + 4)
				{
					num = this._array.Length + 4;
				}
				this.SetCapacity(num);
			}
			this._array[this._tail] = obj;
			this._tail = (this._tail + 1) % this._array.Length;
			this._size++;
			this._version++;
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x0014EED8 File Offset: 0x0014D0D8
		public virtual IEnumerator GetEnumerator()
		{
			return new Queue.QueueEnumerator(this);
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x0014EEE0 File Offset: 0x0014D0E0
		public virtual object Dequeue()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			object obj = this._array[this._head];
			this._array[this._head] = null;
			this._head = (this._head + 1) % this._array.Length;
			this._size--;
			this._version++;
			return obj;
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x0014EF4E File Offset: 0x0014D14E
		public virtual object Peek()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Queue empty.");
			}
			return this._array[this._head];
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x0014EF70 File Offset: 0x0014D170
		public static Queue Synchronized(Queue queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			return new Queue.SynchronizedQueue(queue);
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x0014EF88 File Offset: 0x0014D188
		public virtual bool Contains(object obj)
		{
			int num = this._head;
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[num] == null)
					{
						return true;
					}
				}
				else if (this._array[num] != null && this._array[num].Equals(obj))
				{
					return true;
				}
				num = (num + 1) % this._array.Length;
			}
			return false;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x0014EFE6 File Offset: 0x0014D1E6
		internal object GetElement(int i)
		{
			return this._array[(this._head + i) % this._array.Length];
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x0014F000 File Offset: 0x0014D200
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
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

		// Token: 0x06006230 RID: 25136 RVA: 0x0014F098 File Offset: 0x0014D298
		private void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
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

		// Token: 0x06006231 RID: 25137 RVA: 0x0014F156 File Offset: 0x0014D356
		public virtual void TrimToSize()
		{
			this.SetCapacity(this._size);
		}

		// Token: 0x04003AB7 RID: 15031
		private object[] _array;

		// Token: 0x04003AB8 RID: 15032
		private int _head;

		// Token: 0x04003AB9 RID: 15033
		private int _tail;

		// Token: 0x04003ABA RID: 15034
		private int _size;

		// Token: 0x04003ABB RID: 15035
		private int _growFactor;

		// Token: 0x04003ABC RID: 15036
		private int _version;

		// Token: 0x04003ABD RID: 15037
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003ABE RID: 15038
		private const int _MinimumGrow = 4;

		// Token: 0x04003ABF RID: 15039
		private const int _ShrinkThreshold = 32;

		// Token: 0x02000A80 RID: 2688
		[Serializable]
		private class SynchronizedQueue : Queue
		{
			// Token: 0x06006232 RID: 25138 RVA: 0x0014F164 File Offset: 0x0014D364
			internal SynchronizedQueue(Queue q)
			{
				this._q = q;
				this._root = this._q.SyncRoot;
			}

			// Token: 0x170010C4 RID: 4292
			// (get) Token: 0x06006233 RID: 25139 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010C5 RID: 4293
			// (get) Token: 0x06006234 RID: 25140 RVA: 0x0014F184 File Offset: 0x0014D384
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170010C6 RID: 4294
			// (get) Token: 0x06006235 RID: 25141 RVA: 0x0014F18C File Offset: 0x0014D38C
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._q.Count;
					}
					return count;
				}
			}

			// Token: 0x06006236 RID: 25142 RVA: 0x0014F1D4 File Offset: 0x0014D3D4
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._q.Clear();
				}
			}

			// Token: 0x06006237 RID: 25143 RVA: 0x0014F21C File Offset: 0x0014D41C
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Queue.SynchronizedQueue((Queue)this._q.Clone());
				}
				return obj;
			}

			// Token: 0x06006238 RID: 25144 RVA: 0x0014F270 File Offset: 0x0014D470
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._q.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x06006239 RID: 25145 RVA: 0x0014F2B8 File Offset: 0x0014D4B8
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._q.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x0600623A RID: 25146 RVA: 0x0014F300 File Offset: 0x0014D500
			public override void Enqueue(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._q.Enqueue(value);
				}
			}

			// Token: 0x0600623B RID: 25147 RVA: 0x0014F348 File Offset: 0x0014D548
			public override object Dequeue()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._q.Dequeue();
				}
				return obj;
			}

			// Token: 0x0600623C RID: 25148 RVA: 0x0014F390 File Offset: 0x0014D590
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._q.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x0600623D RID: 25149 RVA: 0x0014F3D8 File Offset: 0x0014D5D8
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._q.Peek();
				}
				return obj;
			}

			// Token: 0x0600623E RID: 25150 RVA: 0x0014F420 File Offset: 0x0014D620
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._q.ToArray();
				}
				return array;
			}

			// Token: 0x0600623F RID: 25151 RVA: 0x0014F468 File Offset: 0x0014D668
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._q.TrimToSize();
				}
			}

			// Token: 0x04003AC0 RID: 15040
			private Queue _q;

			// Token: 0x04003AC1 RID: 15041
			private object _root;
		}

		// Token: 0x02000A81 RID: 2689
		[Serializable]
		private class QueueEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06006240 RID: 25152 RVA: 0x0014F4B0 File Offset: 0x0014D6B0
			internal QueueEnumerator(Queue q)
			{
				this._q = q;
				this._version = this._q._version;
				this._index = 0;
				this._currentElement = this._q._array;
				if (this._q._size == 0)
				{
					this._index = -1;
				}
			}

			// Token: 0x06006241 RID: 25153 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006242 RID: 25154 RVA: 0x0014F508 File Offset: 0x0014D708
			public virtual bool MoveNext()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < 0)
				{
					this._currentElement = this._q._array;
					return false;
				}
				this._currentElement = this._q.GetElement(this._index);
				this._index++;
				if (this._index == this._q._size)
				{
					this._index = -1;
				}
				return true;
			}

			// Token: 0x170010C7 RID: 4295
			// (get) Token: 0x06006243 RID: 25155 RVA: 0x0014F58F File Offset: 0x0014D78F
			public virtual object Current
			{
				get
				{
					if (this._currentElement != this._q._array)
					{
						return this._currentElement;
					}
					if (this._index == 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					throw new InvalidOperationException("Enumeration already finished.");
				}
			}

			// Token: 0x06006244 RID: 25156 RVA: 0x0014F5C8 File Offset: 0x0014D7C8
			public virtual void Reset()
			{
				if (this._version != this._q._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._q._size == 0)
				{
					this._index = -1;
				}
				else
				{
					this._index = 0;
				}
				this._currentElement = this._q._array;
			}

			// Token: 0x04003AC2 RID: 15042
			private Queue _q;

			// Token: 0x04003AC3 RID: 15043
			private int _index;

			// Token: 0x04003AC4 RID: 15044
			private int _version;

			// Token: 0x04003AC5 RID: 15045
			private object _currentElement;
		}

		// Token: 0x02000A82 RID: 2690
		internal class QueueDebugView
		{
			// Token: 0x06006245 RID: 25157 RVA: 0x0014F621 File Offset: 0x0014D821
			public QueueDebugView(Queue queue)
			{
				if (queue == null)
				{
					throw new ArgumentNullException("queue");
				}
				this._queue = queue;
			}

			// Token: 0x170010C8 RID: 4296
			// (get) Token: 0x06006246 RID: 25158 RVA: 0x0014F63E File Offset: 0x0014D83E
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._queue.ToArray();
				}
			}

			// Token: 0x04003AC6 RID: 15046
			private Queue _queue;
		}
	}
}
