using System;

namespace rail
{
	// Token: 0x0200013F RID: 319
	public interface IRailStatisticHelper
	{
		// Token: 0x060017F7 RID: 6135
		IRailPlayerStats CreatePlayerStats(RailID player);

		// Token: 0x060017F8 RID: 6136
		IRailGlobalStats GetGlobalStats();

		// Token: 0x060017F9 RID: 6137
		RailResult AsyncGetNumberOfPlayer(string user_data);
	}
}
