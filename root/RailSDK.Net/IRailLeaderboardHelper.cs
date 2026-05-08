using System;

namespace rail
{
	// Token: 0x020000E2 RID: 226
	public interface IRailLeaderboardHelper
	{
		// Token: 0x06001746 RID: 5958
		IRailLeaderboard OpenLeaderboard(string leaderboard_name);

		// Token: 0x06001747 RID: 5959
		IRailLeaderboard AsyncCreateLeaderboard(string leaderboard_name, LeaderboardSortType sort_type, LeaderboardDisplayType display_type, string user_data, out RailResult result);
	}
}
