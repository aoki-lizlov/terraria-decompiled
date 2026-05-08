using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000AE1 RID: 2785
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		// Token: 0x060066A6 RID: 26278 RVA: 0x0015C4E8 File Offset: 0x0015A6E8
		public Dictionary()
			: this(0, null)
		{
		}

		// Token: 0x060066A7 RID: 26279 RVA: 0x0015C4F2 File Offset: 0x0015A6F2
		public Dictionary(int capacity)
			: this(capacity, null)
		{
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x0015C4FC File Offset: 0x0015A6FC
		public Dictionary(IEqualityComparer<TKey> comparer)
			: this(0, comparer)
		{
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x0015C506 File Offset: 0x0015A706
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			if (comparer != EqualityComparer<TKey>.Default)
			{
				this._comparer = comparer;
			}
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x0015C534 File Offset: 0x0015A734
		public Dictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x0015C540 File Offset: 0x0015A740
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			if (dictionary.GetType() == typeof(Dictionary<TKey, TValue>))
			{
				Dictionary<TKey, TValue> dictionary2 = (Dictionary<TKey, TValue>)dictionary;
				int count = dictionary2._count;
				Dictionary<TKey, TValue>.Entry[] entries = dictionary2._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						this.Add(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0015C618 File Offset: 0x0015A818
		public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, null)
		{
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x0015C624 File Offset: 0x0015A824
		public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
		{
			ICollection<KeyValuePair<TKey, TValue>> collection2 = collection as ICollection<KeyValuePair<TKey, TValue>>;
			this..ctor((collection2 != null) ? collection2.Count : 0, comparer);
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x0015C69C File Offset: 0x0015A89C
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x0015C6B0 File Offset: 0x0015A8B0
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				if (this._comparer != null)
				{
					return this._comparer;
				}
				return EqualityComparer<TKey>.Default;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060066B0 RID: 26288 RVA: 0x0015C6D3 File Offset: 0x0015A8D3
		public int Count
		{
			get
			{
				return this._count - this._freeCount;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x0015C6E2 File Offset: 0x0015A8E2
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060066B2 RID: 26290 RVA: 0x0015C6E2 File Offset: 0x0015A8E2
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x0015C6E2 File Offset: 0x0015A8E2
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060066B4 RID: 26292 RVA: 0x0015C6FE File Offset: 0x0015A8FE
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060066B5 RID: 26293 RVA: 0x0015C6FE File Offset: 0x0015A8FE
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060066B6 RID: 26294 RVA: 0x0015C6FE File Offset: 0x0015A8FE
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this._values;
			}
		}

		// Token: 0x170011EB RID: 4587
		public TValue this[TKey key]
		{
			get
			{
				int num = this.FindEntry(key);
				if (num >= 0)
				{
					return this._entries[num].value;
				}
				ThrowHelper.ThrowKeyNotFoundException(key);
				return default(TValue);
			}
			set
			{
				this.TryInsert(key, value, InsertionBehavior.OverwriteExisting);
			}
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x0015C767 File Offset: 0x0015A967
		public void Add(TKey key, TValue value)
		{
			this.TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x0015C773 File Offset: 0x0015A973
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x0015C78C File Offset: 0x0015A98C
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this._entries[num].value, keyValuePair.Value);
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x0015C7D4 File Offset: 0x0015A9D4
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this._entries[num].value, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x0015C828 File Offset: 0x0015AA28
		public void Clear()
		{
			int count = this._count;
			if (count > 0)
			{
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._count = 0;
				this._freeList = -1;
				this._freeCount = 0;
				Array.Clear(this._entries, 0, count);
			}
			this._version++;
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0015C884 File Offset: 0x0015AA84
		public bool ContainsKey(TKey key)
		{
			return this.FindEntry(key) >= 0;
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0015C894 File Offset: 0x0015AA94
		public bool ContainsValue(TValue value)
		{
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			if (value == null)
			{
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].hashCode >= 0 && entries[i].value == null)
					{
						return true;
					}
				}
			}
			else if (default(TValue) != null)
			{
				for (int j = 0; j < this._count; j++)
				{
					if (entries[j].hashCode >= 0 && EqualityComparer<TValue>.Default.Equals(entries[j].value, value))
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int k = 0; k < this._count; k++)
				{
					if (entries[k].hashCode >= 0 && @default.Equals(entries[k].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x0015C980 File Offset: 0x0015AB80
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int count = this._count;
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			for (int i = 0; i < count; i++)
			{
				if (entries[i].hashCode >= 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
				}
			}
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0015CA06 File Offset: 0x0015AC06
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x0015CA0F File Offset: 0x0015AC0F
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x0015CA20 File Offset: 0x0015AC20
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this._comparer ?? EqualityComparer<TKey>.Default, typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x0015CABC File Offset: 0x0015ACBC
		private int FindEntry(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			int num = -1;
			int[] buckets = this._buckets;
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			int num2 = 0;
			if (buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				if (comparer == null)
				{
					int num3 = key.GetHashCode() & int.MaxValue;
					num = buckets[num3 % buckets.Length] - 1;
					if (default(TKey) != null)
					{
						while (num < entries.Length && (entries[num].hashCode != num3 || !EqualityComparer<TKey>.Default.Equals(entries[num].key, key)))
						{
							num = entries[num].next;
							if (num2 >= entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
					else
					{
						EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
						while (num < entries.Length && (entries[num].hashCode != num3 || !@default.Equals(entries[num].key, key)))
						{
							num = entries[num].next;
							if (num2 >= entries.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
				else
				{
					int num4 = comparer.GetHashCode(key) & int.MaxValue;
					num = buckets[num4 % buckets.Length] - 1;
					while (num < entries.Length && (entries[num].hashCode != num4 || !comparer.Equals(entries[num].key, key)))
					{
						num = entries[num].next;
						if (num2 >= entries.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
						num2++;
					}
				}
			}
			return num;
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x0015CC40 File Offset: 0x0015AE40
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this._freeList = -1;
			this._buckets = new int[prime];
			this._entries = new Dictionary<TKey, TValue>.Entry[prime];
			return prime;
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0015CC74 File Offset: 0x0015AE74
		private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			this._version++;
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			Dictionary<TKey, TValue>.Entry[] array = this._entries;
			IEqualityComparer<TKey> comparer = this._comparer;
			int num = ((comparer == null) ? key.GetHashCode() : comparer.GetHashCode(key)) & int.MaxValue;
			int num2 = 0;
			ref int ptr = ref this._buckets[num % this._buckets.Length];
			int i = ptr - 1;
			if (comparer == null)
			{
				if (default(TKey) != null)
				{
					while (i < array.Length)
					{
						if (array[i].hashCode == num && EqualityComparer<TKey>.Default.Equals(array[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								array[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
							}
							return false;
						}
						else
						{
							i = array[i].next;
							if (num2 >= array.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
				else
				{
					EqualityComparer<TKey> @default = EqualityComparer<TKey>.Default;
					while (i < array.Length)
					{
						if (array[i].hashCode == num && @default.Equals(array[i].key, key))
						{
							if (behavior == InsertionBehavior.OverwriteExisting)
							{
								array[i].value = value;
								return true;
							}
							if (behavior == InsertionBehavior.ThrowOnExisting)
							{
								ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
							}
							return false;
						}
						else
						{
							i = array[i].next;
							if (num2 >= array.Length)
							{
								ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
							}
							num2++;
						}
					}
				}
			}
			else
			{
				while (i < array.Length)
				{
					if (array[i].hashCode == num && comparer.Equals(array[i].key, key))
					{
						if (behavior == InsertionBehavior.OverwriteExisting)
						{
							array[i].value = value;
							return true;
						}
						if (behavior == InsertionBehavior.ThrowOnExisting)
						{
							ThrowHelper.ThrowAddingDuplicateWithKeyArgumentException(key);
						}
						return false;
					}
					else
					{
						i = array[i].next;
						if (num2 >= array.Length)
						{
							ThrowHelper.ThrowInvalidOperationException_ConcurrentOperationsNotSupported();
						}
						num2++;
					}
				}
			}
			bool flag = false;
			bool flag2 = false;
			int num3;
			if (this._freeCount > 0)
			{
				num3 = this._freeList;
				flag2 = true;
				this._freeCount--;
			}
			else
			{
				int count = this._count;
				if (count == array.Length)
				{
					this.Resize();
					flag = true;
				}
				num3 = count;
				this._count = count + 1;
				array = this._entries;
			}
			ref int ptr2 = (ref flag ? ref this._buckets[num % this._buckets.Length] : ref ptr);
			ref Dictionary<TKey, TValue>.Entry ptr3 = ref array[num3];
			if (flag2)
			{
				this._freeList = ptr3.next;
			}
			ptr3.hashCode = num;
			ptr3.next = ptr2 - 1;
			ptr3.key = key;
			ptr3.value = value;
			ptr2 = num3 + 1;
			return true;
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x0015CF38 File Offset: 0x0015B138
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this._comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.Initialize(int2);
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Key == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
					}
					this.Add(array[i].Key, array[i].Value);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = @int;
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x0015D028 File Offset: 0x0015B228
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this._count), false);
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x0015D03C File Offset: 0x0015B23C
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			int[] array = new int[newSize];
			Dictionary<TKey, TValue>.Entry[] array2 = new Dictionary<TKey, TValue>.Entry[newSize];
			int count = this._count;
			Array.Copy(this._entries, 0, array2, 0, count);
			if (default(TKey) == null && forceNewHashCodes)
			{
				for (int i = 0; i < count; i++)
				{
					if (array2[i].hashCode >= 0)
					{
						array2[i].hashCode = array2[i].key.GetHashCode() & int.MaxValue;
					}
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (array2[j].hashCode >= 0)
				{
					int num = array2[j].hashCode % newSize;
					array2[j].next = array[num] - 1;
					array[num] = j + 1;
				}
			}
			this._buckets = array;
			this._entries = array2;
		}

		// Token: 0x060066CA RID: 26314 RVA: 0x0015D128 File Offset: 0x0015B328
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				int num = ((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode()) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				ref Dictionary<TKey, TValue>.Entry ptr;
				for (int i = this._buckets[num2] - 1; i >= 0; i = ptr.next)
				{
					ptr = ref this._entries[i];
					if (ptr.hashCode == num)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								this._buckets[num2] = ptr.next + 1;
							}
							else
							{
								this._entries[num3].next = ptr.next;
							}
							ptr.hashCode = -1;
							ptr.next = this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							this._version++;
							return true;
						}
					}
					num3 = i;
				}
			}
			return false;
		}

		// Token: 0x060066CB RID: 26315 RVA: 0x0015D280 File Offset: 0x0015B480
		public bool Remove(TKey key, out TValue value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this._buckets != null)
			{
				IEqualityComparer<TKey> comparer = this._comparer;
				int num = ((comparer != null) ? comparer.GetHashCode(key) : key.GetHashCode()) & int.MaxValue;
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				ref Dictionary<TKey, TValue>.Entry ptr;
				for (int i = this._buckets[num2] - 1; i >= 0; i = ptr.next)
				{
					ptr = ref this._entries[i];
					if (ptr.hashCode == num)
					{
						IEqualityComparer<TKey> comparer2 = this._comparer;
						if ((comparer2 != null) ? comparer2.Equals(ptr.key, key) : EqualityComparer<TKey>.Default.Equals(ptr.key, key))
						{
							if (num3 < 0)
							{
								this._buckets[num2] = ptr.next + 1;
							}
							else
							{
								this._entries[num3].next = ptr.next;
							}
							value = ptr.value;
							ptr.hashCode = -1;
							ptr.next = this._freeList;
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
							{
								ptr.key = default(TKey);
							}
							if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
							{
								ptr.value = default(TValue);
							}
							this._freeList = i;
							this._freeCount++;
							this._version++;
							return true;
						}
					}
					num3 = i;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x0015D3EC File Offset: 0x0015B5EC
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				value = this._entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x0015D426 File Offset: 0x0015B626
		public bool TryAdd(TKey key, TValue value)
		{
			return this.TryInsert(key, value, InsertionBehavior.None);
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060066CE RID: 26318 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x0015D431 File Offset: 0x0015B631
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x0015D43C File Offset: 0x0015B63C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index > array.Length)
			{
				ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				Dictionary<TKey, TValue>.Entry[] entries = this._entries;
				for (int i = 0; i < this._count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array3[index++] = new DictionaryEntry(entries[i].key, entries[i].value);
					}
				}
				return;
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
			try
			{
				int count = this._count;
				Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
				for (int j = 0; j < count; j++)
				{
					if (entries2[j].hashCode >= 0)
					{
						array4[index++] = new KeyValuePair<TKey, TValue>(entries2[j].key, entries2[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
			}
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x0015CA0F File Offset: 0x0015AC0F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x0015D59C File Offset: 0x0015B79C
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int num = ((this._entries == null) ? 0 : this._entries.Length);
			if (num >= capacity)
			{
				return num;
			}
			if (this._buckets == null)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.Resize(prime, false);
			return prime;
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x0015D5EE File Offset: 0x0015B7EE
		public void TrimExcess()
		{
			this.TrimExcess(this.Count);
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0015D5FC File Offset: 0x0015B7FC
		public void TrimExcess(int capacity)
		{
			if (capacity < this.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			Dictionary<TKey, TValue>.Entry[] entries = this._entries;
			int num = ((entries == null) ? 0 : entries.Length);
			if (prime >= num)
			{
				return;
			}
			int count = this._count;
			this.Initialize(prime);
			Dictionary<TKey, TValue>.Entry[] entries2 = this._entries;
			int[] buckets = this._buckets;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				int hashCode = entries[i].hashCode;
				if (hashCode >= 0)
				{
					Dictionary<TKey, TValue>.Entry[] array = entries2;
					int num3 = num2;
					array[num3] = entries[i];
					int num4 = hashCode % prime;
					array[num3].next = buckets[num4] - 1;
					buckets[num4] = num2 + 1;
					num2++;
				}
			}
			this._count = num2;
			this._freeCount = 0;
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x0015D6C3 File Offset: 0x0015B8C3
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

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x0000408A File Offset: 0x0000228A
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x0000408A File Offset: 0x0000228A
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x0015D6E5 File Offset: 0x0015B8E5
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x0015D6ED File Offset: 0x0015B8ED
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170011F3 RID: 4595
		object IDictionary.this[object key]
		{
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.FindEntry((TKey)((object)key));
					if (num >= 0)
					{
						return this._entries[num].value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0015D7B0 File Offset: 0x0015B9B0
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x0015D7C4 File Offset: 0x0015B9C4
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x0015D83C File Offset: 0x0015BA3C
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x0015D854 File Offset: 0x0015BA54
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x0015D862 File Offset: 0x0015BA62
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x04003BEC RID: 15340
		private int[] _buckets;

		// Token: 0x04003BED RID: 15341
		private Dictionary<TKey, TValue>.Entry[] _entries;

		// Token: 0x04003BEE RID: 15342
		private int _count;

		// Token: 0x04003BEF RID: 15343
		private int _freeList;

		// Token: 0x04003BF0 RID: 15344
		private int _freeCount;

		// Token: 0x04003BF1 RID: 15345
		private int _version;

		// Token: 0x04003BF2 RID: 15346
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04003BF3 RID: 15347
		private Dictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04003BF4 RID: 15348
		private Dictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x04003BF5 RID: 15349
		private object _syncRoot;

		// Token: 0x04003BF6 RID: 15350
		private const string VersionName = "Version";

		// Token: 0x04003BF7 RID: 15351
		private const string HashSizeName = "HashSize";

		// Token: 0x04003BF8 RID: 15352
		private const string KeyValuePairsName = "KeyValuePairs";

		// Token: 0x04003BF9 RID: 15353
		private const string ComparerName = "Comparer";

		// Token: 0x02000AE2 RID: 2786
		private struct Entry
		{
			// Token: 0x04003BFA RID: 15354
			public int hashCode;

			// Token: 0x04003BFB RID: 15355
			public int next;

			// Token: 0x04003BFC RID: 15356
			public TKey key;

			// Token: 0x04003BFD RID: 15357
			public TValue value;
		}

		// Token: 0x02000AE3 RID: 2787
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x060066E2 RID: 26338 RVA: 0x0015D879 File Offset: 0x0015BA79
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this._dictionary = dictionary;
				this._version = dictionary._version;
				this._index = 0;
				this._getEnumeratorRetType = getEnumeratorRetType;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x060066E3 RID: 26339 RVA: 0x0015D8A8 File Offset: 0x0015BAA8
			public bool MoveNext()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				while (this._index < this._dictionary._count)
				{
					Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
					int index = this._index;
					this._index = index + 1;
					ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
					if (ptr.hashCode >= 0)
					{
						this._current = new KeyValuePair<TKey, TValue>(ptr.key, ptr.value);
						return true;
					}
				}
				this._index = this._dictionary._count + 1;
				this._current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			// Token: 0x170011F4 RID: 4596
			// (get) Token: 0x060066E4 RID: 26340 RVA: 0x0015D946 File Offset: 0x0015BB46
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x060066E5 RID: 26341 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x170011F5 RID: 4597
			// (get) Token: 0x060066E6 RID: 26342 RVA: 0x0015D950 File Offset: 0x0015BB50
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					if (this._getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this._current.Key, this._current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x060066E7 RID: 26343 RVA: 0x0015D9D3 File Offset: 0x0015BBD3
			void IEnumerator.Reset()
			{
				if (this._version != this._dictionary._version)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
				}
				this._index = 0;
				this._current = default(KeyValuePair<TKey, TValue>);
			}

			// Token: 0x170011F6 RID: 4598
			// (get) Token: 0x060066E8 RID: 26344 RVA: 0x0015DA00 File Offset: 0x0015BC00
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return new DictionaryEntry(this._current.Key, this._current.Value);
				}
			}

			// Token: 0x170011F7 RID: 4599
			// (get) Token: 0x060066E9 RID: 26345 RVA: 0x0015DA54 File Offset: 0x0015BC54
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Key;
				}
			}

			// Token: 0x170011F8 RID: 4600
			// (get) Token: 0x060066EA RID: 26346 RVA: 0x0015DA88 File Offset: 0x0015BC88
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this._index == 0 || this._index == this._dictionary._count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return this._current.Value;
				}
			}

			// Token: 0x04003BFE RID: 15358
			private Dictionary<TKey, TValue> _dictionary;

			// Token: 0x04003BFF RID: 15359
			private int _version;

			// Token: 0x04003C00 RID: 15360
			private int _index;

			// Token: 0x04003C01 RID: 15361
			private KeyValuePair<TKey, TValue> _current;

			// Token: 0x04003C02 RID: 15362
			private int _getEnumeratorRetType;

			// Token: 0x04003C03 RID: 15363
			internal const int DictEntry = 1;

			// Token: 0x04003C04 RID: 15364
			internal const int KeyValuePair = 2;
		}

		// Token: 0x02000AE4 RID: 2788
		[DebuggerTypeProxy(typeof(DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x060066EB RID: 26347 RVA: 0x0015DABC File Offset: 0x0015BCBC
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x060066EC RID: 26348 RVA: 0x0015DAD4 File Offset: 0x0015BCD4
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x060066ED RID: 26349 RVA: 0x0015DAE4 File Offset: 0x0015BCE4
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			// Token: 0x170011F9 RID: 4601
			// (get) Token: 0x060066EE RID: 26350 RVA: 0x0015DB6C File Offset: 0x0015BD6C
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x170011FA RID: 4602
			// (get) Token: 0x060066EF RID: 26351 RVA: 0x00003FB7 File Offset: 0x000021B7
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060066F0 RID: 26352 RVA: 0x0015DB79 File Offset: 0x0015BD79
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x060066F1 RID: 26353 RVA: 0x0015DB79 File Offset: 0x0015BD79
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x060066F2 RID: 26354 RVA: 0x0015DB82 File Offset: 0x0015BD82
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._dictionary.ContainsKey(item);
			}

			// Token: 0x060066F3 RID: 26355 RVA: 0x0015DB90 File Offset: 0x0015BD90
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x060066F4 RID: 26356 RVA: 0x0015DB9A File Offset: 0x0015BD9A
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x060066F5 RID: 26357 RVA: 0x0015DB9A File Offset: 0x0015BD9A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this._dictionary);
			}

			// Token: 0x060066F6 RID: 26358 RVA: 0x0015DBAC File Offset: 0x0015BDAC
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x170011FB RID: 4603
			// (get) Token: 0x060066F7 RID: 26359 RVA: 0x0000408A File Offset: 0x0000228A
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011FC RID: 4604
			// (get) Token: 0x060066F8 RID: 26360 RVA: 0x0015DC98 File Offset: 0x0015BE98
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04003C05 RID: 15365
			private Dictionary<TKey, TValue> _dictionary;

			// Token: 0x02000AE5 RID: 2789
			[Serializable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x060066F9 RID: 26361 RVA: 0x0015DCA5 File Offset: 0x0015BEA5
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x060066FA RID: 26362 RVA: 0x00004088 File Offset: 0x00002288
				public void Dispose()
				{
				}

				// Token: 0x060066FB RID: 26363 RVA: 0x0015DCD0 File Offset: 0x0015BED0
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.hashCode >= 0)
						{
							this._currentKey = ptr.key;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentKey = default(TKey);
					return false;
				}

				// Token: 0x170011FD RID: 4605
				// (get) Token: 0x060066FC RID: 26364 RVA: 0x0015DD63 File Offset: 0x0015BF63
				public TKey Current
				{
					get
					{
						return this._currentKey;
					}
				}

				// Token: 0x170011FE RID: 4606
				// (get) Token: 0x060066FD RID: 26365 RVA: 0x0015DD6B File Offset: 0x0015BF6B
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentKey;
					}
				}

				// Token: 0x060066FE RID: 26366 RVA: 0x0015DD9A File Offset: 0x0015BF9A
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentKey = default(TKey);
				}

				// Token: 0x04003C06 RID: 15366
				private Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04003C07 RID: 15367
				private int _index;

				// Token: 0x04003C08 RID: 15368
				private int _version;

				// Token: 0x04003C09 RID: 15369
				private TKey _currentKey;
			}
		}

		// Token: 0x02000AE6 RID: 2790
		[DebuggerTypeProxy(typeof(DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x060066FF RID: 26367 RVA: 0x0015DDC7 File Offset: 0x0015BFC7
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this._dictionary = dictionary;
			}

			// Token: 0x06006700 RID: 26368 RVA: 0x0015DDDF File Offset: 0x0015BFDF
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06006701 RID: 26369 RVA: 0x0015DDEC File Offset: 0x0015BFEC
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			// Token: 0x170011FF RID: 4607
			// (get) Token: 0x06006702 RID: 26370 RVA: 0x0015DE74 File Offset: 0x0015C074
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x17001200 RID: 4608
			// (get) Token: 0x06006703 RID: 26371 RVA: 0x00003FB7 File Offset: 0x000021B7
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006704 RID: 26372 RVA: 0x0015DE81 File Offset: 0x0015C081
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006705 RID: 26373 RVA: 0x0015DE8A File Offset: 0x0015C08A
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x06006706 RID: 26374 RVA: 0x0015DE81 File Offset: 0x0015C081
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006707 RID: 26375 RVA: 0x0015DE94 File Offset: 0x0015C094
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._dictionary.ContainsValue(item);
			}

			// Token: 0x06006708 RID: 26376 RVA: 0x0015DEA2 File Offset: 0x0015C0A2
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x06006709 RID: 26377 RVA: 0x0015DEA2 File Offset: 0x0015C0A2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this._dictionary);
			}

			// Token: 0x0600670A RID: 26378 RVA: 0x0015DEB4 File Offset: 0x0015C0B4
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index > array.Length)
				{
					ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
				}
				if (array.Length - index < this._dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
				int count = this._dictionary._count;
				Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
				}
			}

			// Token: 0x17001201 RID: 4609
			// (get) Token: 0x0600670B RID: 26379 RVA: 0x0000408A File Offset: 0x0000228A
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001202 RID: 4610
			// (get) Token: 0x0600670C RID: 26380 RVA: 0x0015DFA0 File Offset: 0x0015C1A0
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dictionary).SyncRoot;
				}
			}

			// Token: 0x04003C0A RID: 15370
			private Dictionary<TKey, TValue> _dictionary;

			// Token: 0x02000AE7 RID: 2791
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x0600670D RID: 26381 RVA: 0x0015DFAD File Offset: 0x0015C1AD
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this._dictionary = dictionary;
					this._version = dictionary._version;
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x0600670E RID: 26382 RVA: 0x00004088 File Offset: 0x00002288
				public void Dispose()
				{
				}

				// Token: 0x0600670F RID: 26383 RVA: 0x0015DFD8 File Offset: 0x0015C1D8
				public bool MoveNext()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					while (this._index < this._dictionary._count)
					{
						Dictionary<TKey, TValue>.Entry[] entries = this._dictionary._entries;
						int index = this._index;
						this._index = index + 1;
						ref Dictionary<TKey, TValue>.Entry ptr = ref entries[index];
						if (ptr.hashCode >= 0)
						{
							this._currentValue = ptr.value;
							return true;
						}
					}
					this._index = this._dictionary._count + 1;
					this._currentValue = default(TValue);
					return false;
				}

				// Token: 0x17001203 RID: 4611
				// (get) Token: 0x06006710 RID: 26384 RVA: 0x0015E06B File Offset: 0x0015C26B
				public TValue Current
				{
					get
					{
						return this._currentValue;
					}
				}

				// Token: 0x17001204 RID: 4612
				// (get) Token: 0x06006711 RID: 26385 RVA: 0x0015E073 File Offset: 0x0015C273
				object IEnumerator.Current
				{
					get
					{
						if (this._index == 0 || this._index == this._dictionary._count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
						}
						return this._currentValue;
					}
				}

				// Token: 0x06006712 RID: 26386 RVA: 0x0015E0A2 File Offset: 0x0015C2A2
				void IEnumerator.Reset()
				{
					if (this._version != this._dictionary._version)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
					}
					this._index = 0;
					this._currentValue = default(TValue);
				}

				// Token: 0x04003C0B RID: 15371
				private Dictionary<TKey, TValue> _dictionary;

				// Token: 0x04003C0C RID: 15372
				private int _index;

				// Token: 0x04003C0D RID: 15373
				private int _version;

				// Token: 0x04003C0E RID: 15374
				private TValue _currentValue;
			}
		}
	}
}
