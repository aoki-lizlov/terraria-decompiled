using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001BE RID: 446
	[ComVisible(true)]
	public sealed class LocalDataStoreSlot
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x00052AB8 File Offset: 0x00050CB8
		internal LocalDataStoreSlot(LocalDataStoreMgr mgr, int slot, long cookie)
		{
			this.m_mgr = mgr;
			this.m_slot = slot;
			this.m_cookie = cookie;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00052AD5 File Offset: 0x00050CD5
		internal LocalDataStoreMgr Manager
		{
			get
			{
				return this.m_mgr;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x00052ADD File Offset: 0x00050CDD
		internal int Slot
		{
			get
			{
				return this.m_slot;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00052AE5 File Offset: 0x00050CE5
		internal long Cookie
		{
			get
			{
				return this.m_cookie;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00052AF0 File Offset: 0x00050CF0
		protected override void Finalize()
		{
			try
			{
				LocalDataStoreMgr mgr = this.m_mgr;
				if (mgr != null)
				{
					int slot = this.m_slot;
					this.m_slot = -1;
					mgr.FreeDataSlot(slot, this.m_cookie);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040013D1 RID: 5073
		private LocalDataStoreMgr m_mgr;

		// Token: 0x040013D2 RID: 5074
		private int m_slot;

		// Token: 0x040013D3 RID: 5075
		private long m_cookie;
	}
}
