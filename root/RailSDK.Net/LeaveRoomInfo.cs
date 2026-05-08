using System;

namespace rail
{
	// Token: 0x0200011B RID: 283
	public class LeaveRoomInfo : EventBase
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x0000F049 File Offset: 0x0000D249
		public LeaveRoomInfo()
		{
		}

		// Token: 0x0400045E RID: 1118
		public EnumLeaveRoomReason reason;

		// Token: 0x0400045F RID: 1119
		public ulong room_id;
	}
}
