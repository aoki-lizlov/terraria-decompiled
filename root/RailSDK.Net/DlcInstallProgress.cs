using System;

namespace rail
{
	// Token: 0x02000080 RID: 128
	public class DlcInstallProgress : EventBase
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x0001088F File Offset: 0x0000EA8F
		public DlcInstallProgress()
		{
		}

		// Token: 0x040000AF RID: 175
		public RailDlcInstallProgress progress = new RailDlcInstallProgress();

		// Token: 0x040000B0 RID: 176
		public RailDlcID dlc_id = new RailDlcID();
	}
}
