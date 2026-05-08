using System;

namespace rail
{
	// Token: 0x02000083 RID: 131
	public class DlcOwnershipChanged : EventBase
	{
		// Token: 0x06001658 RID: 5720 RVA: 0x000108D3 File Offset: 0x0000EAD3
		public DlcOwnershipChanged()
		{
		}

		// Token: 0x040000B4 RID: 180
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000B5 RID: 181
		public bool is_active;
	}
}
