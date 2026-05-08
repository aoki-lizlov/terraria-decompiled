using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A9 RID: 169
	[CallbackIdentity(5702)]
	[StructLayout(0, Pack = 4)]
	public struct SteamRemotePlaySessionDisconnected_t
	{
		// Token: 0x040001CD RID: 461
		public const int k_iCallback = 5702;

		// Token: 0x040001CE RID: 462
		public RemotePlaySessionID_t m_unSessionID;
	}
}
