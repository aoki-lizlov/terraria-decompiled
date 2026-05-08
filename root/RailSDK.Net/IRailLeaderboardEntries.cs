using System;

namespace rail
{
	// Token: 0x020000E1 RID: 225
	public interface IRailLeaderboardEntries : IRailComponent
	{
		// Token: 0x06001740 RID: 5952
		RailID GetRailID();

		// Token: 0x06001741 RID: 5953
		string GetLeaderboardName();

		// Token: 0x06001742 RID: 5954
		RailResult AsyncRequestLeaderboardEntries(RailID player, RequestLeaderboardEntryParam param, string user_data);

		// Token: 0x06001743 RID: 5955
		RequestLeaderboardEntryParam GetEntriesParam();

		// Token: 0x06001744 RID: 5956
		int GetEntriesCount();

		// Token: 0x06001745 RID: 5957
		RailResult GetLeaderboardEntry(int index, LeaderboardEntry leaderboard_entry);
	}
}
