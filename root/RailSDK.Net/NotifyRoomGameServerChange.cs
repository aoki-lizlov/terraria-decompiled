using System;

namespace rail
{
	// Token: 0x0200011F RID: 287
	public class NotifyRoomGameServerChange : EventBase
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x0000F049 File Offset: 0x0000D249
		public NotifyRoomGameServerChange()
		{
		}

		// Token: 0x04000467 RID: 1127
		public ulong game_server_rail_id;

		// Token: 0x04000468 RID: 1128
		public ulong room_id;

		// Token: 0x04000469 RID: 1129
		public ulong game_server_channel_id;
	}
}
