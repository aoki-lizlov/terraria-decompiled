using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000097 RID: 151
	public interface IRailFriends
	{
		// Token: 0x06001687 RID: 5767
		RailResult AsyncGetPersonalInfo(List<RailID> rail_ids, string user_data);

		// Token: 0x06001688 RID: 5768
		RailResult AsyncGetFriendMetadata(RailID rail_id, List<string> keys, string user_data);

		// Token: 0x06001689 RID: 5769
		RailResult AsyncSetMyMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x0600168A RID: 5770
		RailResult AsyncClearAllMyMetadata(string user_data);

		// Token: 0x0600168B RID: 5771
		RailResult AsyncSetInviteCommandLine(string command_line, string user_data);

		// Token: 0x0600168C RID: 5772
		RailResult AsyncGetInviteCommandLine(RailID rail_id, string user_data);

		// Token: 0x0600168D RID: 5773
		RailResult AsyncReportPlayedWithUserList(List<RailUserPlayedWith> player_list, string user_data);

		// Token: 0x0600168E RID: 5774
		RailResult GetFriendsList(List<RailFriendInfo> friends_list);

		// Token: 0x0600168F RID: 5775
		RailResult AsyncQueryFriendPlayedGamesInfo(RailID rail_id, string user_data);

		// Token: 0x06001690 RID: 5776
		RailResult AsyncQueryPlayedWithFriendsList(string user_data);

		// Token: 0x06001691 RID: 5777
		RailResult AsyncQueryPlayedWithFriendsTime(List<RailID> rail_ids, string user_data);

		// Token: 0x06001692 RID: 5778
		RailResult AsyncQueryPlayedWithFriendsGames(List<RailID> rail_ids, string user_data);

		// Token: 0x06001693 RID: 5779
		RailResult AsyncAddFriend(RailFriendsAddFriendRequest request, string user_data);

		// Token: 0x06001694 RID: 5780
		RailResult AsyncUpdateFriendsData(string user_data);
	}
}
