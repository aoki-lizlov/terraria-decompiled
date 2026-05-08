using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000065 RID: 101
	internal class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IWrappedDictionary, IDictionary, ICollection
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00014E09 File Offset: 0x00013009
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00014E23 File Offset: 0x00013023
		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00014E3D File Offset: 0x0001303D
		public void Add(TKey key, TValue value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00014E7A File Offset: 0x0001307A
		public bool ContainsKey(TKey key)
		{
			if (this._dictionary != null)
			{
				return this._dictionary.Contains(key);
			}
			return this._genericDictionary.ContainsKey(key);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00014EA2 File Offset: 0x000130A2
		public ICollection<TKey> Keys
		{
			get
			{
				if (this._dictionary != null)
				{
					return Enumerable.ToList<TKey>(Enumerable.Cast<TKey>(this._dictionary.Keys));
				}
				return this._genericDictionary.Keys;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00014ECD File Offset: 0x000130CD
		public bool Remove(TKey key)
		{
			if (this._dictionary == null)
			{
				return this._genericDictionary.Remove(key);
			}
			if (this._dictionary.Contains(key))
			{
				this._dictionary.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014F0C File Offset: 0x0001310C
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this._dictionary == null)
			{
				return this._genericDictionary.TryGetValue(key, ref value);
			}
			if (!this._dictionary.Contains(key))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)this._dictionary[key]);
			return true;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00014F68 File Offset: 0x00013168
		public ICollection<TValue> Values
		{
			get
			{
				if (this._dictionary != null)
				{
					return Enumerable.ToList<TValue>(Enumerable.Cast<TValue>(this._dictionary.Values));
				}
				return this._genericDictionary.Values;
			}
		}

		// Token: 0x1700010F RID: 271
		public TValue this[TKey key]
		{
			get
			{
				if (this._dictionary != null)
				{
					return (TValue)((object)this._dictionary[key]);
				}
				return this._genericDictionary[key];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				this._genericDictionary[key] = value;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014FEF File Offset: 0x000131EF
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				((IList)this._dictionary).Add(item);
				return;
			}
			IDictionary<TKey, TValue> genericDictionary = this._genericDictionary;
			if (genericDictionary == null)
			{
				return;
			}
			genericDictionary.Add(item);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015022 File Offset: 0x00013222
		public void Clear()
		{
			if (this._dictionary != null)
			{
				this._dictionary.Clear();
				return;
			}
			this._genericDictionary.Clear();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015043 File Offset: 0x00013243
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary != null)
			{
				return ((IList)this._dictionary).Contains(item);
			}
			return this._genericDictionary.Contains(item);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00015070 File Offset: 0x00013270
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._dictionary != null)
			{
				using (IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DictionaryEntry entry = enumerator.Entry;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)entry.Key), (TValue)((object)entry.Value));
					}
					return;
				}
			}
			this._genericDictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000150FC File Offset: 0x000132FC
		public int Count
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.Count;
				}
				return this._genericDictionary.Count;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001511D File Offset: 0x0001331D
		public bool IsReadOnly
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary.IsReadOnly;
				}
				return this._genericDictionary.IsReadOnly;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015140 File Offset: 0x00013340
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this._dictionary == null)
			{
				return this._genericDictionary.Remove(item);
			}
			if (!this._dictionary.Contains(item.Key))
			{
				return true;
			}
			if (object.Equals(this._dictionary[item.Key], item.Value))
			{
				this._dictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000151C4 File Offset: 0x000133C4
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return Enumerable.Select<DictionaryEntry, KeyValuePair<TKey, TValue>>(Enumerable.Cast<DictionaryEntry>(this._dictionary), (DictionaryEntry de) => new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
			}
			return this._genericDictionary.GetEnumerator();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015219 File Offset: 0x00013419
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015221 File Offset: 0x00013421
		void IDictionary.Add(object key, object value)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Add(key, value);
				return;
			}
			this._genericDictionary.Add((TKey)((object)key), (TValue)((object)value));
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00015250 File Offset: 0x00013450
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0001527D File Offset: 0x0001347D
		object IDictionary.Item
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return this._genericDictionary[(TKey)((object)key)];
			}
			set
			{
				if (this._dictionary != null)
				{
					this._dictionary[key] = value;
					return;
				}
				this._genericDictionary[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000152AC File Offset: 0x000134AC
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._dictionary != null)
			{
				return this._dictionary.GetEnumerator();
			}
			return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._genericDictionary.GetEnumerator());
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000152D7 File Offset: 0x000134D7
		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000152FF File Offset: 0x000134FF
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsFixedSize;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00015316 File Offset: 0x00013516
		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return Enumerable.ToList<TKey>(this._genericDictionary.Keys);
				}
				return this._dictionary.Keys;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001533C File Offset: 0x0001353C
		public void Remove(object key)
		{
			if (this._dictionary != null)
			{
				this._dictionary.Remove(key);
				return;
			}
			this._genericDictionary.Remove((TKey)((object)key));
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00015365 File Offset: 0x00013565
		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return Enumerable.ToList<TValue>(this._genericDictionary.Values);
				}
				return this._dictionary.Values;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001538B File Offset: 0x0001358B
		void ICollection.CopyTo(Array array, int index)
		{
			if (this._dictionary != null)
			{
				this._dictionary.CopyTo(array, index);
				return;
			}
			this._genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000153B5 File Offset: 0x000135B5
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._dictionary != null && this._dictionary.IsSynchronized;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000153CC File Offset: 0x000135CC
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000153EE File Offset: 0x000135EE
		public object UnderlyingDictionary
		{
			get
			{
				if (this._dictionary != null)
				{
					return this._dictionary;
				}
				return this._genericDictionary;
			}
		}

		// Token: 0x04000254 RID: 596
		private readonly IDictionary _dictionary;

		// Token: 0x04000255 RID: 597
		private readonly IDictionary<TKey, TValue> _genericDictionary;

		// Token: 0x04000256 RID: 598
		private object _syncRoot;

		// Token: 0x02000134 RID: 308
		private struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000CC5 RID: 3269 RVA: 0x000310F8 File Offset: 0x0002F2F8
			public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0003110C File Offset: 0x0002F30C
			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0003111C File Offset: 0x0002F31C
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00031138 File Offset: 0x0002F338
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00031154 File Offset: 0x0002F354
			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this._e.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x0003119B File Offset: 0x0002F39B
			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x000311A8 File Offset: 0x0002F3A8
			public void Reset()
			{
				this._e.Reset();
			}

			// Token: 0x04000493 RID: 1171
			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}

		// Token: 0x02000135 RID: 309
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000CCC RID: 3276 RVA: 0x000311B5 File Offset: 0x0002F3B5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x000311C1 File Offset: 0x0002F3C1
			internal KeyValuePair<TKey, TValue> <GetEnumerator>b__25_0(DictionaryEntry de)
			{
				return new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value));
			}

			// Token: 0x04000494 RID: 1172
			public static readonly DictionaryWrapper<TKey, TValue>.<>c <>9 = new DictionaryWrapper<TKey, TValue>.<>c();

			// Token: 0x04000495 RID: 1173
			public static Func<DictionaryEntry, KeyValuePair<TKey, TValue>> <>9__25_0;
		}
	}
}
