using System;

namespace rail
{
	// Token: 0x0200010D RID: 269
	public interface IRailRoomHelper
	{
		// Token: 0x060017B1 RID: 6065
		void set_current_zone_id(ulong zone_id);

		// Token: 0x060017B2 RID: 6066
		ulong get_current_zone_id();

		// Token: 0x060017B3 RID: 6067
		IRailRoom CreateRoom(RoomOptions options, string room_name, out RailResult result);

		// Token: 0x060017B4 RID: 6068
		IRailRoom AsyncCreateRoom(RoomOptions options, string room_name, string user_data);

		// Token: 0x060017B5 RID: 6069
		IRailRoom OpenRoom(ulong zone_id, ulong room_id, out RailResult result);

		// Token: 0x060017B6 RID: 6070
		RailResult AsyncGetUserRoomList(string user_data);
	}
}
