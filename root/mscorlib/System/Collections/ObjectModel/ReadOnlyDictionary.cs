using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000ADB RID: 2779
	[DebuggerTypeProxy(typeof(DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x0600665D RID: 26205 RVA: 0x0015BE4E File Offset: 0x0015A04E
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x0600665E RID: 26206 RVA: 0x0015BE6B File Offset: 0x0015A06B
		protected IDictionary<TKey, TValue> Dictionary
		{
			get
			{
				return this.m_dictionary;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600665F RID: 26207 RVA: 0x0015BE73 File Offset: 0x0015A073
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06006660 RID: 26208 RVA: 0x0015BE99 File Offset: 0x0015A099
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				if (this._values == null)
				{
					this._values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this._values;
			}
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x0015BEBF File Offset: 0x0015A0BF
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x0015BECD File Offset: 0x0015A0CD
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0015BED5 File Offset: 0x0015A0D5
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06006664 RID: 26212 RVA: 0x0015BEE4 File Offset: 0x0015A0E4
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170011CA RID: 4554
		public TValue this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x001545BD File Offset: 0x001527BD
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x001545BD File Offset: 0x001527BD
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x170011CB RID: 4555
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				return this.m_dictionary[key];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x0015BEFA File Offset: 0x0015A0FA
		public int Count
		{
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x0015BF07 File Offset: 0x0015A107
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x0015BF15 File Offset: 0x0015A115
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x001545BD File Offset: 0x001527BD
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x001545BD File Offset: 0x001527BD
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x001545BD File Offset: 0x001527BD
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x0015BF24 File Offset: 0x0015A124
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0015BF31 File Offset: 0x0015A131
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x0015BF3E File Offset: 0x0015A13E
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return key is TKey;
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x001545BD File Offset: 0x001527BD
		void IDictionary.Add(object key, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x001545BD File Offset: 0x001527BD
		void IDictionary.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x0015BF57 File Offset: 0x0015A157
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0015BF70 File Offset: 0x0015A170
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006678 RID: 26232 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool IDictionary.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006679 RID: 26233 RVA: 0x00003FB7 File Offset: 0x000021B7
		bool IDictionary.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x0015BECD File Offset: 0x0015A0CD
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x001545BD File Offset: 0x001527BD
		void IDictionary.Remove(object key)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600667C RID: 26236 RVA: 0x0015BEE4 File Offset: 0x0015A0E4
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x170011D2 RID: 4562
		object IDictionary.this[object key]
		{
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x0015BFC0 File Offset: 0x0015A1C0
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
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.");
			}
			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06006680 RID: 26240 RVA: 0x0000408A File Offset: 0x0000228A
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06006681 RID: 26241 RVA: 0x0015C148 File Offset: 0x0015A348
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this._syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
					}
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x0015BECD File Offset: 0x0015A0CD
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06006683 RID: 26243 RVA: 0x0015BEE4 File Offset: 0x0015A0E4
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x04003BDE RID: 15326
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x04003BDF RID: 15327
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003BE0 RID: 15328
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection _keys;

		// Token: 0x04003BE1 RID: 15329
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection _values;

		// Token: 0x02000ADC RID: 2780
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006684 RID: 26244 RVA: 0x0015C192 File Offset: 0x0015A392
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this._dictionary = dictionary;
				this._enumerator = this._dictionary.GetEnumerator();
			}

			// Token: 0x170011D7 RID: 4567
			// (get) Token: 0x06006685 RID: 26245 RVA: 0x0015C1AC File Offset: 0x0015A3AC
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

			// Token: 0x170011D8 RID: 4568
			// (get) Token: 0x06006686 RID: 26246 RVA: 0x0015C1F0 File Offset: 0x0015A3F0
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170011D9 RID: 4569
			// (get) Token: 0x06006687 RID: 26247 RVA: 0x0015C218 File Offset: 0x0015A418
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this._enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x170011DA RID: 4570
			// (get) Token: 0x06006688 RID: 26248 RVA: 0x0015C23D File Offset: 0x0015A43D
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006689 RID: 26249 RVA: 0x0015C24A File Offset: 0x0015A44A
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x0600668A RID: 26250 RVA: 0x0015C257 File Offset: 0x0015A457
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x04003BE2 RID: 15330
			private readonly IDictionary<TKey, TValue> _dictionary;

			// Token: 0x04003BE3 RID: 15331
			private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;
		}

		// Token: 0x02000ADD RID: 2781
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x0600668B RID: 26251 RVA: 0x0015C264 File Offset: 0x0015A464
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x0600668C RID: 26252 RVA: 0x001545BD File Offset: 0x001527BD
			void ICollection<TKey>.Add(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600668D RID: 26253 RVA: 0x001545BD File Offset: 0x001527BD
			void ICollection<TKey>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600668E RID: 26254 RVA: 0x0015C281 File Offset: 0x0015A481
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this._collection.Contains(item);
			}

			// Token: 0x0600668F RID: 26255 RVA: 0x0015C28F File Offset: 0x0015A48F
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			// Token: 0x170011DB RID: 4571
			// (get) Token: 0x06006690 RID: 26256 RVA: 0x0015C29E File Offset: 0x0015A49E
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x170011DC RID: 4572
			// (get) Token: 0x06006691 RID: 26257 RVA: 0x00003FB7 File Offset: 0x000021B7
			bool ICollection<TKey>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006692 RID: 26258 RVA: 0x001545BD File Offset: 0x001527BD
			bool ICollection<TKey>.Remove(TKey item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x06006693 RID: 26259 RVA: 0x0015C2AB File Offset: 0x0015A4AB
			public IEnumerator<TKey> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x06006694 RID: 26260 RVA: 0x0015C2B8 File Offset: 0x0015A4B8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x06006695 RID: 26261 RVA: 0x0015C2C5 File Offset: 0x0015A4C5
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this._collection, array, index);
			}

			// Token: 0x170011DD RID: 4573
			// (get) Token: 0x06006696 RID: 26262 RVA: 0x0000408A File Offset: 0x0000228A
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011DE RID: 4574
			// (get) Token: 0x06006697 RID: 26263 RVA: 0x0015C2D4 File Offset: 0x0015A4D4
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x04003BE4 RID: 15332
			private readonly ICollection<TKey> _collection;

			// Token: 0x04003BE5 RID: 15333
			[NonSerialized]
			private object _syncRoot;
		}

		// Token: 0x02000ADE RID: 2782
		[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006698 RID: 26264 RVA: 0x0015C31E File Offset: 0x0015A51E
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._collection = collection;
			}

			// Token: 0x06006699 RID: 26265 RVA: 0x001545BD File Offset: 0x001527BD
			void ICollection<TValue>.Add(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600669A RID: 26266 RVA: 0x001545BD File Offset: 0x001527BD
			void ICollection<TValue>.Clear()
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x0600669B RID: 26267 RVA: 0x0015C33B File Offset: 0x0015A53B
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this._collection.Contains(item);
			}

			// Token: 0x0600669C RID: 26268 RVA: 0x0015C349 File Offset: 0x0015A549
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this._collection.CopyTo(array, arrayIndex);
			}

			// Token: 0x170011DF RID: 4575
			// (get) Token: 0x0600669D RID: 26269 RVA: 0x0015C358 File Offset: 0x0015A558
			public int Count
			{
				get
				{
					return this._collection.Count;
				}
			}

			// Token: 0x170011E0 RID: 4576
			// (get) Token: 0x0600669E RID: 26270 RVA: 0x00003FB7 File Offset: 0x000021B7
			bool ICollection<TValue>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600669F RID: 26271 RVA: 0x001545BD File Offset: 0x001527BD
			bool ICollection<TValue>.Remove(TValue item)
			{
				throw new NotSupportedException("Collection is read-only.");
			}

			// Token: 0x060066A0 RID: 26272 RVA: 0x0015C365 File Offset: 0x0015A565
			public IEnumerator<TValue> GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060066A1 RID: 26273 RVA: 0x0015C372 File Offset: 0x0015A572
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._collection.GetEnumerator();
			}

			// Token: 0x060066A2 RID: 26274 RVA: 0x0015C37F File Offset: 0x0015A57F
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this._collection, array, index);
			}

			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x060066A3 RID: 26275 RVA: 0x0000408A File Offset: 0x0000228A
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011E2 RID: 4578
			// (get) Token: 0x060066A4 RID: 26276 RVA: 0x0015C390 File Offset: 0x0015A590
			object ICollection.SyncRoot
			{
				get
				{
					if (this._syncRoot == null)
					{
						ICollection collection = this._collection as ICollection;
						if (collection != null)
						{
							this._syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
						}
					}
					return this._syncRoot;
				}
			}

			// Token: 0x04003BE6 RID: 15334
			private readonly ICollection<TValue> _collection;

			// Token: 0x04003BE7 RID: 15335
			[NonSerialized]
			private object _syncRoot;
		}
	}
}
