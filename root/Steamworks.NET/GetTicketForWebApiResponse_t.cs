using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E7 RID: 231
	[CallbackIdentity(168)]
	[StructLayout(0, Pack = 4)]
	public struct GetTicketForWebApiResponse_t
	{
		// Token: 0x040002CE RID: 718
		public const int k_iCallback = 168;

		// Token: 0x040002CF RID: 719
		public HAuthTicket m_hAuthTicket;

		// Token: 0x040002D0 RID: 720
		public EResult m_eResult;

		// Token: 0x040002D1 RID: 721
		public int m_cubTicket;

		// Token: 0x040002D2 RID: 722
		[MarshalAs(30, SizeConst = 2560)]
		public byte[] m_rgubTicket;
	}
}
