using System;

namespace rail
{
	// Token: 0x020000F8 RID: 248
	public class ChannelMemberStateChanged : EventBase
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x00010D00 File Offset: 0x0000EF00
		public ChannelMemberStateChanged()
		{
		}

		// Token: 0x040002AF RID: 687
		public ulong channel_id;

		// Token: 0x040002B0 RID: 688
		public RailID local_peer = new RailID();

		// Token: 0x040002B1 RID: 689
		public RailChannelMemberState member_state;

		// Token: 0x040002B2 RID: 690
		public RailID remote_peer = new RailID();
	}
}
