using System;

namespace rail
{
	// Token: 0x02000119 RID: 281
	public class JoinRoomInfo : EventBase
	{
		// Token: 0x060017BD RID: 6077 RVA: 0x0000F049 File Offset: 0x0000D249
		public JoinRoomInfo()
		{
		}

		// Token: 0x0400045A RID: 1114
		public ulong room_id;

		// Token: 0x0400045B RID: 1115
		public ulong zone_id;
	}
}
