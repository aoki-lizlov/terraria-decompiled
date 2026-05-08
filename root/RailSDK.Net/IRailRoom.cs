using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200010C RID: 268
	public interface IRailRoom : IRailComponent
	{
		// Token: 0x0600178F RID: 6031
		ulong GetRoomId();

		// Token: 0x06001790 RID: 6032
		RailResult GetRoomName(out string name);

		// Token: 0x06001791 RID: 6033
		ulong GetZoneId();

		// Token: 0x06001792 RID: 6034
		RailID GetOwnerId();

		// Token: 0x06001793 RID: 6035
		RailResult GetHasPassword(out bool has_password);

		// Token: 0x06001794 RID: 6036
		EnumRoomType GetRoomType();

		// Token: 0x06001795 RID: 6037
		bool SetNewOwner(RailID new_owner_id);

		// Token: 0x06001796 RID: 6038
		RailResult AsyncGetRoomMembers(string user_data);

		// Token: 0x06001797 RID: 6039
		void Leave();

		// Token: 0x06001798 RID: 6040
		RailResult AsyncJoinRoom(string password, string user_data);

		// Token: 0x06001799 RID: 6041
		RailResult AsyncGetAllRoomData(string user_data);

		// Token: 0x0600179A RID: 6042
		RailResult AsyncKickOffMember(RailID member_id, string user_data);

		// Token: 0x0600179B RID: 6043
		bool GetRoomMetadata(string key, out string value);

		// Token: 0x0600179C RID: 6044
		bool SetRoomMetadata(string key, string value);

		// Token: 0x0600179D RID: 6045
		RailResult AsyncSetRoomMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x0600179E RID: 6046
		RailResult AsyncGetRoomMetadata(List<string> keys, string user_data);

		// Token: 0x0600179F RID: 6047
		RailResult AsyncClearRoomMetadata(List<string> keys, string user_data);

		// Token: 0x060017A0 RID: 6048
		bool GetMemberMetadata(RailID member_id, string key, out string value);

		// Token: 0x060017A1 RID: 6049
		bool SetMemberMetadata(RailID member_id, string key, string value);

		// Token: 0x060017A2 RID: 6050
		RailResult AsyncGetMemberMetadata(RailID member_id, List<string> keys, string user_data);

		// Token: 0x060017A3 RID: 6051
		RailResult AsyncSetMemberMetadata(RailID member_id, List<RailKeyValue> key_values, string user_data);

		// Token: 0x060017A4 RID: 6052
		RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x060017A5 RID: 6053
		RailResult SendDataToMember(RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x060017A6 RID: 6054
		uint GetNumOfMembers();

		// Token: 0x060017A7 RID: 6055
		RailID GetMemberByIndex(uint index);

		// Token: 0x060017A8 RID: 6056
		RailResult GetMemberNameByIndex(uint index, out string name);

		// Token: 0x060017A9 RID: 6057
		uint GetMaxMembers();

		// Token: 0x060017AA RID: 6058
		bool SetGameServerID(ulong game_server_rail_id);

		// Token: 0x060017AB RID: 6059
		bool GetGameServerID(out ulong game_server_rail_id);

		// Token: 0x060017AC RID: 6060
		bool SetRoomJoinable(bool is_joinable);

		// Token: 0x060017AD RID: 6061
		bool GetRoomJoinable();

		// Token: 0x060017AE RID: 6062
		RailResult GetFriendsInRoom(List<RailID> friend_ids);

		// Token: 0x060017AF RID: 6063
		bool IsUserInRoom(RailID user_rail_id);

		// Token: 0x060017B0 RID: 6064
		RailResult EnableTeamVoice(bool enable);
	}
}
