using System;

namespace rail
{
	// Token: 0x02000110 RID: 272
	public class CreateRoomInfo : EventBase
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x0000F049 File Offset: 0x0000D249
		public CreateRoomInfo()
		{
		}

		// Token: 0x0400043B RID: 1083
		public ulong room_id;

		// Token: 0x0400043C RID: 1084
		public ulong zone_id;
	}
}
