using System;

namespace rail
{
	// Token: 0x02000081 RID: 129
	public class DlcInstallStart : EventBase
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x000108AD File Offset: 0x0000EAAD
		public DlcInstallStart()
		{
		}

		// Token: 0x040000B1 RID: 177
		public RailDlcID dlc_id = new RailDlcID();
	}
}
