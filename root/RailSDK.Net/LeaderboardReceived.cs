using System;

namespace rail
{
	// Token: 0x020000EA RID: 234
	public class LeaderboardReceived : EventBase
	{
		// Token: 0x0600174E RID: 5966 RVA: 0x0000F049 File Offset: 0x0000D249
		public LeaderboardReceived()
		{
		}

		// Token: 0x04000278 RID: 632
		public string leaderboard_name;

		// Token: 0x04000279 RID: 633
		public bool does_exist;
	}
}
