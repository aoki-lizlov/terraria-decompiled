using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200054C RID: 1356
	public class GeneralIssueReporter : IProvideReports
	{
		// Token: 0x06003784 RID: 14212 RVA: 0x0062F66D File Offset: 0x0062D86D
		public void AddReport(string textToShow)
		{
			this._reports.Add(new IssueReport(textToShow));
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0062F680 File Offset: 0x0062D880
		public List<IssueReport> GetReports()
		{
			return this._reports;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x0062F688 File Offset: 0x0062D888
		public GeneralIssueReporter()
		{
		}

		// Token: 0x04005BBA RID: 23482
		private List<IssueReport> _reports = new List<IssueReport>();
	}
}
