using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000179 RID: 377
	[StructLayout(0, Pack = 4)]
	public struct LeaderboardEntry_t
	{
		// Token: 0x04000A1C RID: 2588
		public CSteamID m_steamIDUser;

		// Token: 0x04000A1D RID: 2589
		public int m_nGlobalRank;

		// Token: 0x04000A1E RID: 2590
		public int m_nScore;

		// Token: 0x04000A1F RID: 2591
		public int m_cDetails;

		// Token: 0x04000A20 RID: 2592
		public UGCHandle_t m_hUGC;
	}
}
