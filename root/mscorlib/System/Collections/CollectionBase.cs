using System;

namespace System.Collections
{
	// Token: 0x02000A7C RID: 2684
	[Serializable]
	public abstract class CollectionBase : IList, ICollection, IEnumerable
	{
		// Token: 0x060061E1 RID: 25057 RVA: 0x0014E56D File Offset: 0x0014C76D
		protected CollectionBase()
		{
			this._list = new ArrayList();
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x0014E580 File Offset: 0x0014C780
		protected CollectionBase(int capacity)
		{
			this._list = new ArrayList(capacity);
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x0014E594 File Offset: 0x0014C794
		protected ArrayList InnerList
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060061E4 RID: 25060 RVA: 0x000025CE File Offset: 0x000007CE
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0014E59C File Offset: 0x0014C79C
		// (set) Token: 0x060061E6 RID: 25062 RVA: 0x0014E5A9 File Offset: 0x0014C7A9
		public int Capacity
		{
			get
			{
				return this.InnerList.Capacity;
			}
			set
			{
				this.InnerList.Capacity = value;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x0014E5B7 File Offset: 0x0014C7B7
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0014E5C4 File Offset: 0x0014C7C4
		public void Clear()
		{
			this.OnClear();
			this.InnerList.Clear();
			this.OnClearComplete();
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0014E5E0 File Offset: 0x0014C7E0
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			object obj = this.InnerList[index];
			this.OnValidate(obj);
			this.OnRemove(index, obj);
			this.InnerList.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, obj);
			}
			catch
			{
				this.InnerList.Insert(index, obj);
				throw;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060061EA RID: 25066 RVA: 0x0014E65C File Offset: 0x0014C85C
		bool IList.IsReadOnly
		{
			get
			{
				return this.InnerList.IsReadOnly;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x0014E669 File Offset: 0x0014C869
		bool IList.IsFixedSize
		{
			get
			{
				return this.InnerList.IsFixedSize;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060061EC RID: 25068 RVA: 0x0014E676 File Offset: 0x0014C876
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x0014E683 File Offset: 0x0014C883
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0014E690 File Offset: 0x0014C890
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x170010B6 RID: 4278
		object IList.this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				return this.InnerList[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				this.OnValidate(value);
				object obj = this.InnerList[index];
				this.OnSet(index, obj, value);
				this.InnerList[index] = value;
				try
				{
					this.OnSetComplete(index, obj, value);
				}
				catch
				{
					this.InnerList[index] = obj;
					throw;
				}
			}
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x0014E74C File Offset: 0x0014C94C
		bool IList.Contains(object value)
		{
			return this.InnerList.Contains(value);
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x0014E75C File Offset: 0x0014C95C
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.OnInsert(this.InnerList.Count, value);
			int num = this.InnerList.Add(value);
			try
			{
				this.OnInsertComplete(num, value);
			}
			catch
			{
				this.InnerList.RemoveAt(num);
				throw;
			}
			return num;
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x0014E7BC File Offset: 0x0014C9BC
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			int num = this.InnerList.IndexOf(value);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.OnRemove(num, value);
			this.InnerList.RemoveAt(num);
			try
			{
				this.OnRemoveComplete(num, value);
			}
			catch
			{
				this.InnerList.Insert(num, value);
				throw;
			}
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x0014E82C File Offset: 0x0014CA2C
		int IList.IndexOf(object value)
		{
			return this.InnerList.IndexOf(value);
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x0014E83C File Offset: 0x0014CA3C
		void IList.Insert(int index, object value)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			this.OnValidate(value);
			this.OnInsert(index, value);
			this.InnerList.Insert(index, value);
			try
			{
				this.OnInsertComplete(index, value);
			}
			catch
			{
				this.InnerList.RemoveAt(index);
				throw;
			}
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x0014E8AC File Offset: 0x0014CAAC
		public IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnClear()
		{
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x0014E8B9 File Offset: 0x0014CAB9
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnSetComplete(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x060061FD RID: 25085 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x04003AB3 RID: 15027
		private ArrayList _list;
	}
}
