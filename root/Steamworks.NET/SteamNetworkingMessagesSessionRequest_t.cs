using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A2 RID: 162
	[CallbackIdentity(1251)]
	[StructLayout(0, Pack = 4)]
	public struct SteamNetworkingMessagesSessionRequest_t
	{
		// Token: 0x040001B9 RID: 441
		public const int k_iCallback = 1251;

		// Token: 0x040001BA RID: 442
		public SteamNetworkingIdentity m_identityRemote;
	}
}
