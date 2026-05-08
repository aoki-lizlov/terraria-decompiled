using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200054B RID: 1355
	public class IssueReport
	{
		// Token: 0x06003783 RID: 14211 RVA: 0x0062F653 File Offset: 0x0062D853
		public IssueReport(string reportText)
		{
			this.timeReported = DateTime.Now;
			this.reportText = reportText;
		}

		// Token: 0x04005BB8 RID: 23480
		public DateTime timeReported;

		// Token: 0x04005BB9 RID: 23481
		public string reportText;
	}
}
