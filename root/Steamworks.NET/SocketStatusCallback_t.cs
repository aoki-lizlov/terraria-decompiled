using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A1 RID: 161
	[CallbackIdentity(1201)]
	[StructLayout(0, Pack = 4)]
	public struct SocketStatusCallback_t
	{
		// Token: 0x040001B4 RID: 436
		public const int k_iCallback = 1201;

		// Token: 0x040001B5 RID: 437
		public SNetSocket_t m_hSocket;

		// Token: 0x040001B6 RID: 438
		public SNetListenSocket_t m_hListenSocket;

		// Token: 0x040001B7 RID: 439
		public CSteamID m_steamIDRemote;

		// Token: 0x040001B8 RID: 440
		public int m_eSNetSocketState;
	}
}
