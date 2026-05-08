using System;

namespace rail
{
	// Token: 0x020000E7 RID: 231
	public class LeaderboardEntry
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x00010C80 File Offset: 0x0000EE80
		public LeaderboardEntry()
		{
		}

		// Token: 0x04000274 RID: 628
		public RailID player_id = new RailID();

		// Token: 0x04000275 RID: 629
		public LeaderboardData data = new LeaderboardData();
	}
}
