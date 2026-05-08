using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000ED RID: 237
	[CallbackIdentity(1106)]
	[StructLayout(0, Pack = 4)]
	public struct LeaderboardScoreUploaded_t
	{
		// Token: 0x040002E7 RID: 743
		public const int k_iCallback = 1106;

		// Token: 0x040002E8 RID: 744
		public byte m_bSuccess;

		// Token: 0x040002E9 RID: 745
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002EA RID: 746
		public int m_nScore;

		// Token: 0x040002EB RID: 747
		public byte m_bScoreChanged;

		// Token: 0x040002EC RID: 748
		public int m_nGlobalRankNew;

		// Token: 0x040002ED RID: 749
		public int m_nGlobalRankPrevious;
	}
}
