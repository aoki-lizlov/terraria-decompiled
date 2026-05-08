using System;

namespace System
{
	// Token: 0x020001BB RID: 443
	internal sealed class LocalDataStoreHolder
	{
		// Token: 0x060014B1 RID: 5297 RVA: 0x00052851 File Offset: 0x00050A51
		public LocalDataStoreHolder(LocalDataStore store)
		{
			this.m_Store = store;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00052860 File Offset: 0x00050A60
		protected override void Finalize()
		{
			try
			{
				LocalDataStore store = this.m_Store;
				if (store != null)
				{
					store.Dispose();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00052898 File Offset: 0x00050A98
		public LocalDataStore Store
		{
			get
			{
				return this.m_Store;
			}
		}

		// Token: 0x040013CC RID: 5068
		private LocalDataStore m_Store;
	}
}
