using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000160 RID: 352
	public interface IRailUsersHelper
	{
		// Token: 0x0600183D RID: 6205
		RailResult AsyncGetUsersInfo(List<RailID> rail_ids, string user_data);

		// Token: 0x0600183E RID: 6206
		RailResult AsyncInviteUsers(string command_line, List<RailID> users, RailInviteOptions options, string user_data);

		// Token: 0x0600183F RID: 6207
		RailResult AsyncGetInviteDetail(RailID inviter, EnumRailUsersInviteType invite_type, string user_data);

		// Token: 0x06001840 RID: 6208
		RailResult AsyncCancelInvite(EnumRailUsersInviteType invite_type, string user_data);

		// Token: 0x06001841 RID: 6209
		RailResult AsyncCancelAllInvites(string user_data);

		// Token: 0x06001842 RID: 6210
		RailResult AsyncGetUserLimits(RailID user_id, string user_data);

		// Token: 0x06001843 RID: 6211
		RailResult AsyncShowChatWindowWithFriend(RailID rail_id, string user_data);

		// Token: 0x06001844 RID: 6212
		RailResult AsyncShowUserHomepageWindow(RailID rail_id, string user_data);
	}
}
