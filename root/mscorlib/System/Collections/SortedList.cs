using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A84 RID: 2692
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600624E RID: 25166 RVA: 0x0014F69A File Offset: 0x0014D89A
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0014F6A8 File Offset: 0x0014D8A8
		private void Init()
		{
			this.keys = Array.Empty<object>();
			this.values = Array.Empty<object>();
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0014F6D8 File Offset: 0x0014D8D8
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0014F727 File Offset: 0x0014D927
		public SortedList(IComparer comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x0014F739 File Offset: 0x0014D939
		public SortedList(IComparer comparer, int capacity)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x0014F749 File Offset: 0x0014D949
		public SortedList(IDictionary d)
			: this(d, null)
		{
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x0014F754 File Offset: 0x0014D954
		public SortedList(IDictionary d, IComparer comparer)
			: this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", "Dictionary cannot be null.");
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, comparer);
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.values[i] = d[this.keys[i]];
			}
			this._size = d.Count;
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x0014F7EC File Offset: 0x0014D9EC
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", this.GetKey(num), key));
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x0014F84C File Offset: 0x0014DA4C
		// (set) Token: 0x06006257 RID: 25175 RVA: 0x0014F858 File Offset: 0x0014DA58
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						object[] array2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = Array.Empty<object>();
					this.values = Array.Empty<object>();
				}
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x0014F8F1 File Offset: 0x0014DAF1
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06006259 RID: 25177 RVA: 0x0014F8F9 File Offset: 0x0014DAF9
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x0014F901 File Offset: 0x0014DB01
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600625B RID: 25179 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x0600625D RID: 25181 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x0600625E RID: 25182 RVA: 0x0014F909 File Offset: 0x0014DB09
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

		// Token: 0x0600625F RID: 25183 RVA: 0x0014F92B File Offset: 0x0014DB2B
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0014F968 File Offset: 0x0014DB68
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x0014F9D8 File Offset: 0x0014DBD8
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x0014F9D8 File Offset: 0x0014DBD8
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x0014F9E7 File Offset: 0x0014DBE7
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x0014F9F8 File Offset: 0x0014DBF8
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
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x0014FA98 File Offset: 0x0014DC98
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x0014FADC File Offset: 0x0014DCDC
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 16 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x0014FB1C File Offset: 0x0014DD1C
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.values[index];
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x0014FB43 File Offset: 0x0014DD43
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0014FB43 File Offset: 0x0014DD43
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x0014FB53 File Offset: 0x0014DD53
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return this.keys[index];
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x0014FB7A File Offset: 0x0014DD7A
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x0014FB96 File Offset: 0x0014DD96
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x170010D5 RID: 4309
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x0014FC38 File Offset: 0x0014DE38
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x0014FC79 File Offset: 0x0014DE79
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x0014FC90 File Offset: 0x0014DE90
		private void Insert(int index, object key, object value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0014FD2C File Offset: 0x0014DF2C
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x0014FDD4 File Offset: 0x0014DFD4
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0014FDF4 File Offset: 0x0014DFF4
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this.values[index] = value;
			this.version++;
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0014FE2A File Offset: 0x0014E02A
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x0014FE40 File Offset: 0x0014E040
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x04003AC8 RID: 15048
		private object[] keys;

		// Token: 0x04003AC9 RID: 15049
		private object[] values;

		// Token: 0x04003ACA RID: 15050
		private int _size;

		// Token: 0x04003ACB RID: 15051
		private int version;

		// Token: 0x04003ACC RID: 15052
		private IComparer comparer;

		// Token: 0x04003ACD RID: 15053
		private SortedList.KeyList keyList;

		// Token: 0x04003ACE RID: 15054
		private SortedList.ValueList valueList;

		// Token: 0x04003ACF RID: 15055
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003AD0 RID: 15056
		private const int _defaultCapacity = 16;

		// Token: 0x04003AD1 RID: 15057
		internal const int MaxArrayLength = 2146435071;

		// Token: 0x02000A85 RID: 2693
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06006277 RID: 25207 RVA: 0x0014FE4E File Offset: 0x0014E04E
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x170010D6 RID: 4310
			// (get) Token: 0x06006278 RID: 25208 RVA: 0x0014FE6C File Offset: 0x0014E06C
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x170010D7 RID: 4311
			// (get) Token: 0x06006279 RID: 25209 RVA: 0x0014FEB4 File Offset: 0x0014E0B4
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170010D8 RID: 4312
			// (get) Token: 0x0600627A RID: 25210 RVA: 0x0014FEBC File Offset: 0x0014E0BC
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170010D9 RID: 4313
			// (get) Token: 0x0600627B RID: 25211 RVA: 0x0014FEC9 File Offset: 0x0014E0C9
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170010DA RID: 4314
			// (get) Token: 0x0600627C RID: 25212 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010DB RID: 4315
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[key];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x0600627F RID: 25215 RVA: 0x0014FF68 File Offset: 0x0014E168
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x170010DC RID: 4316
			// (get) Token: 0x06006280 RID: 25216 RVA: 0x0014FFB0 File Offset: 0x0014E1B0
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06006281 RID: 25217 RVA: 0x0014FFF8 File Offset: 0x0014E1F8
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006282 RID: 25218 RVA: 0x00150040 File Offset: 0x0014E240
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._list.Clone();
				}
				return obj;
			}

			// Token: 0x06006283 RID: 25219 RVA: 0x00150088 File Offset: 0x0014E288
			public override bool Contains(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(key);
				}
				return flag2;
			}

			// Token: 0x06006284 RID: 25220 RVA: 0x001500D0 File Offset: 0x0014E2D0
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsKey(key);
				}
				return flag2;
			}

			// Token: 0x06006285 RID: 25221 RVA: 0x00150118 File Offset: 0x0014E318
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x06006286 RID: 25222 RVA: 0x00150160 File Offset: 0x0014E360
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006287 RID: 25223 RVA: 0x001501A8 File Offset: 0x0014E3A8
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06006288 RID: 25224 RVA: 0x001501F0 File Offset: 0x0014E3F0
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006289 RID: 25225 RVA: 0x00150238 File Offset: 0x0014E438
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x0600628A RID: 25226 RVA: 0x00150280 File Offset: 0x0014E480
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x0600628B RID: 25227 RVA: 0x001502C8 File Offset: 0x0014E4C8
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x0600628C RID: 25228 RVA: 0x00150310 File Offset: 0x0014E510
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfKey(key);
				}
				return num;
			}

			// Token: 0x0600628D RID: 25229 RVA: 0x0015036C File Offset: 0x0014E56C
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfValue(value);
				}
				return num;
			}

			// Token: 0x0600628E RID: 25230 RVA: 0x001503B4 File Offset: 0x0014E5B4
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x0600628F RID: 25231 RVA: 0x001503FC File Offset: 0x0014E5FC
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06006290 RID: 25232 RVA: 0x00150444 File Offset: 0x0014E644
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06006291 RID: 25233 RVA: 0x0015048C File Offset: 0x0014E68C
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06006292 RID: 25234 RVA: 0x0015049C File Offset: 0x0014E69C
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x04003AD2 RID: 15058
			private SortedList _list;

			// Token: 0x04003AD3 RID: 15059
			private object _root;
		}

		// Token: 0x02000A86 RID: 2694
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06006293 RID: 25235 RVA: 0x001504E4 File Offset: 0x0014E6E4
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this._sortedList = sortedList;
				this._index = index;
				this._startIndex = index;
				this._endIndex = index + count;
				this._version = sortedList.version;
				this._getObjectRetType = getObjRetType;
				this._current = false;
			}

			// Token: 0x06006294 RID: 25236 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170010DD RID: 4317
			// (get) Token: 0x06006295 RID: 25237 RVA: 0x00150530 File Offset: 0x0014E730
			public virtual object Key
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._key;
				}
			}

			// Token: 0x06006296 RID: 25238 RVA: 0x0015056C File Offset: 0x0014E76C
			public virtual bool MoveNext()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index < this._endIndex)
				{
					this._key = this._sortedList.keys[this._index];
					this._value = this._sortedList.values[this._index];
					this._index++;
					this._current = true;
					return true;
				}
				this._key = null;
				this._value = null;
				this._current = false;
				return false;
			}

			// Token: 0x170010DE RID: 4318
			// (get) Token: 0x06006297 RID: 25239 RVA: 0x00150604 File Offset: 0x0014E804
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x170010DF RID: 4319
			// (get) Token: 0x06006298 RID: 25240 RVA: 0x00150654 File Offset: 0x0014E854
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
						return this._key;
					}
					if (this._getObjectRetType == 2)
					{
						return this._value;
					}
					return new DictionaryEntry(this._key, this._value);
				}
			}

			// Token: 0x170010E0 RID: 4320
			// (get) Token: 0x06006299 RID: 25241 RVA: 0x001506AA File Offset: 0x0014E8AA
			public virtual object Value
			{
				get
				{
					if (this._version != this._sortedList.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (!this._current)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._value;
				}
			}

			// Token: 0x0600629A RID: 25242 RVA: 0x001506E4 File Offset: 0x0014E8E4
			public virtual void Reset()
			{
				if (this._version != this._sortedList.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = this._startIndex;
				this._current = false;
				this._key = null;
				this._value = null;
			}

			// Token: 0x04003AD4 RID: 15060
			private SortedList _sortedList;

			// Token: 0x04003AD5 RID: 15061
			private object _key;

			// Token: 0x04003AD6 RID: 15062
			private object _value;

			// Token: 0x04003AD7 RID: 15063
			private int _index;

			// Token: 0x04003AD8 RID: 15064
			private int _startIndex;

			// Token: 0x04003AD9 RID: 15065
			private int _endIndex;

			// Token: 0x04003ADA RID: 15066
			private int _version;

			// Token: 0x04003ADB RID: 15067
			private bool _current;

			// Token: 0x04003ADC RID: 15068
			private int _getObjectRetType;

			// Token: 0x04003ADD RID: 15069
			internal const int Keys = 1;

			// Token: 0x04003ADE RID: 15070
			internal const int Values = 2;

			// Token: 0x04003ADF RID: 15071
			internal const int DictEntry = 3;
		}

		// Token: 0x02000A87 RID: 2695
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x0600629B RID: 25243 RVA: 0x00150730 File Offset: 0x0014E930
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170010E1 RID: 4321
			// (get) Token: 0x0600629C RID: 25244 RVA: 0x0015073F File Offset: 0x0014E93F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170010E2 RID: 4322
			// (get) Token: 0x0600629D RID: 25245 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010E3 RID: 4323
			// (get) Token: 0x0600629E RID: 25246 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010E4 RID: 4324
			// (get) Token: 0x0600629F RID: 25247 RVA: 0x0015074C File Offset: 0x0014E94C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170010E5 RID: 4325
			// (get) Token: 0x060062A0 RID: 25248 RVA: 0x00150759 File Offset: 0x0014E959
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x060062A1 RID: 25249 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062A2 RID: 25250 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062A3 RID: 25251 RVA: 0x00150772 File Offset: 0x0014E972
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x060062A4 RID: 25252 RVA: 0x00150780 File Offset: 0x0014E980
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x060062A5 RID: 25253 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x170010E6 RID: 4326
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Mutating a key collection derived from a dictionary is not allowed.");
				}
			}

			// Token: 0x060062A8 RID: 25256 RVA: 0x001507D6 File Offset: 0x0014E9D6
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x060062A9 RID: 25257 RVA: 0x001507F0 File Offset: 0x0014E9F0
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x060062AA RID: 25258 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Remove(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062AB RID: 25259 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04003AE0 RID: 15072
			private SortedList sortedList;
		}

		// Token: 0x02000A88 RID: 2696
		[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x060062AC RID: 25260 RVA: 0x00150840 File Offset: 0x0014EA40
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170010E7 RID: 4327
			// (get) Token: 0x060062AD RID: 25261 RVA: 0x0015084F File Offset: 0x0014EA4F
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170010E8 RID: 4328
			// (get) Token: 0x060062AE RID: 25262 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010E9 RID: 4329
			// (get) Token: 0x060062AF RID: 25263 RVA: 0x00003FB7 File Offset: 0x000021B7
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170010EA RID: 4330
			// (get) Token: 0x060062B0 RID: 25264 RVA: 0x0015085C File Offset: 0x0014EA5C
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170010EB RID: 4331
			// (get) Token: 0x060062B1 RID: 25265 RVA: 0x00150869 File Offset: 0x0014EA69
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x060062B2 RID: 25266 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual int Add(object key)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062B3 RID: 25267 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Clear()
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062B4 RID: 25268 RVA: 0x00150876 File Offset: 0x0014EA76
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x060062B5 RID: 25269 RVA: 0x00150884 File Offset: 0x0014EA84
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x060062B6 RID: 25270 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x170010EC RID: 4332
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
				}
			}

			// Token: 0x060062B9 RID: 25273 RVA: 0x001508CE File Offset: 0x0014EACE
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x060062BA RID: 25274 RVA: 0x001508E8 File Offset: 0x0014EAE8
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x060062BB RID: 25275 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void Remove(object value)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x060062BC RID: 25276 RVA: 0x00150766 File Offset: 0x0014E966
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("This operation is not supported on SortedList nested types because they require modifying the original SortedList.");
			}

			// Token: 0x04003AE1 RID: 15073
			private SortedList sortedList;
		}

		// Token: 0x02000A89 RID: 2697
		internal class SortedListDebugView
		{
			// Token: 0x060062BD RID: 25277 RVA: 0x00150907 File Offset: 0x0014EB07
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this._sortedList = sortedList;
			}

			// Token: 0x170010ED RID: 4333
			// (get) Token: 0x060062BE RID: 25278 RVA: 0x00150924 File Offset: 0x0014EB24
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this._sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x04003AE2 RID: 15074
			private SortedList _sortedList;
		}
	}
}
