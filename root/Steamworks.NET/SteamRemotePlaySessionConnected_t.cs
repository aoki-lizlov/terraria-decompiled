using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A8 RID: 168
	[CallbackIdentity(5701)]
	[StructLayout(0, Pack = 4)]
	public struct SteamRemotePlaySessionConnected_t
	{
		// Token: 0x040001CB RID: 459
		public const int k_iCallback = 5701;

		// Token: 0x040001CC RID: 460
		public RemotePlaySessionID_t m_unSessionID;
	}
}
