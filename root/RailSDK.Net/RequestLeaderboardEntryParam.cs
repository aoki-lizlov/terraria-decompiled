using System;

namespace rail
{
	// Token: 0x020000EF RID: 239
	public class RequestLeaderboardEntryParam
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x00002119 File Offset: 0x00000319
		public RequestLeaderboardEntryParam()
		{
		}

		// Token: 0x0400028D RID: 653
		public int range_end;

		// Token: 0x0400028E RID: 654
		public int range_start;

		// Token: 0x0400028F RID: 655
		public LeaderboardType type;

		// Token: 0x04000290 RID: 656
		public bool user_coordinate;
	}
}
