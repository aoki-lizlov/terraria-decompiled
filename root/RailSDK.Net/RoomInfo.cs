using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000125 RID: 293
	public class RoomInfo
	{
		// Token: 0x060017C9 RID: 6089 RVA: 0x00010E18 File Offset: 0x0000F018
		public RoomInfo()
		{
		}

		// Token: 0x0400047B RID: 1147
		public ulong zone_id;

		// Token: 0x0400047C RID: 1148
		public bool has_password;

		// Token: 0x0400047D RID: 1149
		public uint create_time;

		// Token: 0x0400047E RID: 1150
		public uint max_members;

		// Token: 0x0400047F RID: 1151
		public string room_name;

		// Token: 0x04000480 RID: 1152
		public ulong game_server_rail_id;

		// Token: 0x04000481 RID: 1153
		public ulong room_id;

		// Token: 0x04000482 RID: 1154
		public uint current_members;

		// Token: 0x04000483 RID: 1155
		public bool is_joinable;

		// Token: 0x04000484 RID: 1156
		public EnumRoomStatus room_state;

		// Token: 0x04000485 RID: 1157
		public List<RailKeyValue> room_kvs = new List<RailKeyValue>();

		// Token: 0x04000486 RID: 1158
		public EnumRoomType type;

		// Token: 0x04000487 RID: 1159
		public RailID owner_id = new RailID();
	}
}
