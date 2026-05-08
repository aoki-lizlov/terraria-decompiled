using System;

namespace rail
{
	// Token: 0x020000B7 RID: 183
	public class AsyncAcquireGameServerSessionTicketResponse : EventBase
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x00010AB7 File Offset: 0x0000ECB7
		public AsyncAcquireGameServerSessionTicketResponse()
		{
		}

		// Token: 0x040001F7 RID: 503
		public RailSessionTicket session_ticket = new RailSessionTicket();
	}
}
