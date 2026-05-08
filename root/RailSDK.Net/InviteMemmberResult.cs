using System;

namespace rail
{
	// Token: 0x020000FC RID: 252
	public class InviteMemmberResult : EventBase
	{
		// Token: 0x06001777 RID: 6007 RVA: 0x00010D62 File Offset: 0x0000EF62
		public InviteMemmberResult()
		{
		}

		// Token: 0x040002BB RID: 699
		public ulong channel_id;

		// Token: 0x040002BC RID: 700
		public RailID local_peer = new RailID();

		// Token: 0x040002BD RID: 701
		public RailID remote_peer = new RailID();
	}
}
