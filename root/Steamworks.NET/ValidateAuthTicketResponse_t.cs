using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DF RID: 223
	[CallbackIdentity(143)]
	[StructLayout(0, Pack = 4)]
	public struct ValidateAuthTicketResponse_t
	{
		// Token: 0x040002AE RID: 686
		public const int k_iCallback = 143;

		// Token: 0x040002AF RID: 687
		public CSteamID m_SteamID;

		// Token: 0x040002B0 RID: 688
		public EAuthSessionResponse m_eAuthSessionResponse;

		// Token: 0x040002B1 RID: 689
		public CSteamID m_OwnerSteamID;
	}
}
