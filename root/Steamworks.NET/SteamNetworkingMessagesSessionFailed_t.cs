using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A3 RID: 163
	[CallbackIdentity(1252)]
	[StructLayout(0, Pack = 4)]
	public struct SteamNetworkingMessagesSessionFailed_t
	{
		// Token: 0x040001BB RID: 443
		public const int k_iCallback = 1252;

		// Token: 0x040001BC RID: 444
		public SteamNetConnectionInfo_t m_info;
	}
}
