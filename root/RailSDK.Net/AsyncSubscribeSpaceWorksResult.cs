using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000177 RID: 375
	public class AsyncSubscribeSpaceWorksResult : EventBase
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x0001103D File Offset: 0x0000F23D
		public AsyncSubscribeSpaceWorksResult()
		{
		}

		// Token: 0x0400053B RID: 1339
		public List<SpaceWorkID> success_ids = new List<SpaceWorkID>();

		// Token: 0x0400053C RID: 1340
		public List<SpaceWorkID> failure_ids = new List<SpaceWorkID>();

		// Token: 0x0400053D RID: 1341
		public bool subscribe;
	}
}
