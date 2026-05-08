using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A74 RID: 2676
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060061AE RID: 25006 RVA: 0x000025BE File Offset: 0x000007BE
		public ListDictionaryInternal()
		{
		}

		// Token: 0x17001098 RID: 4248
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x0014DDC3 File Offset: 0x0014BFC3
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x0014DDCB File Offset: 0x0014BFCB
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060061B3 RID: 25011 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060061B5 RID: 25013 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x0014DDD4 File Offset: 0x0014BFD4
		public object SyncRoot
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

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060061B7 RID: 25015 RVA: 0x0014DDF6 File Offset: 0x0014BFF6
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x0014DE00 File Offset: 0x0014C000
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", next.key, key));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x0014DEAA File Offset: 0x0014C0AA
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x0014DEC8 File Offset: 0x0014C0C8
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x0014DF0C File Offset: 0x0014C10C
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x0014DFA4 File Offset: 0x0014C1A4
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0014DFA4 File Offset: 0x0014C1A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x0014DFAC File Offset: 0x0014C1AC
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04003A9B RID: 15003
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x04003A9C RID: 15004
		private int version;

		// Token: 0x04003A9D RID: 15005
		private int count;

		// Token: 0x04003A9E RID: 15006
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000A75 RID: 2677
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060061BF RID: 25023 RVA: 0x0014E034 File Offset: 0x0014C234
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x170010A0 RID: 4256
			// (get) Token: 0x060061C0 RID: 25024 RVA: 0x0014E05D File Offset: 0x0014C25D
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170010A1 RID: 4257
			// (get) Token: 0x060061C1 RID: 25025 RVA: 0x0014E06A File Offset: 0x0014C26A
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x170010A2 RID: 4258
			// (get) Token: 0x060061C2 RID: 25026 RVA: 0x0014E09A File Offset: 0x0014C29A
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.key;
				}
			}

			// Token: 0x170010A3 RID: 4259
			// (get) Token: 0x060061C3 RID: 25027 RVA: 0x0014E0BA File Offset: 0x0014C2BA
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.value;
				}
			}

			// Token: 0x060061C4 RID: 25028 RVA: 0x0014E0DC File Offset: 0x0014C2DC
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x060061C5 RID: 25029 RVA: 0x0014E14B File Offset: 0x0014C34B
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04003A9F RID: 15007
			private ListDictionaryInternal list;

			// Token: 0x04003AA0 RID: 15008
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04003AA1 RID: 15009
			private int version;

			// Token: 0x04003AA2 RID: 15010
			private bool start;
		}

		// Token: 0x02000A76 RID: 2678
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060061C6 RID: 25030 RVA: 0x0014E179 File Offset: 0x0014C379
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x060061C7 RID: 25031 RVA: 0x0014E190 File Offset: 0x0014C390
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
				}
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170010A4 RID: 4260
			// (get) Token: 0x060061C8 RID: 25032 RVA: 0x0014E234 File Offset: 0x0014C434
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170010A5 RID: 4261
			// (get) Token: 0x060061C9 RID: 25033 RVA: 0x0000408A File Offset: 0x0000228A
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170010A6 RID: 4262
			// (get) Token: 0x060061CA RID: 25034 RVA: 0x0014E260 File Offset: 0x0014C460
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x060061CB RID: 25035 RVA: 0x0014E26D File Offset: 0x0014C46D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04003AA3 RID: 15011
			private ListDictionaryInternal list;

			// Token: 0x04003AA4 RID: 15012
			private bool isKeys;

			// Token: 0x02000A77 RID: 2679
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x060061CC RID: 25036 RVA: 0x0014E280 File Offset: 0x0014C480
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x170010A7 RID: 4263
				// (get) Token: 0x060061CD RID: 25037 RVA: 0x0014E2B0 File Offset: 0x0014C4B0
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x060061CE RID: 25038 RVA: 0x0014E2E4 File Offset: 0x0014C4E4
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x060061CF RID: 25039 RVA: 0x0014E353 File Offset: 0x0014C553
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003AA5 RID: 15013
				private ListDictionaryInternal list;

				// Token: 0x04003AA6 RID: 15014
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x04003AA7 RID: 15015
				private int version;

				// Token: 0x04003AA8 RID: 15016
				private bool isKeys;

				// Token: 0x04003AA9 RID: 15017
				private bool start;
			}
		}

		// Token: 0x02000A78 RID: 2680
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x060061D0 RID: 25040 RVA: 0x000025BE File Offset: 0x000007BE
			public DictionaryNode()
			{
			}

			// Token: 0x04003AAA RID: 15018
			public object key;

			// Token: 0x04003AAB RID: 15019
			public object value;

			// Token: 0x04003AAC RID: 15020
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
