using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Collections.ObjectModel
{
	// Token: 0x02000ADA RID: 2778
	[DebuggerTypeProxy(typeof(CollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public abstract class KeyedCollection<TKey, TItem> : Collection<TItem>
	{
		// Token: 0x06006649 RID: 26185 RVA: 0x0015B909 File Offset: 0x00159B09
		protected KeyedCollection()
			: this(null, 0)
		{
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x0015B913 File Offset: 0x00159B13
		protected KeyedCollection(IEqualityComparer<TKey> comparer)
			: this(comparer, 0)
		{
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x0015B920 File Offset: 0x00159B20
		protected KeyedCollection(IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold)
			: base(new List<TItem>())
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			if (dictionaryCreationThreshold == -1)
			{
				dictionaryCreationThreshold = int.MaxValue;
			}
			if (dictionaryCreationThreshold < -1)
			{
				throw new ArgumentOutOfRangeException("dictionaryCreationThreshold", "The specified threshold for creating dictionary is out of range.");
			}
			this.comparer = comparer;
			this.threshold = dictionaryCreationThreshold;
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x0015B96F File Offset: 0x00159B6F
		private new List<TItem> Items
		{
			get
			{
				return (List<TItem>)base.Items;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x0015B97C File Offset: 0x00159B7C
		public IEqualityComparer<TKey> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170011C3 RID: 4547
		public TItem this[TKey key]
		{
			get
			{
				TItem titem;
				if (this.TryGetValue(key, out titem))
				{
					return titem;
				}
				throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
			}
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x0015B9BC File Offset: 0x00159BBC
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dict != null)
			{
				return this.dict.ContainsKey(key);
			}
			foreach (TItem titem in this.Items)
			{
				if (this.comparer.Equals(this.GetKeyForItem(titem), key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x0015BA4C File Offset: 0x00159C4C
		public bool TryGetValue(TKey key, out TItem item)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dict != null)
			{
				return this.dict.TryGetValue(key, out item);
			}
			foreach (TItem titem in this.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null && this.comparer.Equals(key, keyForItem))
				{
					item = titem;
					return true;
				}
			}
			item = default(TItem);
			return false;
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x0015BAF8 File Offset: 0x00159CF8
		private bool ContainsItem(TItem item)
		{
			TKey keyForItem;
			if (this.dict == null || (keyForItem = this.GetKeyForItem(item)) == null)
			{
				return this.Items.Contains(item);
			}
			TItem titem;
			return this.dict.TryGetValue(keyForItem, out titem) && EqualityComparer<TItem>.Default.Equals(titem, item);
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x0015BB48 File Offset: 0x00159D48
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.dict != null)
			{
				TItem titem;
				return this.dict.TryGetValue(key, out titem) && base.Remove(titem);
			}
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.comparer.Equals(this.GetKeyForItem(this.Items[i]), key))
				{
					this.RemoveItem(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x0015BBCA File Offset: 0x00159DCA
		protected IDictionary<TKey, TItem> Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x0015BBD4 File Offset: 0x00159DD4
		protected void ChangeItemKey(TItem item, TKey newKey)
		{
			if (!this.ContainsItem(item))
			{
				throw new ArgumentException("The specified item does not exist in this KeyedCollection.");
			}
			TKey keyForItem = this.GetKeyForItem(item);
			if (!this.comparer.Equals(keyForItem, newKey))
			{
				if (newKey != null)
				{
					this.AddKey(newKey, item);
				}
				if (keyForItem != null)
				{
					this.RemoveKey(keyForItem);
				}
			}
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x0015BC2B File Offset: 0x00159E2B
		protected override void ClearItems()
		{
			base.ClearItems();
			if (this.dict != null)
			{
				this.dict.Clear();
			}
			this.keyCount = 0;
		}

		// Token: 0x06006656 RID: 26198
		protected abstract TKey GetKeyForItem(TItem item);

		// Token: 0x06006657 RID: 26199 RVA: 0x0015BC50 File Offset: 0x00159E50
		protected override void InsertItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			if (keyForItem != null)
			{
				this.AddKey(keyForItem, item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x0015BC80 File Offset: 0x00159E80
		protected override void RemoveItem(int index)
		{
			TKey keyForItem = this.GetKeyForItem(this.Items[index]);
			if (keyForItem != null)
			{
				this.RemoveKey(keyForItem);
			}
			base.RemoveItem(index);
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x0015BCB8 File Offset: 0x00159EB8
		protected override void SetItem(int index, TItem item)
		{
			TKey keyForItem = this.GetKeyForItem(item);
			TKey keyForItem2 = this.GetKeyForItem(this.Items[index]);
			if (this.comparer.Equals(keyForItem2, keyForItem))
			{
				if (keyForItem != null && this.dict != null)
				{
					this.dict[keyForItem] = item;
				}
			}
			else
			{
				if (keyForItem != null)
				{
					this.AddKey(keyForItem, item);
				}
				if (keyForItem2 != null)
				{
					this.RemoveKey(keyForItem2);
				}
			}
			base.SetItem(index, item);
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x0015BD38 File Offset: 0x00159F38
		private void AddKey(TKey key, TItem item)
		{
			if (this.dict != null)
			{
				this.dict.Add(key, item);
				return;
			}
			if (this.keyCount == this.threshold)
			{
				this.CreateDictionary();
				this.dict.Add(key, item);
				return;
			}
			if (this.Contains(key))
			{
				throw new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
			}
			this.keyCount++;
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x0015BDAC File Offset: 0x00159FAC
		private void CreateDictionary()
		{
			this.dict = new Dictionary<TKey, TItem>(this.comparer);
			foreach (TItem titem in this.Items)
			{
				TKey keyForItem = this.GetKeyForItem(titem);
				if (keyForItem != null)
				{
					this.dict.Add(keyForItem, titem);
				}
			}
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x0015BE28 File Offset: 0x0015A028
		private void RemoveKey(TKey key)
		{
			if (this.dict != null)
			{
				this.dict.Remove(key);
				return;
			}
			this.keyCount--;
		}

		// Token: 0x04003BD9 RID: 15321
		private const int defaultThreshold = 0;

		// Token: 0x04003BDA RID: 15322
		private readonly IEqualityComparer<TKey> comparer;

		// Token: 0x04003BDB RID: 15323
		private Dictionary<TKey, TItem> dict;

		// Token: 0x04003BDC RID: 15324
		private int keyCount;

		// Token: 0x04003BDD RID: 15325
		private readonly int threshold;
	}
}
