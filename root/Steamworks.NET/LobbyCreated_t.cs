using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000080 RID: 128
	[CallbackIdentity(513)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyCreated_t
	{
		// Token: 0x0400015D RID: 349
		public const int k_iCallback = 513;

		// Token: 0x0400015E RID: 350
		public EResult m_eResult;

		// Token: 0x0400015F RID: 351
		public ulong m_ulSteamIDLobby;
	}
}
