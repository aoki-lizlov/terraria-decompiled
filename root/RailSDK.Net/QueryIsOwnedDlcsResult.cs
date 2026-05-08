using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000086 RID: 134
	public class QueryIsOwnedDlcsResult : EventBase
	{
		// Token: 0x0600165B RID: 5723 RVA: 0x0001090C File Offset: 0x0000EB0C
		public QueryIsOwnedDlcsResult()
		{
		}

		// Token: 0x040000BA RID: 186
		public List<RailDlcOwned> dlc_owned_list = new List<RailDlcOwned>();
	}
}
