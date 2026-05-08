using System;

namespace rail
{
	// Token: 0x020000FB RID: 251
	public class InviteJoinChannelRequest : EventBase
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x00010D44 File Offset: 0x0000EF44
		public InviteJoinChannelRequest()
		{
		}

		// Token: 0x040002B8 RID: 696
		public ulong channel_id;

		// Token: 0x040002B9 RID: 697
		public RailID local_peer = new RailID();

		// Token: 0x040002BA RID: 698
		public RailID remote_peer = new RailID();
	}
}
