using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E2 RID: 226
	[CallbackIdentity(163)]
	[StructLayout(0, Pack = 4)]
	public struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x040002B8 RID: 696
		public const int k_iCallback = 163;

		// Token: 0x040002B9 RID: 697
		public HAuthTicket m_hAuthTicket;

		// Token: 0x040002BA RID: 698
		public EResult m_eResult;
	}
}
