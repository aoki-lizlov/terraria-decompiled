using System;

namespace rail
{
	// Token: 0x0200007F RID: 127
	public class DlcInstallFinished : EventBase
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0001087C File Offset: 0x0000EA7C
		public DlcInstallFinished()
		{
		}

		// Token: 0x040000AD RID: 173
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000AE RID: 174
		public new RailResult result;
	}
}
