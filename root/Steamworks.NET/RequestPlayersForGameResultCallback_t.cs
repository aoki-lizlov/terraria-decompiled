using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000085 RID: 133
	[CallbackIdentity(5212)]
	[StructLayout(0, Pack = 4)]
	public struct RequestPlayersForGameResultCallback_t
	{
		// Token: 0x04000173 RID: 371
		public const int k_iCallback = 5212;

		// Token: 0x04000174 RID: 372
		public EResult m_eResult;

		// Token: 0x04000175 RID: 373
		public ulong m_ullSearchID;

		// Token: 0x04000176 RID: 374
		public CSteamID m_SteamIDPlayerFound;

		// Token: 0x04000177 RID: 375
		public CSteamID m_SteamIDLobby;

		// Token: 0x04000178 RID: 376
		public PlayerAcceptState_t m_ePlayerAcceptState;

		// Token: 0x04000179 RID: 377
		public int m_nPlayerIndex;

		// Token: 0x0400017A RID: 378
		public int m_nTotalPlayersFound;

		// Token: 0x0400017B RID: 379
		public int m_nTotalPlayersAcceptedGame;

		// Token: 0x0400017C RID: 380
		public int m_nSuggestedTeamIndex;

		// Token: 0x0400017D RID: 381
		public ulong m_ullUniqueGameID;
	}
}
