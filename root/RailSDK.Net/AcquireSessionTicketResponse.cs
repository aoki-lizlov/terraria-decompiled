using System;

namespace rail
{
	// Token: 0x02000100 RID: 256
	public class AcquireSessionTicketResponse : EventBase
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x00010D93 File Offset: 0x0000EF93
		public AcquireSessionTicketResponse()
		{
		}

		// Token: 0x040002C5 RID: 709
		public RailSessionTicket session_ticket = new RailSessionTicket();
	}
}
