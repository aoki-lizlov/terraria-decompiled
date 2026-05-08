using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E1 RID: 225
	[CallbackIdentity(154)]
	[StructLayout(0, Pack = 4)]
	public struct EncryptedAppTicketResponse_t
	{
		// Token: 0x040002B6 RID: 694
		public const int k_iCallback = 154;

		// Token: 0x040002B7 RID: 695
		public EResult m_eResult;
	}
}
