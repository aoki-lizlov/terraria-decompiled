using System;

namespace System.Collections
{
	// Token: 0x02000AA8 RID: 2728
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x060064B5 RID: 25781 RVA: 0x000025BE File Offset: 0x000007BE
		public EmptyReadOnlyDictionaryInternal()
		{
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x00156FE3 File Offset: 0x001551E3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x00156FEC File Offset: 0x001551EC
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Only single dimensional arrays are supported for the requested action."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."), "index");
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x0000408A File Offset: 0x0000228A
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060064B9 RID: 25785 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001161 RID: 4449
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
				}
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060064BD RID: 25789 RVA: 0x001570F7 File Offset: 0x001552F7
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x001570F7 File Offset: 0x001552F7
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x0000408A File Offset: 0x0000228A
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x00157100 File Offset: 0x00155300
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("Key cannot be null."));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument passed in is not serializable."), "value");
			}
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x0015717B File Offset: 0x0015537B
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x00156FE3 File Offset: 0x001551E3
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x0015717B File Offset: 0x0015537B
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
		}

		// Token: 0x02000AA9 RID: 2729
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060064C6 RID: 25798 RVA: 0x000025BE File Offset: 0x000007BE
			public NodeEnumerator()
			{
			}

			// Token: 0x060064C7 RID: 25799 RVA: 0x0000408A File Offset: 0x0000228A
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x17001166 RID: 4454
			// (get) Token: 0x060064C8 RID: 25800 RVA: 0x0015718C File Offset: 0x0015538C
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x060064C9 RID: 25801 RVA: 0x00004088 File Offset: 0x00002288
			public void Reset()
			{
			}

			// Token: 0x17001167 RID: 4455
			// (get) Token: 0x060064CA RID: 25802 RVA: 0x0015718C File Offset: 0x0015538C
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x17001168 RID: 4456
			// (get) Token: 0x060064CB RID: 25803 RVA: 0x0015718C File Offset: 0x0015538C
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}

			// Token: 0x17001169 RID: 4457
			// (get) Token: 0x060064CC RID: 25804 RVA: 0x0015718C File Offset: 0x0015538C
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has either not started or has already finished."));
				}
			}
		}
	}
}
