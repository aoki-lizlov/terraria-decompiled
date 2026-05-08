using System;

namespace rail
{
	// Token: 0x0200015A RID: 346
	public class RailPlatformNotifyEventJoinGameByUser : EventBase
	{
		// Token: 0x06001836 RID: 6198 RVA: 0x00010F25 File Offset: 0x0000F125
		public RailPlatformNotifyEventJoinGameByUser()
		{
		}

		// Token: 0x040004F5 RID: 1269
		public RailID rail_id_to_join = new RailID();

		// Token: 0x040004F6 RID: 1270
		public string commandline_info;
	}
}
