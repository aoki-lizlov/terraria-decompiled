using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200013B RID: 315
	public class RailSmallObjectStateQueryResult : EventBase
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x00010EBB File Offset: 0x0000F0BB
		public RailSmallObjectStateQueryResult()
		{
		}

		// Token: 0x040004BD RID: 1213
		public List<RailSmallObjectState> objects_state = new List<RailSmallObjectState>();
	}
}
