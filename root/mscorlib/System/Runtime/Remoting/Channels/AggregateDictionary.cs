using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000573 RID: 1395
	[ComVisible(true)]
	internal class AggregateDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003794 RID: 14228 RVA: 0x000C8BE8 File Offset: 0x000C6DE8
		public AggregateDictionary(IDictionary[] dics)
		{
			this.dictionaries = dics;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007D3 RID: 2003
		public object this[object key]
		{
			get
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						return dictionary[key];
					}
				}
				return null;
			}
			set
			{
				foreach (IDictionary dictionary in this.dictionaries)
				{
					if (dictionary.Contains(key))
					{
						dictionary[key] = value;
					}
				}
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000C8C68 File Offset: 0x000C6E68
		public ICollection Keys
		{
			get
			{
				if (this._keys != null)
				{
					return this._keys;
				}
				this._keys = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					this._keys.AddRange(dictionary.Keys);
				}
				return this._keys;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000C8CC0 File Offset: 0x000C6EC0
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (IDictionary dictionary in this.dictionaries)
				{
					arrayList.AddRange(dictionary.Values);
				}
				return arrayList;
			}
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x00047E00 File Offset: 0x00046000
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x00047E00 File Offset: 0x00046000
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000C8CFC File Offset: 0x000C6EFC
		public bool Contains(object ob)
		{
			IDictionary[] array = this.dictionaries;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(ob))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000C8D2C File Offset: 0x000C6F2C
		public IDictionaryEnumerator GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000C8D2C File Offset: 0x000C6F2C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AggregateEnumerator(this.dictionaries);
		}

		// Token: 0x060037A0 RID: 14240 RVA: 0x00047E00 File Offset: 0x00046000
		public void Remove(object ob)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x000C8D3C File Offset: 0x000C6F3C
		public void CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				array.SetValue(obj, index++);
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000C8D94 File Offset: 0x000C6F94
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IDictionary dictionary in this.dictionaries)
				{
					num += dictionary.Count;
				}
				return num;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400254C RID: 9548
		private IDictionary[] dictionaries;

		// Token: 0x0400254D RID: 9549
		private ArrayList _keys;
	}
}
