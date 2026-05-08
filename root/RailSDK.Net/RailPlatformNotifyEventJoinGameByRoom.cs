using System;

namespace rail
{
	// Token: 0x02000159 RID: 345
	public class RailPlatformNotifyEventJoinGameByRoom : EventBase
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x0000F049 File Offset: 0x0000D249
		public RailPlatformNotifyEventJoinGameByRoom()
		{
		}

		// Token: 0x040004F2 RID: 1266
		public string commandline_info;

		// Token: 0x040004F3 RID: 1267
		public ulong room_id;

		// Token: 0x040004F4 RID: 1268
		public ulong zone_id;
	}
}
