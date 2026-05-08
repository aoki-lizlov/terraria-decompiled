using System;

namespace rail
{
	// Token: 0x020000FA RID: 250
	public class CreateChannelResult : EventBase
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x00010D31 File Offset: 0x0000EF31
		public CreateChannelResult()
		{
		}

		// Token: 0x040002B6 RID: 694
		public ulong channel_id;

		// Token: 0x040002B7 RID: 695
		public RailID local_peer = new RailID();
	}
}
