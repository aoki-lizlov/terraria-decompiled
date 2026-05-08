using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000082 RID: 130
	[CallbackIdentity(5201)]
	[StructLayout(0, Pack = 4)]
	public struct SearchForGameProgressCallback_t
	{
		// Token: 0x04000162 RID: 354
		public const int k_iCallback = 5201;

		// Token: 0x04000163 RID: 355
		public ulong m_ullSearchID;

		// Token: 0x04000164 RID: 356
		public EResult m_eResult;

		// Token: 0x04000165 RID: 357
		public CSteamID m_lobbyID;

		// Token: 0x04000166 RID: 358
		public CSteamID m_steamIDEndedSearch;

		// Token: 0x04000167 RID: 359
		public int m_nSecondsRemainingEstimate;

		// Token: 0x04000168 RID: 360
		public int m_cPlayersSearching;
	}
}
