using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000078 RID: 120
	[CallbackIdentity(503)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyInvite_t
	{
		// Token: 0x0400013B RID: 315
		public const int k_iCallback = 503;

		// Token: 0x0400013C RID: 316
		public ulong m_ulSteamIDUser;

		// Token: 0x0400013D RID: 317
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400013E RID: 318
		public ulong m_ulGameID;
	}
}
