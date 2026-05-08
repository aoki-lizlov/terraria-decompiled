using System;

namespace rail
{
	// Token: 0x020000F3 RID: 243
	public class CreateSessionRequest : EventBase
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x00010CCF File Offset: 0x0000EECF
		public CreateSessionRequest()
		{
		}

		// Token: 0x04000295 RID: 661
		public RailID local_peer = new RailID();

		// Token: 0x04000296 RID: 662
		public RailID remote_peer = new RailID();
	}
}
