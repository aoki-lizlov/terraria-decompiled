using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B03 RID: 2819
	internal class LowLevelDictionary<TKey, TValue>
	{
		// Token: 0x060067C5 RID: 26565 RVA: 0x0015FC9B File Offset: 0x0015DE9B
		public LowLevelDictionary()
			: this(17, new LowLevelDictionary<TKey, TValue>.DefaultComparer<TKey>())
		{
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x0015FCAA File Offset: 0x0015DEAA
		public LowLevelDictionary(int capacity)
			: this(capacity, new LowLevelDictionary<TKey, TValue>.DefaultComparer<TKey>())
		{
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x0015FCB8 File Offset: 0x0015DEB8
		public LowLevelDictionary(IEqualityComparer<TKey> comparer)
			: this(17, comparer)
		{
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x0015FCC3 File Offset: 0x0015DEC3
		public LowLevelDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer;
			this.Clear(capacity);
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060067C9 RID: 26569 RVA: 0x0015FCD9 File Offset: 0x0015DED9
		public int Count
		{
			get
			{
				return this._numEntries;
			}
		}

		// Token: 0x1700122E RID: 4654
		public TValue this[TKey key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
				if (entry == null)
				{
					throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
				}
				return entry._value;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this._version++;
				LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
				if (entry != null)
				{
					entry._value = value;
					return;
				}
				this.UncheckedAdd(key, value);
			}
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x0015FD7C File Offset: 0x0015DF7C
		public bool TryGetValue(TKey key, out TValue value)
		{
			value = default(TValue);
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			LowLevelDictionary<TKey, TValue>.Entry entry = this.Find(key);
			if (entry != null)
			{
				value = entry._value;
				return true;
			}
			return false;
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x0015FDC0 File Offset: 0x0015DFC0
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.Find(key) != null)
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
			}
			this._version++;
			this.UncheckedAdd(key, value);
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x0015FE16 File Offset: 0x0015E016
		public void Clear(int capacity = 17)
		{
			this._version++;
			this._buckets = new LowLevelDictionary<TKey, TValue>.Entry[capacity];
			this._numEntries = 0;
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x0015FE3C File Offset: 0x0015E03C
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int bucket = this.GetBucket(key, 0);
			LowLevelDictionary<TKey, TValue>.Entry entry = null;
			for (LowLevelDictionary<TKey, TValue>.Entry entry2 = this._buckets[bucket]; entry2 != null; entry2 = entry2._next)
			{
				if (this._comparer.Equals(key, entry2._key))
				{
					if (entry == null)
					{
						this._buckets[bucket] = entry2._next;
					}
					else
					{
						entry._next = entry2._next;
					}
					this._version++;
					this._numEntries--;
					return true;
				}
				entry = entry2;
			}
			return false;
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x0015FED0 File Offset: 0x0015E0D0
		private LowLevelDictionary<TKey, TValue>.Entry Find(TKey key)
		{
			int bucket = this.GetBucket(key, 0);
			for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[bucket]; entry != null; entry = entry._next)
			{
				if (this._comparer.Equals(key, entry._key))
				{
					return entry;
				}
			}
			return null;
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x0015FF14 File Offset: 0x0015E114
		private LowLevelDictionary<TKey, TValue>.Entry UncheckedAdd(TKey key, TValue value)
		{
			LowLevelDictionary<TKey, TValue>.Entry entry = new LowLevelDictionary<TKey, TValue>.Entry();
			entry._key = key;
			entry._value = value;
			int bucket = this.GetBucket(key, 0);
			entry._next = this._buckets[bucket];
			this._buckets[bucket] = entry;
			this._numEntries++;
			if (this._numEntries > this._buckets.Length * 2)
			{
				this.ExpandBuckets();
			}
			return entry;
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x0015FF7C File Offset: 0x0015E17C
		private void ExpandBuckets()
		{
			try
			{
				int num = this._buckets.Length * 2 + 1;
				LowLevelDictionary<TKey, TValue>.Entry[] array = new LowLevelDictionary<TKey, TValue>.Entry[num];
				for (int i = 0; i < this._buckets.Length; i++)
				{
					LowLevelDictionary<TKey, TValue>.Entry next;
					for (LowLevelDictionary<TKey, TValue>.Entry entry = this._buckets[i]; entry != null; entry = next)
					{
						next = entry._next;
						int bucket = this.GetBucket(entry._key, num);
						entry._next = array[bucket];
						array[bucket] = entry;
					}
				}
				this._buckets = array;
			}
			catch (OutOfMemoryException)
			{
			}
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x00160000 File Offset: 0x0015E200
		private int GetBucket(TKey key, int numBuckets = 0)
		{
			return (this._comparer.GetHashCode(key) & int.MaxValue) % ((numBuckets == 0) ? this._buckets.Length : numBuckets);
		}

		// Token: 0x04003C33 RID: 15411
		private const int DefaultSize = 17;

		// Token: 0x04003C34 RID: 15412
		private LowLevelDictionary<TKey, TValue>.Entry[] _buckets;

		// Token: 0x04003C35 RID: 15413
		private int _numEntries;

		// Token: 0x04003C36 RID: 15414
		private int _version;

		// Token: 0x04003C37 RID: 15415
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x02000B04 RID: 2820
		private sealed class Entry
		{
			// Token: 0x060067D4 RID: 26580 RVA: 0x000025BE File Offset: 0x000007BE
			public Entry()
			{
			}

			// Token: 0x04003C38 RID: 15416
			public TKey _key;

			// Token: 0x04003C39 RID: 15417
			public TValue _value;

			// Token: 0x04003C3A RID: 15418
			public LowLevelDictionary<TKey, TValue>.Entry _next;
		}

		// Token: 0x02000B05 RID: 2821
		private sealed class DefaultComparer<T> : IEqualityComparer<T>
		{
			// Token: 0x060067D5 RID: 26581 RVA: 0x00160024 File Offset: 0x0015E224
			public bool Equals(T x, T y)
			{
				if (x == null)
				{
					return y == null;
				}
				IEquatable<T> equatable = x as IEquatable<T>;
				if (equatable != null)
				{
					return equatable.Equals(y);
				}
				return x.Equals(y);
			}

			// Token: 0x060067D6 RID: 26582 RVA: 0x0016006B File Offset: 0x0015E26B
			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x060067D7 RID: 26583 RVA: 0x000025BE File Offset: 0x000007BE
			public DefaultComparer()
			{
			}
		}
	}
}
