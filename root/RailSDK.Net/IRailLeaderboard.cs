using System;

namespace rail
{
	// Token: 0x020000E0 RID: 224
	public interface IRailLeaderboard : IRailComponent
	{
		// Token: 0x06001737 RID: 5943
		string GetLeaderboardName();

		// Token: 0x06001738 RID: 5944
		int GetTotalEntriesCount();

		// Token: 0x06001739 RID: 5945
		RailResult AsyncGetLeaderboard(string user_data);

		// Token: 0x0600173A RID: 5946
		RailResult GetLeaderboardParameters(LeaderboardParameters param);

		// Token: 0x0600173B RID: 5947
		IRailLeaderboardEntries CreateLeaderboardEntries();

		// Token: 0x0600173C RID: 5948
		RailResult AsyncUploadLeaderboard(UploadLeaderboardParam update_param, string user_data);

		// Token: 0x0600173D RID: 5949
		RailResult GetLeaderboardSortType(out int sort_type);

		// Token: 0x0600173E RID: 5950
		RailResult GetLeaderboardDisplayType(out int display_type);

		// Token: 0x0600173F RID: 5951
		RailResult AsyncAttachSpaceWork(SpaceWorkID spacework_id, string user_data);
	}
}
