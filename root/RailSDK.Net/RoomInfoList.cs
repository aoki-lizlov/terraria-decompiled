using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000126 RID: 294
	public class RoomInfoList : EventBase
	{
		// Token: 0x060017CA RID: 6090 RVA: 0x00010E36 File Offset: 0x0000F036
		public RoomInfoList()
		{
		}

		// Token: 0x04000488 RID: 1160
		public uint total_room_num_in_zone;

		// Token: 0x04000489 RID: 1161
		public List<RoomInfo> room_info = new List<RoomInfo>();

		// Token: 0x0400048A RID: 1162
		public uint end_index;

		// Token: 0x0400048B RID: 1163
		public ulong zone_id;

		// Token: 0x0400048C RID: 1164
		public uint begin_index;
	}
}
