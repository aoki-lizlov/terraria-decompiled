using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007D RID: 125
	[CallbackIdentity(509)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyGameCreated_t
	{
		// Token: 0x04000152 RID: 338
		public const int k_iCallback = 509;

		// Token: 0x04000153 RID: 339
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000154 RID: 340
		public ulong m_ulSteamIDGameServer;

		// Token: 0x04000155 RID: 341
		public uint m_unIP;

		// Token: 0x04000156 RID: 342
		public ushort m_usPort;
	}
}
