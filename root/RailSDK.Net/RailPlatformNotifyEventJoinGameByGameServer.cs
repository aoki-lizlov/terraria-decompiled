using System;

namespace rail
{
	// Token: 0x02000158 RID: 344
	public class RailPlatformNotifyEventJoinGameByGameServer : EventBase
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x00010F12 File Offset: 0x0000F112
		public RailPlatformNotifyEventJoinGameByGameServer()
		{
		}

		// Token: 0x040004F0 RID: 1264
		public string commandline_info;

		// Token: 0x040004F1 RID: 1265
		public RailID gameserver_railid = new RailID();
	}
}
