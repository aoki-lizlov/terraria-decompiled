using System;

namespace rail
{
	// Token: 0x020000FD RID: 253
	public class JoinChannelResult : EventBase
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x00010D80 File Offset: 0x0000EF80
		public JoinChannelResult()
		{
		}

		// Token: 0x040002BE RID: 702
		public ulong channel_id;

		// Token: 0x040002BF RID: 703
		public RailID local_peer = new RailID();
	}
}
