using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200081D RID: 2077
	public sealed class ConditionalWeakTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class where TValue : class
	{
		// Token: 0x06004660 RID: 18016 RVA: 0x000E6C21 File Offset: 0x000E4E21
		public ConditionalWeakTable()
		{
			this.data = new Ephemeron[13];
			GC.register_ephemeron_array(this.data);
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000E6C4C File Offset: 0x000E4E4C
		~ConditionalWeakTable()
		{
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000E6C74 File Offset: 0x000E4E74
		private void RehashWithoutResize()
		{
			int num = this.data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.data[i].key == GC.EPHEMERON_TOMBSTONE)
				{
					this.data[i].key = null;
				}
			}
			for (int j = 0; j < num; j++)
			{
				object key = this.data[j].key;
				if (key != null)
				{
					int num2 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num;
					while (this.data[num2].key != null)
					{
						if (this.data[num2].key == key)
						{
							goto IL_0108;
						}
						if (++num2 == num)
						{
							num2 = 0;
						}
					}
					this.data[num2].key = key;
					this.data[num2].value = this.data[j].value;
					this.data[j].key = null;
					this.data[j].value = null;
				}
				IL_0108:;
			}
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x000E6D94 File Offset: 0x000E4F94
		private void RecomputeSize()
		{
			this.size = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				if (this.data[i].key != null)
				{
					this.size++;
				}
			}
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x000E6DDC File Offset: 0x000E4FDC
		private void Rehash()
		{
			this.RecomputeSize();
			uint prime = (uint)HashHelpers.GetPrime(((int)((float)this.size / 0.7f) << 1) | 1);
			if (prime > (float)this.data.Length * 0.5f && prime < (float)this.data.Length * 1.1f)
			{
				this.RehashWithoutResize();
				return;
			}
			Ephemeron[] array = new Ephemeron[prime];
			GC.register_ephemeron_array(array);
			this.size = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				object key = this.data[i].key;
				object value = this.data[i].value;
				if (key != null && key != GC.EPHEMERON_TOMBSTONE)
				{
					int num = array.Length;
					int num2 = -1;
					int num4;
					int num3 = (num4 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
					do
					{
						object key2 = array[num4].key;
						if (key2 == null || key2 == GC.EPHEMERON_TOMBSTONE)
						{
							goto IL_00D3;
						}
						if (++num4 == num)
						{
							num4 = 0;
						}
					}
					while (num4 != num3);
					IL_00ED:
					array[num2].key = key;
					array[num2].value = value;
					this.size++;
					goto IL_0118;
					IL_00D3:
					num2 = num4;
					goto IL_00ED;
				}
				IL_0118:;
			}
			this.data = array;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000E6F1C File Offset: 0x000E511C
		public void AddOrUpdate(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				if ((float)this.size >= (float)this.data.Length * 0.7f)
				{
					this.Rehash();
				}
				int num = this.data.Length;
				int num2 = -1;
				int num4;
				int num3 = (num4 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num4].key;
					if (key2 == null)
					{
						break;
					}
					if (key2 == GC.EPHEMERON_TOMBSTONE && num2 == -1)
					{
						num2 = num4;
					}
					else if (key2 == key)
					{
						num2 = num4;
					}
					if (++num4 == num)
					{
						num4 = 0;
					}
					if (num4 == num3)
					{
						goto IL_00BA;
					}
				}
				if (num2 == -1)
				{
					num2 = num4;
				}
				IL_00BA:
				this.data[num2].key = key;
				this.data[num2].value = value;
				this.size++;
			}
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x000E7040 File Offset: 0x000E5240
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				if ((float)this.size >= (float)this.data.Length * 0.7f)
				{
					this.Rehash();
				}
				int num = this.data.Length;
				int num2 = -1;
				int num4;
				int num3 = (num4 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num4].key;
					if (key2 == null)
					{
						break;
					}
					if (key2 == GC.EPHEMERON_TOMBSTONE && num2 == -1)
					{
						num2 = num4;
					}
					else if (key2 == key)
					{
						goto Block_9;
					}
					if (++num4 == num)
					{
						num4 = 0;
					}
					if (num4 == num3)
					{
						goto IL_00C7;
					}
				}
				if (num2 == -1)
				{
					num2 = num4;
					goto IL_00C7;
				}
				goto IL_00C7;
				Block_9:
				throw new ArgumentException("Key already in the list", "key");
				IL_00C7:
				this.data[num2].key = key;
				this.data[num2].value = value;
				this.size++;
			}
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x000E7170 File Offset: 0x000E5370
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			object @lock = this._lock;
			lock (@lock)
			{
				int num = this.data.Length;
				int num3;
				int num2 = (num3 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num3].key;
					if (key2 == key)
					{
						break;
					}
					if (key2 == null)
					{
						goto Block_5;
					}
					if (++num3 == num)
					{
						num3 = 0;
					}
					if (num3 == num2)
					{
						goto Block_7;
					}
				}
				this.data[num3].key = GC.EPHEMERON_TOMBSTONE;
				this.data[num3].value = null;
				return true;
				Block_5:
				Block_7:;
			}
			return false;
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x000E7248 File Offset: 0x000E5448
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Null key", "key");
			}
			value = default(TValue);
			object @lock = this._lock;
			lock (@lock)
			{
				int num = this.data.Length;
				int num3;
				int num2 = (num3 = (RuntimeHelpers.GetHashCode(key) & int.MaxValue) % num);
				for (;;)
				{
					object key2 = this.data[num3].key;
					if (key2 == key)
					{
						break;
					}
					if (key2 == null)
					{
						goto Block_5;
					}
					if (++num3 == num)
					{
						num3 = 0;
					}
					if (num3 == num2)
					{
						goto Block_7;
					}
				}
				value = (TValue)((object)this.data[num3].value);
				return true;
				Block_5:
				Block_7:;
			}
			return false;
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x000E7318 File Offset: 0x000E5518
		public TValue GetOrCreateValue(TKey key)
		{
			return this.GetValue(key, (TKey k) => Activator.CreateInstance<TValue>());
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x000E7340 File Offset: 0x000E5540
		public TValue GetValue(TKey key, ConditionalWeakTable<TKey, TValue>.CreateValueCallback createValueCallback)
		{
			if (createValueCallback == null)
			{
				throw new ArgumentNullException("Null create delegate", "createValueCallback");
			}
			object @lock = this._lock;
			TValue tvalue;
			lock (@lock)
			{
				if (this.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				tvalue = createValueCallback(key);
				this.Add(key, tvalue);
			}
			return tvalue;
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x000E73B0 File Offset: 0x000E55B0
		internal TKey FindEquivalentKeyUnsafe(TKey key, out TValue value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this.data.Length; i++)
				{
					Ephemeron ephemeron = this.data[i];
					if (object.Equals(ephemeron.key, key))
					{
						value = (TValue)((object)ephemeron.value);
						return (TKey)((object)ephemeron.key);
					}
				}
			}
			value = default(TValue);
			return default(TKey);
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x000E7454 File Offset: 0x000E5654
		[SecuritySafeCritical]
		public void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				for (int i = 0; i < this.data.Length; i++)
				{
					this.data[i].key = null;
					this.data[i].value = null;
				}
				this.size = 0;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x000E74CC File Offset: 0x000E56CC
		internal ICollection<TKey> Keys
		{
			[SecuritySafeCritical]
			get
			{
				object ephemeron_TOMBSTONE = GC.EPHEMERON_TOMBSTONE;
				List<TKey> list = new List<TKey>(this.data.Length);
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						TKey tkey = (TKey)((object)this.data[i].key);
						if (tkey != null && tkey != ephemeron_TOMBSTONE)
						{
							list.Add(tkey);
						}
					}
				}
				return list;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x000E7568 File Offset: 0x000E5768
		internal ICollection<TValue> Values
		{
			[SecuritySafeCritical]
			get
			{
				object ephemeron_TOMBSTONE = GC.EPHEMERON_TOMBSTONE;
				List<TValue> list = new List<TValue>(this.data.Length);
				object @lock = this._lock;
				lock (@lock)
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						Ephemeron ephemeron = this.data[i];
						if (ephemeron.key != null && ephemeron.key != ephemeron_TOMBSTONE)
						{
							list.Add((TValue)((object)ephemeron.value));
						}
					}
				}
				return list;
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x000E7604 File Offset: 0x000E5804
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			object @lock = this._lock;
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
			lock (@lock)
			{
				IEnumerator<KeyValuePair<TKey, TValue>> enumerator2;
				if (this.size != 0)
				{
					enumerator = new ConditionalWeakTable<TKey, TValue>.Enumerator(this);
					enumerator2 = enumerator;
				}
				else
				{
					enumerator2 = ((IEnumerable<KeyValuePair<TKey, TValue>>)Array.Empty<KeyValuePair<TKey, TValue>>()).GetEnumerator();
				}
				enumerator = enumerator2;
			}
			return enumerator;
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x000E765C File Offset: 0x000E585C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x04002D13 RID: 11539
		private const int INITIAL_SIZE = 13;

		// Token: 0x04002D14 RID: 11540
		private const float LOAD_FACTOR = 0.7f;

		// Token: 0x04002D15 RID: 11541
		private const float COMPACT_FACTOR = 0.5f;

		// Token: 0x04002D16 RID: 11542
		private const float EXPAND_FACTOR = 1.1f;

		// Token: 0x04002D17 RID: 11543
		private Ephemeron[] data;

		// Token: 0x04002D18 RID: 11544
		private object _lock = new object();

		// Token: 0x04002D19 RID: 11545
		private int size;

		// Token: 0x0200081E RID: 2078
		// (Invoke) Token: 0x06004672 RID: 18034
		public delegate TValue CreateValueCallback(TKey key);

		// Token: 0x0200081F RID: 2079
		private sealed class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06004675 RID: 18037 RVA: 0x000E7664 File Offset: 0x000E5864
			public Enumerator(ConditionalWeakTable<TKey, TValue> table)
			{
				this._table = table;
				this._currentIndex = -1;
			}

			// Token: 0x06004676 RID: 18038 RVA: 0x000E7684 File Offset: 0x000E5884
			~Enumerator()
			{
				this.Dispose();
			}

			// Token: 0x06004677 RID: 18039 RVA: 0x000E76B0 File Offset: 0x000E58B0
			public void Dispose()
			{
				if (Interlocked.Exchange<ConditionalWeakTable<TKey, TValue>>(ref this._table, null) != null)
				{
					this._current = default(KeyValuePair<TKey, TValue>);
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x06004678 RID: 18040 RVA: 0x000E76D4 File Offset: 0x000E58D4
			public bool MoveNext()
			{
				ConditionalWeakTable<TKey, TValue> table = this._table;
				if (table != null)
				{
					object @lock = table._lock;
					lock (@lock)
					{
						object ephemeron_TOMBSTONE = GC.EPHEMERON_TOMBSTONE;
						while (this._currentIndex < table.data.Length - 1)
						{
							this._currentIndex++;
							Ephemeron ephemeron = table.data[this._currentIndex];
							if (ephemeron.key != null && ephemeron.key != ephemeron_TOMBSTONE)
							{
								this._current = new KeyValuePair<TKey, TValue>((TKey)((object)ephemeron.key), (TValue)((object)ephemeron.value));
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x17000ADF RID: 2783
			// (get) Token: 0x06004679 RID: 18041 RVA: 0x000E7798 File Offset: 0x000E5998
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._currentIndex < 0)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current;
				}
			}

			// Token: 0x17000AE0 RID: 2784
			// (get) Token: 0x0600467A RID: 18042 RVA: 0x000E77AE File Offset: 0x000E59AE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600467B RID: 18043 RVA: 0x00004088 File Offset: 0x00002288
			public void Reset()
			{
			}

			// Token: 0x04002D1A RID: 11546
			private ConditionalWeakTable<TKey, TValue> _table;

			// Token: 0x04002D1B RID: 11547
			private int _currentIndex = -1;

			// Token: 0x04002D1C RID: 11548
			private KeyValuePair<TKey, TValue> _current;
		}

		// Token: 0x02000820 RID: 2080
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600467C RID: 18044 RVA: 0x000E77BB File Offset: 0x000E59BB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600467D RID: 18045 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600467E RID: 18046 RVA: 0x000E77C7 File Offset: 0x000E59C7
			internal TValue <GetOrCreateValue>b__17_0(TKey k)
			{
				return Activator.CreateInstance<TValue>();
			}

			// Token: 0x04002D1D RID: 11549
			public static readonly ConditionalWeakTable<TKey, TValue>.<>c <>9 = new ConditionalWeakTable<TKey, TValue>.<>c();

			// Token: 0x04002D1E RID: 11550
			public static ConditionalWeakTable<TKey, TValue>.CreateValueCallback <>9__17_0;
		}
	}
}
