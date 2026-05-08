using System;

namespace rail
{
	// Token: 0x020000F2 RID: 242
	public class CreateSessionFailed : EventBase
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x00010CB1 File Offset: 0x0000EEB1
		public CreateSessionFailed()
		{
		}

		// Token: 0x04000293 RID: 659
		public RailID local_peer = new RailID();

		// Token: 0x04000294 RID: 660
		public RailID remote_peer = new RailID();
	}
}
