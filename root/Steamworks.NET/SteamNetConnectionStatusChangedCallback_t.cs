using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A4 RID: 164
	[CallbackIdentity(1221)]
	[StructLayout(0, Pack = 4)]
	public struct SteamNetConnectionStatusChangedCallback_t
	{
		// Token: 0x040001BD RID: 445
		public const int k_iCallback = 1221;

		// Token: 0x040001BE RID: 446
		public HSteamNetConnection m_hConn;

		// Token: 0x040001BF RID: 447
		public SteamNetConnectionInfo_t m_info;

		// Token: 0x040001C0 RID: 448
		public ESteamNetworkingConnectionState m_eOldState;
	}
}
