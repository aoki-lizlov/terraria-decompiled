using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EC RID: 236
	[CallbackIdentity(1105)]
	[StructLayout(0, Pack = 4)]
	public struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x040002E3 RID: 739
		public const int k_iCallback = 1105;

		// Token: 0x040002E4 RID: 740
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002E5 RID: 741
		public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		// Token: 0x040002E6 RID: 742
		public int m_cEntryCount;
	}
}
