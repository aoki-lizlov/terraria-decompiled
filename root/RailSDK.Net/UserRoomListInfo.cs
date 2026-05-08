using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200012E RID: 302
	public class UserRoomListInfo : EventBase
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x00010E6F File Offset: 0x0000F06F
		public UserRoomListInfo()
		{
		}

		// Token: 0x040004A5 RID: 1189
		public List<RoomInfo> room_info = new List<RoomInfo>();
	}
}
