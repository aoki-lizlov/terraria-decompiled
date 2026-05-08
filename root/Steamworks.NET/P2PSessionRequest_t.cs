using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009F RID: 159
	[CallbackIdentity(1202)]
	[StructLayout(0, Pack = 4)]
	public struct P2PSessionRequest_t
	{
		// Token: 0x040001AF RID: 431
		public const int k_iCallback = 1202;

		// Token: 0x040001B0 RID: 432
		public CSteamID m_steamIDRemote;
	}
}
