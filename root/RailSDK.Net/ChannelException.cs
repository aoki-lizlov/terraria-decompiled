using System;

namespace rail
{
	// Token: 0x020000F6 RID: 246
	public class ChannelException : EventBase
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00010CED File Offset: 0x0000EEED
		public ChannelException()
		{
		}

		// Token: 0x0400029F RID: 671
		public ChannelExceptionType exception_type;

		// Token: 0x040002A0 RID: 672
		public ulong channel_id;

		// Token: 0x040002A1 RID: 673
		public RailID local_peer = new RailID();
	}
}
