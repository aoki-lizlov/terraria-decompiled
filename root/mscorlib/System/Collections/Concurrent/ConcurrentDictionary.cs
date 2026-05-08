using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AB0 RID: 2736
	[DebuggerTypeProxy(typeof(IDictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060064FC RID: 25852 RVA: 0x00158100 File Offset: 0x00156300
		private static bool IsValueWriteAtomic()
		{
			Type typeFromHandle = typeof(TValue);
			if (!typeFromHandle.IsValueType)
			{
				return true;
			}
			switch (Type.GetTypeCode(typeFromHandle))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Single:
				return true;
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Double:
				return IntPtr.Size == 8;
			default:
				return false;
			}
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x0015816F File Offset: 0x0015636F
		public ConcurrentDictionary()
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, null)
		{
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x00158180 File Offset: 0x00156380
		public ConcurrentDictionary(int concurrencyLevel, int capacity)
			: this(concurrencyLevel, capacity, false, null)
		{
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x0015818C File Offset: 0x0015638C
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
			: this(collection, null)
		{
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x00158196 File Offset: 0x00156396
		public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
			: this(ConcurrentDictionary<TKey, TValue>.DefaultConcurrencyLevel, 31, true, comparer)
		{
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x001581A7 File Offset: 0x001563A7
		public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x001581C5 File Offset: 0x001563C5
		public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, 31, false, comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x001581E8 File Offset: 0x001563E8
		private void InitializeFromCollection(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				if (keyValuePair.Key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (!this.TryAddInternal(keyValuePair.Key, this._comparer.GetHashCode(keyValuePair.Key), keyValuePair.Value, false, false, out tvalue))
				{
					throw new ArgumentException("The source argument contains duplicate keys.");
				}
			}
			if (this._budget == 0)
			{
				this._budget = this._tables._buckets.Length / this._tables._locks.Length;
			}
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x001582A0 File Offset: 0x001564A0
		public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer)
			: this(concurrencyLevel, capacity, false, comparer)
		{
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001582AC File Offset: 0x001564AC
		internal ConcurrentDictionary(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<TKey> comparer)
		{
			if (concurrencyLevel < 1)
			{
				throw new ArgumentOutOfRangeException("concurrencyLevel", "The concurrencyLevel argument must be positive.");
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "The capacity argument must be greater than or equal to zero.");
			}
			if (capacity < concurrencyLevel)
			{
				capacity = concurrencyLevel;
			}
			object[] array = new object[concurrencyLevel];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new object();
			}
			int[] array2 = new int[array.Length];
			ConcurrentDictionary<TKey, TValue>.Node[] array3 = new ConcurrentDictionary<TKey, TValue>.Node[capacity];
			this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array3, array, array2);
			this._comparer = comparer ?? EqualityComparer<TKey>.Default;
			this._growLockArray = growLockArray;
			this._budget = array3.Length / array.Length;
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x00158350 File Offset: 0x00156550
		public bool TryAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryAddInternal(key, this._comparer.GetHashCode(key), value, false, true, out tvalue);
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00158384 File Offset: 0x00156584
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x001583A8 File Offset: 0x001565A8
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryRemoveInternal(key, out value, false, default(TValue));
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x001583D4 File Offset: 0x001565D4
		private bool TryRemoveInternal(TKey key, out TValue value, bool matchValue, TValue oldValue)
		{
			int hashCode = this._comparer.GetHashCode(key);
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashCode, out num, out num2, tables._buckets.Length, tables._locks.Length);
				object obj = tables._locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num];
					while (node2 != null)
					{
						if (hashCode == node2._hashcode && this._comparer.Equals(node2._key, key))
						{
							if (matchValue && !EqualityComparer<TValue>.Default.Equals(oldValue, node2._value))
							{
								value = default(TValue);
								return false;
							}
							if (node == null)
							{
								Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node2._next);
							}
							else
							{
								node._next = node2._next;
							}
							value = node2._value;
							tables._countPerLock[num2]--;
							return true;
						}
						else
						{
							node = node2;
							node2 = node2._next;
						}
					}
				}
				break;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x00158528 File Offset: 0x00156728
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryGetValueInternal(key, this._comparer.GetHashCode(key), out value);
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x0015854C File Offset: 0x0015674C
		private bool TryGetValueInternal(TKey key, int hashcode, out TValue value)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
			int bucket = ConcurrentDictionary<TKey, TValue>.GetBucket(hashcode, tables._buckets.Length);
			for (ConcurrentDictionary<TKey, TValue>.Node node = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[bucket]); node != null; node = node._next)
			{
				if (hashcode == node._hashcode && this._comparer.Equals(node._key, key))
				{
					value = node._value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x001585C4 File Offset: 0x001567C4
		public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return this.TryUpdateInternal(key, this._comparer.GetHashCode(key), newValue, comparisonValue);
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x001585E8 File Offset: 0x001567E8
		private bool TryUpdateInternal(TKey key, int hashcode, TValue newValue, TValue comparisonValue)
		{
			IEqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			bool flag2;
			for (;;)
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num;
				int num2;
				ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashcode, out num, out num2, tables._buckets.Length, tables._locks.Length);
				object obj = tables._locks[num2];
				lock (obj)
				{
					if (tables != this._tables)
					{
						continue;
					}
					ConcurrentDictionary<TKey, TValue>.Node node = null;
					ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num];
					while (node2 != null)
					{
						if (hashcode == node2._hashcode && this._comparer.Equals(node2._key, key))
						{
							if (@default.Equals(node2._value, comparisonValue))
							{
								if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
								{
									node2._value = newValue;
								}
								else
								{
									ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2._key, newValue, hashcode, node2._next);
									if (node == null)
									{
										Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node3);
									}
									else
									{
										node._next = node3;
									}
								}
								return true;
							}
							return false;
						}
						else
						{
							node = node2;
							node2 = node2._next;
						}
					}
					flag2 = false;
				}
				break;
			}
			return flag2;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x00158714 File Offset: 0x00156914
		public void Clear()
		{
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = new ConcurrentDictionary<TKey, TValue>.Tables(new ConcurrentDictionary<TKey, TValue>.Node[31], this._tables._locks, new int[this._tables._countPerLock.Length]);
				this._tables = tables;
				this._budget = Math.Max(1, tables._buckets.Length / tables._locks.Length);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0015879C File Offset: 0x0015699C
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				int num2 = 0;
				int num3 = 0;
				while (num3 < this._tables._locks.Length && num2 >= 0)
				{
					num2 += this._tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				this.CopyToPairs(array, index);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x00158844 File Offset: 0x00156A44
		public KeyValuePair<TKey, TValue>[] ToArray()
		{
			int num = 0;
			checked
			{
				KeyValuePair<TKey, TValue>[] array;
				try
				{
					this.AcquireAllLocks(ref num);
					int num2 = 0;
					for (int i = 0; i < this._tables._locks.Length; i++)
					{
						num2 += this._tables._countPerLock[i];
					}
					if (num2 == 0)
					{
						array = Array.Empty<KeyValuePair<TKey, TValue>>();
					}
					else
					{
						KeyValuePair<TKey, TValue>[] array2 = new KeyValuePair<TKey, TValue>[num2];
						this.CopyToPairs(array2, 0);
						array = array2;
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return array;
			}
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x001588C8 File Offset: 0x00156AC8
		private void CopyToPairs(KeyValuePair<TKey, TValue>[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x00158920 File Offset: 0x00156B20
		private void CopyToEntries(DictionaryEntry[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new DictionaryEntry(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x00158984 File Offset: 0x00156B84
		private void CopyToObjects(object[] array, int index)
		{
			foreach (ConcurrentDictionary<TKey, TValue>.Node node in this._tables._buckets)
			{
				while (node != null)
				{
					array[index] = new KeyValuePair<TKey, TValue>(node._key, node._value);
					index++;
					node = node._next;
				}
			}
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x001589DD File Offset: 0x00156BDD
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			ConcurrentDictionary<TKey, TValue>.Node[] buckets = this._tables._buckets;
			int num;
			for (int i = 0; i < buckets.Length; i = num + 1)
			{
				ConcurrentDictionary<TKey, TValue>.Node current;
				for (current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]); current != null; current = current._next)
				{
					yield return new KeyValuePair<TKey, TValue>(current._key, current._value);
				}
				current = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x001589EC File Offset: 0x00156BEC
		private bool TryAddInternal(TKey key, int hashcode, TValue value, bool updateIfExists, bool acquireLock, out TValue resultingValue)
		{
			checked
			{
				ConcurrentDictionary<TKey, TValue>.Tables tables;
				bool flag;
				for (;;)
				{
					tables = this._tables;
					int num;
					int num2;
					ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(hashcode, out num, out num2, tables._buckets.Length, tables._locks.Length);
					flag = false;
					bool flag2 = false;
					try
					{
						if (acquireLock)
						{
							Monitor.Enter(tables._locks[num2], ref flag2);
						}
						if (tables != this._tables)
						{
							continue;
						}
						ConcurrentDictionary<TKey, TValue>.Node node = null;
						for (ConcurrentDictionary<TKey, TValue>.Node node2 = tables._buckets[num]; node2 != null; node2 = node2._next)
						{
							if (hashcode == node2._hashcode && this._comparer.Equals(node2._key, key))
							{
								if (updateIfExists)
								{
									if (ConcurrentDictionary<TKey, TValue>.s_isValueWriteAtomic)
									{
										node2._value = value;
									}
									else
									{
										ConcurrentDictionary<TKey, TValue>.Node node3 = new ConcurrentDictionary<TKey, TValue>.Node(node2._key, value, hashcode, node2._next);
										if (node == null)
										{
											Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], node3);
										}
										else
										{
											node._next = node3;
										}
									}
									resultingValue = value;
								}
								else
								{
									resultingValue = node2._value;
								}
								return false;
							}
							node = node2;
						}
						Volatile.Write<ConcurrentDictionary<TKey, TValue>.Node>(ref tables._buckets[num], new ConcurrentDictionary<TKey, TValue>.Node(key, value, hashcode, tables._buckets[num]));
						tables._countPerLock[num2]++;
						if (tables._countPerLock[num2] > this._budget)
						{
							flag = true;
						}
					}
					finally
					{
						if (flag2)
						{
							Monitor.Exit(tables._locks[num2]);
						}
					}
					break;
				}
				if (flag)
				{
					this.GrowTable(tables);
				}
				resultingValue = value;
				return true;
			}
		}

		// Token: 0x17001172 RID: 4466
		public TValue this[TKey key]
		{
			get
			{
				TValue tvalue;
				if (!this.TryGetValue(key, out tvalue))
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNotFoundException(key);
				}
				return tvalue;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				this.TryAddInternal(key, this._comparer.GetHashCode(key), value, true, true, out tvalue);
			}
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x0004EA26 File Offset: 0x0004CC26
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNotFoundException(object key)
		{
			throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x00158BE3 File Offset: 0x00156DE3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowKeyNullException()
		{
			throw new ArgumentNullException("key");
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600651A RID: 25882 RVA: 0x00158BF0 File Offset: 0x00156DF0
		public int Count
		{
			get
			{
				int num = 0;
				int countInternal;
				try
				{
					this.AcquireAllLocks(ref num);
					countInternal = this.GetCountInternal();
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return countInternal;
			}
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x00158C2C File Offset: 0x00156E2C
		private int GetCountInternal()
		{
			int num = 0;
			for (int i = 0; i < this._tables._countPerLock.Length; i++)
			{
				num += this._tables._countPerLock[i];
			}
			return num;
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x00158C6C File Offset: 0x00156E6C
		public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, valueFactory(key), false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x00158CC4 File Offset: 0x00156EC4
		public TValue GetOrAdd<TArg>(TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, valueFactory(key, factoryArgument), false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x00158D1C File Offset: 0x00156F1C
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue;
			if (!this.TryGetValueInternal(key, hashCode, out tvalue))
			{
				this.TryAddInternal(key, hashCode, value, false, true, out tvalue);
			}
			return tvalue;
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x00158D60 File Offset: 0x00156F60
		public TValue AddOrUpdate<TArg>(TKey key, Func<TKey, TArg, TValue> addValueFactory, Func<TKey, TValue, TArg, TValue> updateValueFactory, TArg factoryArgument)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue, factoryArgument);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValueFactory(key, factoryArgument), false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x00158DE0 File Offset: 0x00156FE0
		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (addValueFactory == null)
			{
				throw new ArgumentNullException("addValueFactory");
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValueFactory(key), false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x00158E5C File Offset: 0x0015705C
		public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (updateValueFactory == null)
			{
				throw new ArgumentNullException("updateValueFactory");
			}
			int hashCode = this._comparer.GetHashCode(key);
			TValue tvalue2;
			for (;;)
			{
				TValue tvalue;
				TValue tvalue3;
				if (this.TryGetValueInternal(key, hashCode, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
					if (this.TryUpdateInternal(key, hashCode, tvalue2, tvalue))
					{
						break;
					}
				}
				else if (this.TryAddInternal(key, hashCode, addValue, false, true, out tvalue3))
				{
					return tvalue3;
				}
			}
			return tvalue2;
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x00158EC4 File Offset: 0x001570C4
		public bool IsEmpty
		{
			get
			{
				int num = 0;
				try
				{
					this.AcquireAllLocks(ref num);
					for (int i = 0; i < this._tables._countPerLock.Length; i++)
					{
						if (this._tables._countPerLock[i] != 0)
						{
							return false;
						}
					}
				}
				finally
				{
					this.ReleaseLocks(0, num);
				}
				return true;
			}
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x00158F2C File Offset: 0x0015712C
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			if (!this.TryAdd(key, value))
			{
				throw new ArgumentException("The key already existed in the dictionary.");
			}
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x00158F44 File Offset: 0x00157144
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			TValue tvalue;
			return this.TryRemove(key, out tvalue);
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x00158F5A File Offset: 0x0015715A
		public ICollection<TKey> Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x00158F5A File Offset: 0x0015715A
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x00158F62 File Offset: 0x00157162
		public ICollection<TValue> Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x00158F62 File Offset: 0x00157162
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x00158F6A File Offset: 0x0015716A
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			((IDictionary<TKey, TValue>)this).Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x00158F80 File Offset: 0x00157180
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			TValue tvalue;
			return this.TryGetValue(keyValuePair.Key, out tvalue) && EqualityComparer<TValue>.Default.Equals(tvalue, keyValuePair.Value);
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600652B RID: 25899 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x00158FB4 File Offset: 0x001571B4
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			if (keyValuePair.Key == null)
			{
				throw new ArgumentNullException("keyValuePair", "TKey is a reference type and item.Key is null.");
			}
			TValue tvalue;
			return this.TryRemoveInternal(keyValuePair.Key, out tvalue, true, keyValuePair.Value);
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x00158FF6 File Offset: 0x001571F6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x00159000 File Offset: 0x00157200
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (!(key is TKey))
			{
				throw new ArgumentException("The key was of an incorrect type for this dictionary.");
			}
			TValue tvalue;
			try
			{
				tvalue = (TValue)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException("The value was of an incorrect type for this dictionary.");
			}
			((IDictionary<TKey, TValue>)this).Add((TKey)((object)key), tvalue);
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x0015905C File Offset: 0x0015725C
		bool IDictionary.Contains(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			return key is TKey && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x0015907C File Offset: 0x0015727C
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new ConcurrentDictionary<TKey, TValue>.DictionaryEnumerator(this);
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06006531 RID: 25905 RVA: 0x0000408A File Offset: 0x0000228A
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x0000408A File Offset: 0x0000228A
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06006533 RID: 25907 RVA: 0x00158F5A File Offset: 0x0015715A
		ICollection IDictionary.Keys
		{
			get
			{
				return this.GetKeys();
			}
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x00159084 File Offset: 0x00157284
		void IDictionary.Remove(object key)
		{
			if (key == null)
			{
				ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
			}
			if (key is TKey)
			{
				TValue tvalue;
				this.TryRemove((TKey)((object)key), out tvalue);
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x00158F62 File Offset: 0x00157162
		ICollection IDictionary.Values
		{
			get
			{
				return this.GetValues();
			}
		}

		// Token: 0x1700117E RID: 4478
		object IDictionary.this[object key]
		{
			get
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				TValue tvalue;
				if (key is TKey && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					ConcurrentDictionary<TKey, TValue>.ThrowKeyNullException();
				}
				if (!(key is TKey))
				{
					throw new ArgumentException("The key was of an incorrect type for this dictionary.");
				}
				if (!(value is TValue))
				{
					throw new ArgumentException("The value was of an incorrect type for this dictionary.");
				}
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00159138 File Offset: 0x00157338
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument is less than zero.");
			}
			int num = 0;
			try
			{
				this.AcquireAllLocks(ref num);
				ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
				int num2 = 0;
				int num3 = 0;
				while (num3 < tables._locks.Length && num2 >= 0)
				{
					num2 += tables._countPerLock[num3];
					num3++;
				}
				if (array.Length - num2 < index || num2 < 0)
				{
					throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.");
				}
				KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
				if (array2 != null)
				{
					this.CopyToPairs(array2, index);
				}
				else
				{
					DictionaryEntry[] array3 = array as DictionaryEntry[];
					if (array3 != null)
					{
						this.CopyToEntries(array3, index);
					}
					else
					{
						object[] array4 = array as object[];
						if (array4 == null)
						{
							throw new ArgumentException("The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.", "array");
						}
						this.CopyToObjects(array4, index);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06006539 RID: 25913 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x001572CF File Offset: 0x001554CF
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x0015922C File Offset: 0x0015742C
		private void GrowTable(ConcurrentDictionary<TKey, TValue>.Tables tables)
		{
			int num = 0;
			try
			{
				this.AcquireLocks(0, 1, ref num);
				if (tables == this._tables)
				{
					long num2 = 0L;
					for (int i = 0; i < tables._countPerLock.Length; i++)
					{
						num2 += (long)tables._countPerLock[i];
					}
					if (num2 < (long)(tables._buckets.Length / 4))
					{
						this._budget = 2 * this._budget;
						if (this._budget < 0)
						{
							this._budget = int.MaxValue;
						}
					}
					else
					{
						int num3 = 0;
						bool flag = false;
						object[] array;
						checked
						{
							try
							{
								num3 = tables._buckets.Length * 2 + 1;
								while (num3 % 3 == 0 || num3 % 5 == 0 || num3 % 7 == 0)
								{
									num3 += 2;
								}
								if (num3 > 2146435071)
								{
									flag = true;
								}
							}
							catch (OverflowException)
							{
								flag = true;
							}
							if (flag)
							{
								num3 = 2146435071;
								this._budget = int.MaxValue;
							}
							this.AcquireLocks(1, tables._locks.Length, ref num);
							array = tables._locks;
						}
						if (this._growLockArray && tables._locks.Length < 1024)
						{
							array = new object[tables._locks.Length * 2];
							Array.Copy(tables._locks, 0, array, 0, tables._locks.Length);
							for (int j = tables._locks.Length; j < array.Length; j++)
							{
								array[j] = new object();
							}
						}
						ConcurrentDictionary<TKey, TValue>.Node[] array2 = new ConcurrentDictionary<TKey, TValue>.Node[num3];
						int[] array3 = new int[array.Length];
						for (int k = 0; k < tables._buckets.Length; k++)
						{
							checked
							{
								ConcurrentDictionary<TKey, TValue>.Node next;
								for (ConcurrentDictionary<TKey, TValue>.Node node = tables._buckets[k]; node != null; node = next)
								{
									next = node._next;
									int num4;
									int num5;
									ConcurrentDictionary<TKey, TValue>.GetBucketAndLockNo(node._hashcode, out num4, out num5, array2.Length, array.Length);
									array2[num4] = new ConcurrentDictionary<TKey, TValue>.Node(node._key, node._value, node._hashcode, array2[num4]);
									array3[num5]++;
								}
							}
						}
						this._budget = Math.Max(1, array2.Length / array.Length);
						this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array2, array, array3);
					}
				}
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x00159474 File Offset: 0x00157674
		private static int GetBucket(int hashcode, int bucketCount)
		{
			return (hashcode & int.MaxValue) % bucketCount;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x0015947F File Offset: 0x0015767F
		private static void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
		{
			bucketNo = (hashcode & int.MaxValue) % bucketCount;
			lockNo = bucketNo % lockCount;
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x00159493 File Offset: 0x00157693
		private static int DefaultConcurrencyLevel
		{
			get
			{
				return PlatformHelper.ProcessorCount;
			}
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x0015949C File Offset: 0x0015769C
		private void AcquireAllLocks(ref int locksAcquired)
		{
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentDictionary_AcquiringAllLocks(this._tables._buckets.Length);
			}
			this.AcquireLocks(0, 1, ref locksAcquired);
			this.AcquireLocks(1, this._tables._locks.Length, ref locksAcquired);
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x001594F0 File Offset: 0x001576F0
		private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
		{
			object[] locks = this._tables._locks;
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				bool flag = false;
				try
				{
					Monitor.Enter(locks[i], ref flag);
				}
				finally
				{
					if (flag)
					{
						locksAcquired++;
					}
				}
			}
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00159540 File Offset: 0x00157740
		private void ReleaseLocks(int fromInclusive, int toExclusive)
		{
			for (int i = fromInclusive; i < toExclusive; i++)
			{
				Monitor.Exit(this._tables._locks[i]);
			}
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00159570 File Offset: 0x00157770
		private ReadOnlyCollection<TKey> GetKeys()
		{
			int num = 0;
			ReadOnlyCollection<TKey> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TKey> list = new List<TKey>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._key);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TKey>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00159608 File Offset: 0x00157808
		private ReadOnlyCollection<TValue> GetValues()
		{
			int num = 0;
			ReadOnlyCollection<TValue> readOnlyCollection;
			try
			{
				this.AcquireAllLocks(ref num);
				int countInternal = this.GetCountInternal();
				if (countInternal < 0)
				{
					throw new OutOfMemoryException();
				}
				List<TValue> list = new List<TValue>(countInternal);
				for (int i = 0; i < this._tables._buckets.Length; i++)
				{
					for (ConcurrentDictionary<TKey, TValue>.Node node = this._tables._buckets[i]; node != null; node = node._next)
					{
						list.Add(node._value);
					}
				}
				readOnlyCollection = new ReadOnlyCollection<TValue>(list);
			}
			finally
			{
				this.ReleaseLocks(0, num);
			}
			return readOnlyCollection;
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x001596A0 File Offset: 0x001578A0
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			ConcurrentDictionary<TKey, TValue>.Tables tables = this._tables;
			this._serializationArray = this.ToArray();
			this._serializationConcurrencyLevel = tables._locks.Length;
			this._serializationCapacity = tables._buckets.Length;
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x001596DE File Offset: 0x001578DE
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this._serializationArray = null;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x001596E8 File Offset: 0x001578E8
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			KeyValuePair<TKey, TValue>[] serializationArray = this._serializationArray;
			ConcurrentDictionary<TKey, TValue>.Node[] array = new ConcurrentDictionary<TKey, TValue>.Node[this._serializationCapacity];
			int[] array2 = new int[this._serializationConcurrencyLevel];
			object[] array3 = new object[this._serializationConcurrencyLevel];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = new object();
			}
			this._tables = new ConcurrentDictionary<TKey, TValue>.Tables(array, array3, array2);
			this.InitializeFromCollection(serializationArray);
			this._serializationArray = null;
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x00159759 File Offset: 0x00157959
		// Note: this type is marked as 'beforefieldinit'.
		static ConcurrentDictionary()
		{
		}

		// Token: 0x04003B6A RID: 15210
		[NonSerialized]
		private volatile ConcurrentDictionary<TKey, TValue>.Tables _tables;

		// Token: 0x04003B6B RID: 15211
		private IEqualityComparer<TKey> _comparer;

		// Token: 0x04003B6C RID: 15212
		[NonSerialized]
		private readonly bool _growLockArray;

		// Token: 0x04003B6D RID: 15213
		[NonSerialized]
		private int _budget;

		// Token: 0x04003B6E RID: 15214
		private KeyValuePair<TKey, TValue>[] _serializationArray;

		// Token: 0x04003B6F RID: 15215
		private int _serializationConcurrencyLevel;

		// Token: 0x04003B70 RID: 15216
		private int _serializationCapacity;

		// Token: 0x04003B71 RID: 15217
		private const int DefaultCapacity = 31;

		// Token: 0x04003B72 RID: 15218
		private const int MaxLockNumber = 1024;

		// Token: 0x04003B73 RID: 15219
		private static readonly bool s_isValueWriteAtomic = ConcurrentDictionary<TKey, TValue>.IsValueWriteAtomic();

		// Token: 0x02000AB1 RID: 2737
		private sealed class Tables
		{
			// Token: 0x06006548 RID: 25928 RVA: 0x00159765 File Offset: 0x00157965
			internal Tables(ConcurrentDictionary<TKey, TValue>.Node[] buckets, object[] locks, int[] countPerLock)
			{
				this._buckets = buckets;
				this._locks = locks;
				this._countPerLock = countPerLock;
			}

			// Token: 0x04003B74 RID: 15220
			internal readonly ConcurrentDictionary<TKey, TValue>.Node[] _buckets;

			// Token: 0x04003B75 RID: 15221
			internal readonly object[] _locks;

			// Token: 0x04003B76 RID: 15222
			internal volatile int[] _countPerLock;
		}

		// Token: 0x02000AB2 RID: 2738
		[Serializable]
		private sealed class Node
		{
			// Token: 0x06006549 RID: 25929 RVA: 0x00159784 File Offset: 0x00157984
			internal Node(TKey key, TValue value, int hashcode, ConcurrentDictionary<TKey, TValue>.Node next)
			{
				this._key = key;
				this._value = value;
				this._next = next;
				this._hashcode = hashcode;
			}

			// Token: 0x04003B77 RID: 15223
			internal readonly TKey _key;

			// Token: 0x04003B78 RID: 15224
			internal TValue _value;

			// Token: 0x04003B79 RID: 15225
			internal volatile ConcurrentDictionary<TKey, TValue>.Node _next;

			// Token: 0x04003B7A RID: 15226
			internal readonly int _hashcode;
		}

		// Token: 0x02000AB3 RID: 2739
		[Serializable]
		private sealed class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600654A RID: 25930 RVA: 0x001597AB File Offset: 0x001579AB
			internal DictionaryEnumerator(ConcurrentDictionary<TKey, TValue> dictionary)
			{
				this._enumerator = dictionary.GetEnumerator();
			}

			// Token: 0x17001182 RID: 4482
			// (get) Token: 0x0600654B RID: 25931 RVA: 0x001597C0 File Offset: 0x001579C0
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x17001183 RID: 4483
			// (get) Token: 0x0600654C RID: 25932 RVA: 0x00159804 File Offset: 0x00157A04
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x17001184 RID: 4484
			// (get) Token: 0x0600654D RID: 25933 RVA: 0x0015982C File Offset: 0x00157A2C
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001185 RID: 4485
			// (get) Token: 0x0600654E RID: 25934 RVA: 0x00159851 File Offset: 0x00157A51
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x0600654F RID: 25935 RVA: 0x0015985E File Offset: 0x00157A5E
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06006550 RID: 25936 RVA: 0x0015986B File Offset: 0x00157A6B
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x04003B7B RID: 15227
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}

		// Token: 0x02000AB4 RID: 2740
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__35 : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06006551 RID: 25937 RVA: 0x00159878 File Offset: 0x00157A78
			[DebuggerHidden]
			public <GetEnumerator>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006552 RID: 25938 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006553 RID: 25939 RVA: 0x00159888 File Offset: 0x00157A88
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ConcurrentDictionary<TKey, TValue> concurrentDictionary = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					buckets = concurrentDictionary._tables._buckets;
					i = 0;
					goto IL_00BE;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				current = current._next;
				IL_009F:
				if (current != null)
				{
					this.<>2__current = new KeyValuePair<TKey, TValue>(current._key, current._value);
					this.<>1__state = 1;
					return true;
				}
				current = null;
				int num2 = i;
				i = num2 + 1;
				IL_00BE:
				if (i >= buckets.Length)
				{
					return false;
				}
				current = Volatile.Read<ConcurrentDictionary<TKey, TValue>.Node>(ref buckets[i]);
				goto IL_009F;
			}

			// Token: 0x17001186 RID: 4486
			// (get) Token: 0x06006554 RID: 25940 RVA: 0x00159967 File Offset: 0x00157B67
			KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006555 RID: 25941 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001187 RID: 4487
			// (get) Token: 0x06006556 RID: 25942 RVA: 0x0015996F File Offset: 0x00157B6F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04003B7C RID: 15228
			private int <>1__state;

			// Token: 0x04003B7D RID: 15229
			private KeyValuePair<TKey, TValue> <>2__current;

			// Token: 0x04003B7E RID: 15230
			public ConcurrentDictionary<TKey, TValue> <>4__this;

			// Token: 0x04003B7F RID: 15231
			private ConcurrentDictionary<TKey, TValue>.Node[] <buckets>5__2;

			// Token: 0x04003B80 RID: 15232
			private int <i>5__3;

			// Token: 0x04003B81 RID: 15233
			private ConcurrentDictionary<TKey, TValue>.Node <current>5__4;
		}
	}
}
