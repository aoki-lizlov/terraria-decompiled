using System;

namespace rail
{
	// Token: 0x02000121 RID: 289
	public class NotifyRoomMemberKicked : EventBase
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x0000F049 File Offset: 0x0000D249
		public NotifyRoomMemberKicked()
		{
		}

		// Token: 0x0400046E RID: 1134
		public ulong id_for_making_kick;

		// Token: 0x0400046F RID: 1135
		public uint due_to_kicker_lost_connect;

		// Token: 0x04000470 RID: 1136
		public ulong room_id;

		// Token: 0x04000471 RID: 1137
		public ulong kicked_id;
	}
}
