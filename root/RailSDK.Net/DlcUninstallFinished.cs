using System;

namespace rail
{
	// Token: 0x02000085 RID: 133
	public class DlcUninstallFinished : EventBase
	{
		// Token: 0x0600165A RID: 5722 RVA: 0x000108F9 File Offset: 0x0000EAF9
		public DlcUninstallFinished()
		{
		}

		// Token: 0x040000B8 RID: 184
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000B9 RID: 185
		public new RailResult result;
	}
}
