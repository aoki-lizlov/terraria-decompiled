using System;

namespace System.Collections
{
	// Token: 0x02000A7D RID: 2685
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06006200 RID: 25088 RVA: 0x0014E8C9 File Offset: 0x0014CAC9
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this._hashtable == null)
				{
					this._hashtable = new Hashtable();
				}
				return this._hashtable;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06006201 RID: 25089 RVA: 0x000025CE File Offset: 0x000007CE
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06006202 RID: 25090 RVA: 0x0014E8E4 File Offset: 0x0014CAE4
		public int Count
		{
			get
			{
				if (this._hashtable != null)
				{
					return this._hashtable.Count;
				}
				return 0;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06006203 RID: 25091 RVA: 0x0014E8FB File Offset: 0x0014CAFB
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x0014E908 File Offset: 0x0014CB08
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06006205 RID: 25093 RVA: 0x0014E915 File Offset: 0x0014CB15
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x0014E922 File Offset: 0x0014CB22
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06006207 RID: 25095 RVA: 0x0014E92F File Offset: 0x0014CB2F
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x0014E93C File Offset: 0x0014CB3C
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x0014E949 File Offset: 0x0014CB49
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		// Token: 0x170010C0 RID: 4288
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x0014EA04 File Offset: 0x0014CC04
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0014EA14 File Offset: 0x0014CC14
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x0014EA68 File Offset: 0x0014CC68
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x0014EA84 File Offset: 0x0014CC84
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object obj = this.InnerHashtable[key];
				this.OnValidate(key, obj);
				this.OnRemove(key, obj);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, obj);
				}
				catch
				{
					this.InnerHashtable.Add(key, obj);
					throw;
				}
			}
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x0014EAF4 File Offset: 0x0014CCF4
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x0014EAF4 File Offset: 0x0014CCF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x000887DF File Offset: 0x000869DF
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnInsert(object key, object value)
		{
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnClear()
		{
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnRemove(object key, object value)
		{
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnValidate(object key, object value)
		{
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x000025BE File Offset: 0x000007BE
		protected DictionaryBase()
		{
		}

		// Token: 0x04003AB4 RID: 15028
		private Hashtable _hashtable;
	}
}
