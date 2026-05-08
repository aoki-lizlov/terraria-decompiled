using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F2 RID: 242
	[CallbackIdentity(1111)]
	[StructLayout(0, Pack = 4)]
	public struct LeaderboardUGCSet_t
	{
		// Token: 0x040002FB RID: 763
		public const int k_iCallback = 1111;

		// Token: 0x040002FC RID: 764
		public EResult m_eResult;

		// Token: 0x040002FD RID: 765
		public SteamLeaderboard_t m_hSteamLeaderboard;
	}
}
