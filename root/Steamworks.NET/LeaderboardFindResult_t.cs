using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EB RID: 235
	[CallbackIdentity(1104)]
	[StructLayout(0, Pack = 4)]
	public struct LeaderboardFindResult_t
	{
		// Token: 0x040002E0 RID: 736
		public const int k_iCallback = 1104;

		// Token: 0x040002E1 RID: 737
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002E2 RID: 738
		public byte m_bLeaderboardFound;
	}
}
