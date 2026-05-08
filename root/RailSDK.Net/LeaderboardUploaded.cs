using System;

namespace rail
{
	// Token: 0x020000ED RID: 237
	public class LeaderboardUploaded : EventBase
	{
		// Token: 0x0600174F RID: 5967 RVA: 0x0000F049 File Offset: 0x0000D249
		public LeaderboardUploaded()
		{
		}

		// Token: 0x04000284 RID: 644
		public int old_rank;

		// Token: 0x04000285 RID: 645
		public string leaderboard_name;

		// Token: 0x04000286 RID: 646
		public double score;

		// Token: 0x04000287 RID: 647
		public bool better_score;

		// Token: 0x04000288 RID: 648
		public int new_rank;
	}
}
