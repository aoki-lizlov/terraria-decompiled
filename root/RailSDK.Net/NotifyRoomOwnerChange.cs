using System;

namespace rail
{
	// Token: 0x02000122 RID: 290
	public class NotifyRoomOwnerChange : EventBase
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0000F049 File Offset: 0x0000D249
		public NotifyRoomOwnerChange()
		{
		}

		// Token: 0x04000472 RID: 1138
		public ulong old_owner_id;

		// Token: 0x04000473 RID: 1139
		public EnumRoomOwnerChangeReason reason;

		// Token: 0x04000474 RID: 1140
		public ulong room_id;

		// Token: 0x04000475 RID: 1141
		public ulong new_owner_id;
	}
}
