using System;

namespace rail
{
	// Token: 0x020000F9 RID: 249
	public class ChannelNetDelay : EventBase
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x00010D1E File Offset: 0x0000EF1E
		public ChannelNetDelay()
		{
		}

		// Token: 0x040002B3 RID: 691
		public ulong channel_id;

		// Token: 0x040002B4 RID: 692
		public RailID local_peer = new RailID();

		// Token: 0x040002B5 RID: 693
		public uint net_delay_ms;
	}
}
