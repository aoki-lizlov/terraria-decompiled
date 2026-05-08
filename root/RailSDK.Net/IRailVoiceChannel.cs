using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000198 RID: 408
	public interface IRailVoiceChannel : IRailComponent
	{
		// Token: 0x060018C1 RID: 6337
		RailVoiceChannelID GetVoiceChannelID();

		// Token: 0x060018C2 RID: 6338
		string GetVoiceChannelName();

		// Token: 0x060018C3 RID: 6339
		EnumRailVoiceChannelJoinState GetJoinState();

		// Token: 0x060018C4 RID: 6340
		RailResult AsyncJoinVoiceChannel(string user_data);

		// Token: 0x060018C5 RID: 6341
		RailResult AsyncLeaveVoiceChannel(string user_data);

		// Token: 0x060018C6 RID: 6342
		RailResult GetUsers(List<RailID> user_list);

		// Token: 0x060018C7 RID: 6343
		RailResult AsyncAddUsers(List<RailID> user_list, string user_data);

		// Token: 0x060018C8 RID: 6344
		RailResult AsyncRemoveUsers(List<RailID> user_list, string user_data);

		// Token: 0x060018C9 RID: 6345
		RailResult CloseChannel();

		// Token: 0x060018CA RID: 6346
		RailResult SetSelfSpeaking(bool speaking);

		// Token: 0x060018CB RID: 6347
		bool IsSelfSpeaking();

		// Token: 0x060018CC RID: 6348
		RailResult AsyncSetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state, string user_data);

		// Token: 0x060018CD RID: 6349
		RailResult GetUsersSpeakingState(List<RailVoiceChannelUserSpeakingState> users_speaking_state);

		// Token: 0x060018CE RID: 6350
		RailResult GetSpeakingUsers(List<RailID> speaking_users, List<RailID> not_speaking_users);

		// Token: 0x060018CF RID: 6351
		bool IsOwner();
	}
}
