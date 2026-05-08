using System;

namespace System.Collections
{
	// Token: 0x02000A83 RID: 2691
	[Serializable]
	public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06006247 RID: 25159 RVA: 0x0014F64B File Offset: 0x0014D84B
		protected ArrayList InnerList
		{
			get
			{
				if (this._list == null)
				{
					this._list = new ArrayList();
				}
				return this._list;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x0014F666 File Offset: 0x0014D866
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06006249 RID: 25161 RVA: 0x0014F673 File Offset: 0x0014D873
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600624A RID: 25162 RVA: 0x0014F680 File Offset: 0x0014D880
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x000BC349 File Offset: 0x000BA549
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x0014F68D File Offset: 0x0014D88D
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x000025BE File Offset: 0x000007BE
		protected ReadOnlyCollectionBase()
		{
		}

		// Token: 0x04003AC7 RID: 15047
		private ArrayList _list;
	}
}
