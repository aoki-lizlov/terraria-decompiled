using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000173 RID: 371
	public class AsyncModifyFavoritesWorksResult : EventBase
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		public AsyncModifyFavoritesWorksResult()
		{
		}

		// Token: 0x04000533 RID: 1331
		public List<SpaceWorkID> success_ids = new List<SpaceWorkID>();

		// Token: 0x04000534 RID: 1332
		public List<SpaceWorkID> failure_ids = new List<SpaceWorkID>();

		// Token: 0x04000535 RID: 1333
		public EnumRailModifyFavoritesSpaceWorkType modify_flag;
	}
}
