using System;

namespace rail
{
	// Token: 0x02000120 RID: 288
	public class NotifyRoomMemberChange : EventBase
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x0000F049 File Offset: 0x0000D249
		public NotifyRoomMemberChange()
		{
		}

		// Token: 0x0400046A RID: 1130
		public ulong changer_id;

		// Token: 0x0400046B RID: 1131
		public ulong id_for_making_change;

		// Token: 0x0400046C RID: 1132
		public EnumRoomMemberActionStatus state_change;

		// Token: 0x0400046D RID: 1133
		public ulong room_id;
	}
}
