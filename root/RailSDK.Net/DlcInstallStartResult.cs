using System;

namespace rail
{
	// Token: 0x02000082 RID: 130
	public class DlcInstallStartResult : EventBase
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public DlcInstallStartResult()
		{
		}

		// Token: 0x040000B2 RID: 178
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000B3 RID: 179
		public new RailResult result;
	}
}
