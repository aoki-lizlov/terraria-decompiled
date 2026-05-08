using System;

namespace rail
{
	// Token: 0x020000E3 RID: 227
	public class LeaderboardAttachSpaceWork : EventBase
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x00010C5A File Offset: 0x0000EE5A
		public LeaderboardAttachSpaceWork()
		{
		}

		// Token: 0x04000268 RID: 616
		public string leaderboard_name;

		// Token: 0x04000269 RID: 617
		public SpaceWorkID spacework_id = new SpaceWorkID();
	}
}
