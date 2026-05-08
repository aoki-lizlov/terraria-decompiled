using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000AA0 RID: 2720
	[DebuggerTypeProxy(typeof(Hashtable.HashtableDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Hashtable : IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback, ICloneable
	{
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x00155500 File Offset: 0x00153700
		private static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
		{
			get
			{
				return LazyInitializer.EnsureInitialized<ConditionalWeakTable<object, SerializationInfo>>(ref Hashtable.s_serializationInfoTable);
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x0600644D RID: 25677 RVA: 0x0015550C File Offset: 0x0015370C
		// (set) Token: 0x0600644E RID: 25678 RVA: 0x00155540 File Offset: 0x00153740
		[Obsolete("Please use EqualityComparer property.")]
		protected IHashCodeProvider hcp
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).HashCodeProvider;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(value, compatibleComparer.Comparer);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(value, null);
					return;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x0600644F RID: 25679 RVA: 0x00155599 File Offset: 0x00153799
		// (set) Token: 0x06006450 RID: 25680 RVA: 0x001555D0 File Offset: 0x001537D0
		[Obsolete("Please use KeyComparer properties.")]
		protected IComparer comparer
		{
			get
			{
				if (this._keycomparer is CompatibleComparer)
				{
					return ((CompatibleComparer)this._keycomparer).Comparer;
				}
				if (this._keycomparer == null)
				{
					return null;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
			set
			{
				if (this._keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = (CompatibleComparer)this._keycomparer;
					this._keycomparer = new CompatibleComparer(compatibleComparer.HashCodeProvider, value);
					return;
				}
				if (this._keycomparer == null)
				{
					this._keycomparer = new CompatibleComparer(null, value);
					return;
				}
				throw new ArgumentException("The usage of IKeyComparer and IHashCodeProvider/IComparer interfaces cannot be mixed; use one or the other.");
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x00155629 File Offset: 0x00153829
		protected IEqualityComparer EqualityComparer
		{
			get
			{
				return this._keycomparer;
			}
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x000025BE File Offset: 0x000007BE
		internal Hashtable(bool trash)
		{
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x00155631 File Offset: 0x00153831
		public Hashtable()
			: this(0, 1f)
		{
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x0015563F File Offset: 0x0015383F
		public Hashtable(int capacity)
			: this(capacity, 1f)
		{
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x00155650 File Offset: 0x00153850
		public Hashtable(int capacity, float loadFactor)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Non-negative number required.");
			}
			if (loadFactor < 0.1f || loadFactor > 1f)
			{
				throw new ArgumentOutOfRangeException("loadFactor", SR.Format("Load factor needs to be between 0.1 and 1.0.", 0.1, 1.0));
			}
			this._loadFactor = 0.72f * loadFactor;
			double num = (double)((float)capacity / this._loadFactor);
			if (num > 2147483647.0)
			{
				throw new ArgumentException("Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.", "capacity");
			}
			int num2 = ((num > 3.0) ? HashHelpers.GetPrime((int)num) : 3);
			this._buckets = new Hashtable.bucket[num2];
			this._loadsize = (int)(this._loadFactor * (float)num2);
			this._isWriterInProgress = false;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00155728 File Offset: 0x00153928
		public Hashtable(int capacity, float loadFactor, IEqualityComparer equalityComparer)
			: this(capacity, loadFactor)
		{
			this._keycomparer = equalityComparer;
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x00155739 File Offset: 0x00153939
		[Obsolete("Please use Hashtable(IEqualityComparer) instead.")]
		public Hashtable(IHashCodeProvider hcp, IComparer comparer)
			: this(0, 1f, hcp, comparer)
		{
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x00155749 File Offset: 0x00153949
		public Hashtable(IEqualityComparer equalityComparer)
			: this(0, 1f, equalityComparer)
		{
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x00155758 File Offset: 0x00153958
		[Obsolete("Please use Hashtable(int, IEqualityComparer) instead.")]
		public Hashtable(int capacity, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, 1f, hcp, comparer)
		{
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x00155768 File Offset: 0x00153968
		public Hashtable(int capacity, IEqualityComparer equalityComparer)
			: this(capacity, 1f, equalityComparer)
		{
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x00155777 File Offset: 0x00153977
		public Hashtable(IDictionary d)
			: this(d, 1f)
		{
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00155785 File Offset: 0x00153985
		public Hashtable(IDictionary d, float loadFactor)
			: this(d, loadFactor, null)
		{
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x00155790 File Offset: 0x00153990
		[Obsolete("Please use Hashtable(IDictionary, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, IHashCodeProvider hcp, IComparer comparer)
			: this(d, 1f, hcp, comparer)
		{
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x001557A0 File Offset: 0x001539A0
		public Hashtable(IDictionary d, IEqualityComparer equalityComparer)
			: this(d, 1f, equalityComparer)
		{
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x001557AF File Offset: 0x001539AF
		[Obsolete("Please use Hashtable(int, float, IEqualityComparer) instead.")]
		public Hashtable(int capacity, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this(capacity, loadFactor)
		{
			if (hcp != null || comparer != null)
			{
				this._keycomparer = new CompatibleComparer(hcp, comparer);
			}
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x001557D0 File Offset: 0x001539D0
		[Obsolete("Please use Hashtable(IDictionary, float, IEqualityComparer) instead.")]
		public Hashtable(IDictionary d, float loadFactor, IHashCodeProvider hcp, IComparer comparer)
			: this((d != null) ? d.Count : 0, loadFactor, hcp, comparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x0015582C File Offset: 0x00153A2C
		public Hashtable(IDictionary d, float loadFactor, IEqualityComparer equalityComparer)
			: this((d != null) ? d.Count : 0, loadFactor, equalityComparer)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			IDictionaryEnumerator enumerator = d.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.Add(enumerator.Key, enumerator.Value);
			}
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x00155883 File Offset: 0x00153A83
		protected Hashtable(SerializationInfo info, StreamingContext context)
		{
			Hashtable.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x00155898 File Offset: 0x00153A98
		private uint InitHash(object key, int hashsize, out uint seed, out uint incr)
		{
			uint num = (uint)(this.GetHash(key) & int.MaxValue);
			seed = num;
			incr = 1U + seed * 101U % (uint)(hashsize - 1);
			return num;
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x001558C5 File Offset: 0x00153AC5
		public virtual void Add(object key, object value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001558D0 File Offset: 0x00153AD0
		public virtual void Clear()
		{
			if (this._count == 0 && this._occupancy == 0)
			{
				return;
			}
			this._isWriterInProgress = true;
			for (int i = 0; i < this._buckets.Length; i++)
			{
				this._buckets[i].hash_coll = 0;
				this._buckets[i].key = null;
				this._buckets[i].val = null;
			}
			this._count = 0;
			this._occupancy = 0;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00155960 File Offset: 0x00153B60
		public virtual object Clone()
		{
			Hashtable.bucket[] buckets = this._buckets;
			Hashtable hashtable = new Hashtable(this._count, this._keycomparer);
			hashtable._version = this._version;
			hashtable._loadFactor = this._loadFactor;
			hashtable._count = 0;
			int i = buckets.Length;
			while (i > 0)
			{
				i--;
				object key = buckets[i].key;
				if (key != null && key != buckets)
				{
					hashtable[key] = buckets[i].val;
				}
			}
			return hashtable;
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x001559DF File Offset: 0x00153BDF
		public virtual bool Contains(object key)
		{
			return this.ContainsKey(key);
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x001559E8 File Offset: 0x00153BE8
		public virtual bool ContainsKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			Hashtable.bucket[] buckets = this._buckets;
			uint num2;
			uint num3;
			uint num = this.InitHash(key, buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = buckets[num5];
				if (bucket.key == null)
				{
					break;
				}
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					return true;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x00155A88 File Offset: 0x00153C88
		public virtual bool ContainsValue(object value)
		{
			if (value == null)
			{
				int num = this._buckets.Length;
				while (--num >= 0)
				{
					if (this._buckets[num].key != null && this._buckets[num].key != this._buckets && this._buckets[num].val == null)
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this._buckets.Length;
				while (--num2 >= 0)
				{
					object val = this._buckets[num2].val;
					if (val != null && val.Equals(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x00155B24 File Offset: 0x00153D24
		private void CopyKeys(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(key, arrayIndex++);
				}
			}
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00155B6C File Offset: 0x00153D6C
		private void CopyEntries(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					DictionaryEntry dictionaryEntry = new DictionaryEntry(key, buckets[num].val);
					array.SetValue(dictionaryEntry, arrayIndex++);
				}
			}
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x00155BD0 File Offset: 0x00153DD0
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Array cannot be null.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			this.CopyEntries(array, arrayIndex);
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x00155C40 File Offset: 0x00153E40
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this._count];
			int num = 0;
			Hashtable.bucket[] buckets = this._buckets;
			int num2 = buckets.Length;
			while (--num2 >= 0)
			{
				object key = buckets[num2].key;
				if (key != null && key != this._buckets)
				{
					array[num++] = new KeyValuePairs(key, buckets[num2].val);
				}
			}
			return array;
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x00155CA8 File Offset: 0x00153EA8
		private void CopyValues(Array array, int arrayIndex)
		{
			Hashtable.bucket[] buckets = this._buckets;
			int num = buckets.Length;
			while (--num >= 0)
			{
				object key = buckets[num].key;
				if (key != null && key != this._buckets)
				{
					array.SetValue(buckets[num].val, arrayIndex++);
				}
			}
		}

		// Token: 0x17001143 RID: 4419
		public virtual object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				Hashtable.bucket[] buckets = this._buckets;
				uint num2;
				uint num3;
				uint num = this.InitHash(key, buckets.Length, out num2, out num3);
				int num4 = 0;
				int num5 = (int)(num2 % (uint)buckets.Length);
				Hashtable.bucket bucket;
				for (;;)
				{
					SpinWait spinWait = default(SpinWait);
					for (;;)
					{
						int version = this._version;
						bucket = buckets[num5];
						if (!this._isWriterInProgress && version == this._version)
						{
							break;
						}
						spinWait.SpinOnce();
					}
					if (bucket.key == null)
					{
						break;
					}
					if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
					{
						goto Block_5;
					}
					num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)buckets.Length));
					if (bucket.hash_coll >= 0 || ++num4 >= buckets.Length)
					{
						goto IL_00CA;
					}
				}
				return null;
				Block_5:
				return bucket.val;
				IL_00CA:
				return null;
			}
			set
			{
				this.Insert(key, value, false);
			}
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00155DE0 File Offset: 0x00153FE0
		private void expand()
		{
			int num = HashHelpers.ExpandPrime(this._buckets.Length);
			this.rehash(num);
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x00155E02 File Offset: 0x00154002
		private void rehash()
		{
			this.rehash(this._buckets.Length);
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x00155E12 File Offset: 0x00154012
		private void UpdateVersion()
		{
			this._version++;
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x00155E28 File Offset: 0x00154028
		private void rehash(int newsize)
		{
			this._occupancy = 0;
			Hashtable.bucket[] array = new Hashtable.bucket[newsize];
			for (int i = 0; i < this._buckets.Length; i++)
			{
				Hashtable.bucket bucket = this._buckets[i];
				if (bucket.key != null && bucket.key != this._buckets)
				{
					int num = bucket.hash_coll & int.MaxValue;
					this.putEntry(array, bucket.key, bucket.val, num);
				}
			}
			this._isWriterInProgress = true;
			this._buckets = array;
			this._loadsize = (int)(this._loadFactor * (float)newsize);
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x00155EC9 File Offset: 0x001540C9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x00155EC9 File Offset: 0x001540C9
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new Hashtable.HashtableEnumerator(this, 3);
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00155ED2 File Offset: 0x001540D2
		protected virtual int GetHash(object key)
		{
			if (this._keycomparer != null)
			{
				return this._keycomparer.GetHashCode(key);
			}
			return key.GetHashCode();
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x0600647A RID: 25722 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00155EEF File Offset: 0x001540EF
		protected virtual bool KeyEquals(object item, object key)
		{
			if (this._buckets == item)
			{
				return false;
			}
			if (item == key)
			{
				return true;
			}
			if (this._keycomparer != null)
			{
				return this._keycomparer.Equals(item, key);
			}
			return item != null && item.Equals(key);
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x00155F24 File Offset: 0x00154124
		public virtual ICollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Hashtable.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x00155F40 File Offset: 0x00154140
		public virtual ICollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Hashtable.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00155F5C File Offset: 0x0015415C
		private void Insert(object key, object nvalue, bool add)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			if (this._count >= this._loadsize)
			{
				this.expand();
			}
			else if (this._occupancy > this._loadsize && this._count > 100)
			{
				this.rehash();
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = -1;
			int num6 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				if (num5 == -1 && this._buckets[num6].key == this._buckets && this._buckets[num6].hash_coll < 0)
				{
					num5 = num6;
				}
				if (this._buckets[num6].key == null || (this._buckets[num6].key == this._buckets && ((long)this._buckets[num6].hash_coll & (long)((ulong)(-2147483648))) == 0L))
				{
					break;
				}
				if ((long)(this._buckets[num6].hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(this._buckets[num6].key, key))
				{
					goto Block_12;
				}
				if (num5 == -1 && this._buckets[num6].hash_coll >= 0)
				{
					Hashtable.bucket[] buckets = this._buckets;
					int num7 = num6;
					buckets[num7].hash_coll = buckets[num7].hash_coll | int.MinValue;
					this._occupancy++;
				}
				num6 = (int)(((long)num6 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (++num4 >= this._buckets.Length)
				{
					goto Block_16;
				}
			}
			if (num5 != -1)
			{
				num6 = num5;
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this._buckets[num6].key = key;
			Hashtable.bucket[] buckets2 = this._buckets;
			int num8 = num6;
			buckets2[num8].hash_coll = buckets2[num8].hash_coll | (int)num;
			this._count++;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_12:
			if (add)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this._buckets[num6].key, key));
			}
			this._isWriterInProgress = true;
			this._buckets[num6].val = nvalue;
			this.UpdateVersion();
			this._isWriterInProgress = false;
			return;
			Block_16:
			if (num5 != -1)
			{
				this._isWriterInProgress = true;
				this._buckets[num5].val = nvalue;
				this._buckets[num5].key = key;
				Hashtable.bucket[] buckets3 = this._buckets;
				int num9 = num5;
				buckets3[num9].hash_coll = buckets3[num9].hash_coll | (int)num;
				this._count++;
				this.UpdateVersion();
				this._isWriterInProgress = false;
				return;
			}
			throw new InvalidOperationException("Hashtable insert failed. Load factor too high. The most common cause is multiple threads writing to the Hashtable simultaneously.");
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x0015622C File Offset: 0x0015442C
		private void putEntry(Hashtable.bucket[] newBuckets, object key, object nvalue, int hashcode)
		{
			uint num = (uint)(1 + hashcode * 101 % (newBuckets.Length - 1));
			int num2 = hashcode % newBuckets.Length;
			while (newBuckets[num2].key != null && newBuckets[num2].key != this._buckets)
			{
				if (newBuckets[num2].hash_coll >= 0)
				{
					int num3 = num2;
					newBuckets[num3].hash_coll = newBuckets[num3].hash_coll | int.MinValue;
					this._occupancy++;
				}
				num2 = (int)(((long)num2 + (long)((ulong)num)) % (long)((ulong)newBuckets.Length));
			}
			newBuckets[num2].val = nvalue;
			newBuckets[num2].key = key;
			int num4 = num2;
			newBuckets[num4].hash_coll = newBuckets[num4].hash_coll | hashcode;
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x001562E0 File Offset: 0x001544E0
		public virtual void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			uint num2;
			uint num3;
			uint num = this.InitHash(key, this._buckets.Length, out num2, out num3);
			int num4 = 0;
			int num5 = (int)(num2 % (uint)this._buckets.Length);
			for (;;)
			{
				Hashtable.bucket bucket = this._buckets[num5];
				if ((long)(bucket.hash_coll & 2147483647) == (long)((ulong)num) && this.KeyEquals(bucket.key, key))
				{
					break;
				}
				num5 = (int)(((long)num5 + (long)((ulong)num3)) % (long)((ulong)this._buckets.Length));
				if (bucket.hash_coll >= 0 || ++num4 >= this._buckets.Length)
				{
					return;
				}
			}
			this._isWriterInProgress = true;
			Hashtable.bucket[] buckets = this._buckets;
			int num6 = num5;
			buckets[num6].hash_coll = buckets[num6].hash_coll & int.MinValue;
			if (this._buckets[num5].hash_coll != 0)
			{
				this._buckets[num5].key = this._buckets;
			}
			else
			{
				this._buckets[num5].key = null;
			}
			this._buckets[num5].val = null;
			this._count--;
			this.UpdateVersion();
			this._isWriterInProgress = false;
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x0015641E File Offset: 0x0015461E
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

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x00156440 File Offset: 0x00154640
		public virtual int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x00156448 File Offset: 0x00154648
		public static Hashtable Synchronized(Hashtable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			return new Hashtable.SyncHashtable(table);
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x00156460 File Offset: 0x00154660
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				int version = this._version;
				info.AddValue("LoadFactor", this._loadFactor);
				info.AddValue("Version", this._version);
				IEqualityComparer keycomparer = this._keycomparer;
				if (keycomparer == null)
				{
					info.AddValue("Comparer", null, typeof(IComparer));
					info.AddValue("HashCodeProvider", null, typeof(IHashCodeProvider));
				}
				else if (keycomparer is CompatibleComparer)
				{
					CompatibleComparer compatibleComparer = keycomparer as CompatibleComparer;
					info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
					info.AddValue("HashCodeProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				}
				else
				{
					info.AddValue("KeyComparer", keycomparer, typeof(IEqualityComparer));
				}
				info.AddValue("HashSize", this._buckets.Length);
				object[] array = new object[this._count];
				object[] array2 = new object[this._count];
				this.CopyKeys(array, 0);
				this.CopyValues(array2, 0);
				info.AddValue("Keys", array, typeof(object[]));
				info.AddValue("Values", array2, typeof(object[]));
				if (this._version != version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x001565FC File Offset: 0x001547FC
		public virtual void OnDeserialization(object sender)
		{
			if (this._buckets != null)
			{
				return;
			}
			SerializationInfo serializationInfo;
			Hashtable.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			int num = 0;
			IComparer comparer = null;
			IHashCodeProvider hashCodeProvider = null;
			object[] array = null;
			object[] array2 = null;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1613443821U)
				{
					if (num2 != 891156946U)
					{
						if (num2 != 1228509323U)
						{
							if (num2 == 1613443821U)
							{
								if (name == "Keys")
								{
									array = (object[])serializationInfo.GetValue("Keys", typeof(object[]));
								}
							}
						}
						else if (name == "KeyComparer")
						{
							this._keycomparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
						}
					}
					else if (name == "Comparer")
					{
						comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
					}
				}
				else if (num2 <= 2484309429U)
				{
					if (num2 != 2370642523U)
					{
						if (num2 == 2484309429U)
						{
							if (name == "HashCodeProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashCodeProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Values")
					{
						array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
					}
				}
				else if (num2 != 3356145248U)
				{
					if (num2 == 3483216242U)
					{
						if (name == "LoadFactor")
						{
							this._loadFactor = serializationInfo.GetSingle("LoadFactor");
						}
					}
				}
				else if (name == "HashSize")
				{
					num = serializationInfo.GetInt32("HashSize");
				}
			}
			this._loadsize = (int)(this._loadFactor * (float)num);
			if (this._keycomparer == null && (comparer != null || hashCodeProvider != null))
			{
				this._keycomparer = new CompatibleComparer(hashCodeProvider, comparer);
			}
			this._buckets = new Hashtable.bucket[num];
			if (array == null)
			{
				throw new SerializationException("The keys for this dictionary are missing.");
			}
			if (array2 == null)
			{
				throw new SerializationException("The values for this dictionary are missing.");
			}
			if (array.Length != array2.Length)
			{
				throw new SerializationException("The keys and values arrays have different sizes.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					throw new SerializationException("One of the serialized keys is null.");
				}
				this.Insert(array[i], array2[i], true);
			}
			this._version = serializationInfo.GetInt32("Version");
			Hashtable.SerializationInfoTable.Remove(this);
		}

		// Token: 0x04003B22 RID: 15138
		internal const int HashPrime = 101;

		// Token: 0x04003B23 RID: 15139
		private const int InitialSize = 3;

		// Token: 0x04003B24 RID: 15140
		private const string LoadFactorName = "LoadFactor";

		// Token: 0x04003B25 RID: 15141
		private const string VersionName = "Version";

		// Token: 0x04003B26 RID: 15142
		private const string ComparerName = "Comparer";

		// Token: 0x04003B27 RID: 15143
		private const string HashCodeProviderName = "HashCodeProvider";

		// Token: 0x04003B28 RID: 15144
		private const string HashSizeName = "HashSize";

		// Token: 0x04003B29 RID: 15145
		private const string KeysName = "Keys";

		// Token: 0x04003B2A RID: 15146
		private const string ValuesName = "Values";

		// Token: 0x04003B2B RID: 15147
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04003B2C RID: 15148
		private Hashtable.bucket[] _buckets;

		// Token: 0x04003B2D RID: 15149
		private int _count;

		// Token: 0x04003B2E RID: 15150
		private int _occupancy;

		// Token: 0x04003B2F RID: 15151
		private int _loadsize;

		// Token: 0x04003B30 RID: 15152
		private float _loadFactor;

		// Token: 0x04003B31 RID: 15153
		private volatile int _version;

		// Token: 0x04003B32 RID: 15154
		private volatile bool _isWriterInProgress;

		// Token: 0x04003B33 RID: 15155
		private ICollection _keys;

		// Token: 0x04003B34 RID: 15156
		private ICollection _values;

		// Token: 0x04003B35 RID: 15157
		private IEqualityComparer _keycomparer;

		// Token: 0x04003B36 RID: 15158
		private object _syncRoot;

		// Token: 0x04003B37 RID: 15159
		private static ConditionalWeakTable<object, SerializationInfo> s_serializationInfoTable;

		// Token: 0x02000AA1 RID: 2721
		private struct bucket
		{
			// Token: 0x04003B38 RID: 15160
			public object key;

			// Token: 0x04003B39 RID: 15161
			public object val;

			// Token: 0x04003B3A RID: 15162
			public int hash_coll;
		}

		// Token: 0x02000AA2 RID: 2722
		[Serializable]
		private class KeyCollection : ICollection, IEnumerable
		{
			// Token: 0x06006486 RID: 25734 RVA: 0x001568E2 File Offset: 0x00154AE2
			internal KeyCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x06006487 RID: 25735 RVA: 0x001568F4 File Offset: 0x00154AF4
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyKeys(array, arrayIndex);
			}

			// Token: 0x06006488 RID: 25736 RVA: 0x00156969 File Offset: 0x00154B69
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 1);
			}

			// Token: 0x1700114B RID: 4427
			// (get) Token: 0x06006489 RID: 25737 RVA: 0x00156977 File Offset: 0x00154B77
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x1700114C RID: 4428
			// (get) Token: 0x0600648A RID: 25738 RVA: 0x00156984 File Offset: 0x00154B84
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x1700114D RID: 4429
			// (get) Token: 0x0600648B RID: 25739 RVA: 0x00156991 File Offset: 0x00154B91
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04003B3B RID: 15163
			private Hashtable _hashtable;
		}

		// Token: 0x02000AA3 RID: 2723
		[Serializable]
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x0600648C RID: 25740 RVA: 0x0015699E File Offset: 0x00154B9E
			internal ValueCollection(Hashtable hashtable)
			{
				this._hashtable = hashtable;
			}

			// Token: 0x0600648D RID: 25741 RVA: 0x001569B0 File Offset: 0x00154BB0
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", "Non-negative number required.");
				}
				if (array.Length - arrayIndex < this._hashtable._count)
				{
					throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
				}
				this._hashtable.CopyValues(array, arrayIndex);
			}

			// Token: 0x0600648E RID: 25742 RVA: 0x00156A25 File Offset: 0x00154C25
			public virtual IEnumerator GetEnumerator()
			{
				return new Hashtable.HashtableEnumerator(this._hashtable, 2);
			}

			// Token: 0x1700114E RID: 4430
			// (get) Token: 0x0600648F RID: 25743 RVA: 0x00156A33 File Offset: 0x00154C33
			public virtual bool IsSynchronized
			{
				get
				{
					return this._hashtable.IsSynchronized;
				}
			}

			// Token: 0x1700114F RID: 4431
			// (get) Token: 0x06006490 RID: 25744 RVA: 0x00156A40 File Offset: 0x00154C40
			public virtual object SyncRoot
			{
				get
				{
					return this._hashtable.SyncRoot;
				}
			}

			// Token: 0x17001150 RID: 4432
			// (get) Token: 0x06006491 RID: 25745 RVA: 0x00156A4D File Offset: 0x00154C4D
			public virtual int Count
			{
				get
				{
					return this._hashtable._count;
				}
			}

			// Token: 0x04003B3C RID: 15164
			private Hashtable _hashtable;
		}

		// Token: 0x02000AA4 RID: 2724
		[Serializable]
		private class SyncHashtable : Hashtable, IEnumerable
		{
			// Token: 0x06006492 RID: 25746 RVA: 0x00156A5A File Offset: 0x00154C5A
			internal SyncHashtable(Hashtable table)
				: base(false)
			{
				this._table = table;
			}

			// Token: 0x06006493 RID: 25747 RVA: 0x00156A6A File Offset: 0x00154C6A
			internal SyncHashtable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06006494 RID: 25748 RVA: 0x0003CB93 File Offset: 0x0003AD93
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x17001151 RID: 4433
			// (get) Token: 0x06006495 RID: 25749 RVA: 0x00156A79 File Offset: 0x00154C79
			public override int Count
			{
				get
				{
					return this._table.Count;
				}
			}

			// Token: 0x17001152 RID: 4434
			// (get) Token: 0x06006496 RID: 25750 RVA: 0x00156A86 File Offset: 0x00154C86
			public override bool IsReadOnly
			{
				get
				{
					return this._table.IsReadOnly;
				}
			}

			// Token: 0x17001153 RID: 4435
			// (get) Token: 0x06006497 RID: 25751 RVA: 0x00156A93 File Offset: 0x00154C93
			public override bool IsFixedSize
			{
				get
				{
					return this._table.IsFixedSize;
				}
			}

			// Token: 0x17001154 RID: 4436
			// (get) Token: 0x06006498 RID: 25752 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001155 RID: 4437
			public override object this[object key]
			{
				get
				{
					return this._table[key];
				}
				set
				{
					object syncRoot = this._table.SyncRoot;
					lock (syncRoot)
					{
						this._table[key] = value;
					}
				}
			}

			// Token: 0x17001156 RID: 4438
			// (get) Token: 0x0600649B RID: 25755 RVA: 0x00156AFC File Offset: 0x00154CFC
			public override object SyncRoot
			{
				get
				{
					return this._table.SyncRoot;
				}
			}

			// Token: 0x0600649C RID: 25756 RVA: 0x00156B0C File Offset: 0x00154D0C
			public override void Add(object key, object value)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Add(key, value);
				}
			}

			// Token: 0x0600649D RID: 25757 RVA: 0x00156B58 File Offset: 0x00154D58
			public override void Clear()
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Clear();
				}
			}

			// Token: 0x0600649E RID: 25758 RVA: 0x00156BA4 File Offset: 0x00154DA4
			public override bool Contains(object key)
			{
				return this._table.Contains(key);
			}

			// Token: 0x0600649F RID: 25759 RVA: 0x00156BB2 File Offset: 0x00154DB2
			public override bool ContainsKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				return this._table.ContainsKey(key);
			}

			// Token: 0x060064A0 RID: 25760 RVA: 0x00156BD4 File Offset: 0x00154DD4
			public override bool ContainsValue(object key)
			{
				object syncRoot = this._table.SyncRoot;
				bool flag2;
				lock (syncRoot)
				{
					flag2 = this._table.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x060064A1 RID: 25761 RVA: 0x00156C24 File Offset: 0x00154E24
			public override void CopyTo(Array array, int arrayIndex)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x060064A2 RID: 25762 RVA: 0x00156C70 File Offset: 0x00154E70
			public override object Clone()
			{
				object syncRoot = this._table.SyncRoot;
				object obj;
				lock (syncRoot)
				{
					obj = Hashtable.Synchronized((Hashtable)this._table.Clone());
				}
				return obj;
			}

			// Token: 0x060064A3 RID: 25763 RVA: 0x00156CC8 File Offset: 0x00154EC8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x060064A4 RID: 25764 RVA: 0x00156CC8 File Offset: 0x00154EC8
			public override IDictionaryEnumerator GetEnumerator()
			{
				return this._table.GetEnumerator();
			}

			// Token: 0x17001157 RID: 4439
			// (get) Token: 0x060064A5 RID: 25765 RVA: 0x00156CD8 File Offset: 0x00154ED8
			public override ICollection Keys
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection keys;
					lock (syncRoot)
					{
						keys = this._table.Keys;
					}
					return keys;
				}
			}

			// Token: 0x17001158 RID: 4440
			// (get) Token: 0x060064A6 RID: 25766 RVA: 0x00156D24 File Offset: 0x00154F24
			public override ICollection Values
			{
				get
				{
					object syncRoot = this._table.SyncRoot;
					ICollection values;
					lock (syncRoot)
					{
						values = this._table.Values;
					}
					return values;
				}
			}

			// Token: 0x060064A7 RID: 25767 RVA: 0x00156D70 File Offset: 0x00154F70
			public override void Remove(object key)
			{
				object syncRoot = this._table.SyncRoot;
				lock (syncRoot)
				{
					this._table.Remove(key);
				}
			}

			// Token: 0x060064A8 RID: 25768 RVA: 0x00004088 File Offset: 0x00002288
			public override void OnDeserialization(object sender)
			{
			}

			// Token: 0x060064A9 RID: 25769 RVA: 0x00156DBC File Offset: 0x00154FBC
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._table.ToKeyValuePairsArray();
			}

			// Token: 0x04003B3D RID: 15165
			protected Hashtable _table;
		}

		// Token: 0x02000AA5 RID: 2725
		[Serializable]
		private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x060064AA RID: 25770 RVA: 0x00156DC9 File Offset: 0x00154FC9
			internal HashtableEnumerator(Hashtable hashtable, int getObjRetType)
			{
				this._hashtable = hashtable;
				this._bucket = hashtable._buckets.Length;
				this._version = hashtable._version;
				this._current = false;
				this._getObjectRetType = getObjRetType;
			}

			// Token: 0x060064AB RID: 25771 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x17001159 RID: 4441
			// (get) Token: 0x060064AC RID: 25772 RVA: 0x00156E02 File Offset: 0x00155002
			public virtual object Key
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					return this._currentKey;
				}
			}

			// Token: 0x060064AD RID: 25773 RVA: 0x00156E20 File Offset: 0x00155020
			public virtual bool MoveNext()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._bucket > 0)
				{
					this._bucket--;
					object key = this._hashtable._buckets[this._bucket].key;
					if (key != null && key != this._hashtable._buckets)
					{
						this._currentKey = key;
						this._currentValue = this._hashtable._buckets[this._bucket].val;
						this._current = true;
						return true;
					}
				}
				this._current = false;
				return false;
			}

			// Token: 0x1700115A RID: 4442
			// (get) Token: 0x060064AE RID: 25774 RVA: 0x00156ECA File Offset: 0x001550CA
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x1700115B RID: 4443
			// (get) Token: 0x060064AF RID: 25775 RVA: 0x00156EF0 File Offset: 0x001550F0
			public virtual object Current
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					if (this._getObjectRetType == 1)
					{
						return this._currentKey;
					}
					if (this._getObjectRetType == 2)
					{
						return this._currentValue;
					}
					return new DictionaryEntry(this._currentKey, this._currentValue);
				}
			}

			// Token: 0x1700115C RID: 4444
			// (get) Token: 0x060064B0 RID: 25776 RVA: 0x00156F46 File Offset: 0x00155146
			public virtual object Value
			{
				get
				{
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._currentValue;
				}
			}

			// Token: 0x060064B1 RID: 25777 RVA: 0x00156F64 File Offset: 0x00155164
			public virtual void Reset()
			{
				if (this._version != this._hashtable._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._current = false;
				this._bucket = this._hashtable._buckets.Length;
				this._currentKey = null;
				this._currentValue = null;
			}

			// Token: 0x04003B3E RID: 15166
			private Hashtable _hashtable;

			// Token: 0x04003B3F RID: 15167
			private int _bucket;

			// Token: 0x04003B40 RID: 15168
			private int _version;

			// Token: 0x04003B41 RID: 15169
			private bool _current;

			// Token: 0x04003B42 RID: 15170
			private int _getObjectRetType;

			// Token: 0x04003B43 RID: 15171
			private object _currentKey;

			// Token: 0x04003B44 RID: 15172
			private object _currentValue;

			// Token: 0x04003B45 RID: 15173
			internal const int Keys = 1;

			// Token: 0x04003B46 RID: 15174
			internal const int Values = 2;

			// Token: 0x04003B47 RID: 15175
			internal const int DictEntry = 3;
		}

		// Token: 0x02000AA6 RID: 2726
		internal class HashtableDebugView
		{
			// Token: 0x060064B2 RID: 25778 RVA: 0x00156FB9 File Offset: 0x001551B9
			public HashtableDebugView(Hashtable hashtable)
			{
				if (hashtable == null)
				{
					throw new ArgumentNullException("hashtable");
				}
				this._hashtable = hashtable;
			}

			// Token: 0x1700115D RID: 4445
			// (get) Token: 0x060064B3 RID: 25779 RVA: 0x00156FD6 File Offset: 0x001551D6
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this._hashtable.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04003B48 RID: 15176
			private Hashtable _hashtable;
		}
	}
}
