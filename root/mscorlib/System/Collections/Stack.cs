using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A8A RID: 2698
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060062BF RID: 25279 RVA: 0x00150931 File Offset: 0x0014EB31
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x00150954 File Offset: 0x0014EB54
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x00150994 File Offset: 0x0014EB94
		public Stack(ICollection col)
			: this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060062C2 RID: 25282 RVA: 0x001509DF File Offset: 0x0014EBDF
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060062C3 RID: 25283 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060062C4 RID: 25284 RVA: 0x001509E7 File Offset: 0x0014EBE7
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

		// Token: 0x060062C5 RID: 25285 RVA: 0x00150A09 File Offset: 0x0014EC09
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x00150A34 File Offset: 0x0014EC34
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x00150A80 File Offset: 0x0014EC80
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x00150ACC File Offset: 0x0014ECCC
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
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = 0;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x00150B88 File Offset: 0x0014ED88
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x00150B90 File Offset: 0x0014ED90
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			return this._array[this._size - 1];
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x00150BB4 File Offset: 0x0014EDB4
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object obj = array[num];
			this._array[this._size] = null;
			return obj;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x00150C08 File Offset: 0x0014EE08
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x00150C77 File Offset: 0x0014EE77
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x00150C90 File Offset: 0x0014EE90
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x04003AE3 RID: 15075
		private object[] _array;

		// Token: 0x04003AE4 RID: 15076
		private int _size;

		// Token: 0x04003AE5 RID: 15077
		private int _version;

		// Token: 0x04003AE6 RID: 15078
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003AE7 RID: 15079
		private const int _defaultCapacity = 10;

		// Token: 0x02000A8B RID: 2699
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x060062CF RID: 25295 RVA: 0x00150CDD File Offset: 0x0014EEDD
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x170010F1 RID: 4337
			// (get) Token: 0x060062D0 RID: 25296 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010F2 RID: 4338
			// (get) Token: 0x060062D1 RID: 25297 RVA: 0x00150CF8 File Offset: 0x0014EEF8
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170010F3 RID: 4339
			// (get) Token: 0x060062D2 RID: 25298 RVA: 0x00150D00 File Offset: 0x0014EF00
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x060062D3 RID: 25299 RVA: 0x00150D48 File Offset: 0x0014EF48
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._s.Contains(obj);
				}
				return flag2;
			}

			// Token: 0x060062D4 RID: 25300 RVA: 0x00150D90 File Offset: 0x0014EF90
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return obj;
			}

			// Token: 0x060062D5 RID: 25301 RVA: 0x00150DE4 File Offset: 0x0014EFE4
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x060062D6 RID: 25302 RVA: 0x00150E2C File Offset: 0x0014F02C
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x060062D7 RID: 25303 RVA: 0x00150E74 File Offset: 0x0014F074
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x060062D8 RID: 25304 RVA: 0x00150EBC File Offset: 0x0014F0BC
			public override object Pop()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Pop();
				}
				return obj;
			}

			// Token: 0x060062D9 RID: 25305 RVA: 0x00150F04 File Offset: 0x0014F104
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x060062DA RID: 25306 RVA: 0x00150F4C File Offset: 0x0014F14C
			public override object Peek()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._s.Peek();
				}
				return obj;
			}

			// Token: 0x060062DB RID: 25307 RVA: 0x00150F94 File Offset: 0x0014F194
			public override object[] ToArray()
			{
				object root = this._root;
				object[] array;
				lock (root)
				{
					array = this._s.ToArray();
				}
				return array;
			}

			// Token: 0x04003AE8 RID: 15080
			private Stack _s;

			// Token: 0x04003AE9 RID: 15081
			private object _root;
		}

		// Token: 0x02000A8C RID: 2700
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x060062DC RID: 25308 RVA: 0x00150FDC File Offset: 0x0014F1DC
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x060062DD RID: 25309 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x060062DE RID: 25310 RVA: 0x0015100C File Offset: 0x0014F20C
			public virtual bool MoveNext()
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
				this._currentElement = null;
				return flag2;
			}

			// Token: 0x170010F4 RID: 4340
			// (get) Token: 0x060062DF RID: 25311 RVA: 0x001510C1 File Offset: 0x0014F2C1
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this._currentElement;
				}
			}

			// Token: 0x060062E0 RID: 25312 RVA: 0x001510F2 File Offset: 0x0014F2F2
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x04003AEA RID: 15082
			private Stack _stack;

			// Token: 0x04003AEB RID: 15083
			private int _index;

			// Token: 0x04003AEC RID: 15084
			private int _version;

			// Token: 0x04003AED RID: 15085
			private object _currentElement;
		}

		// Token: 0x02000A8D RID: 2701
		internal class StackDebugView
		{
			// Token: 0x060062E1 RID: 25313 RVA: 0x00151121 File Offset: 0x0014F321
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this._stack = stack;
			}

			// Token: 0x170010F5 RID: 4341
			// (get) Token: 0x060062E2 RID: 25314 RVA: 0x0015113E File Offset: 0x0014F33E
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._stack.ToArray();
				}
			}

			// Token: 0x04003AEE RID: 15086
			private Stack _stack;
		}
	}
}
