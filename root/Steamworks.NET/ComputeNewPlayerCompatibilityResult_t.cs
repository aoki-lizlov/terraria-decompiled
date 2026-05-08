using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004F RID: 79
	[CallbackIdentity(211)]
	[StructLayout(0, Pack = 4)]
	public struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x0400008B RID: 139
		public const int k_iCallback = 211;

		// Token: 0x0400008C RID: 140
		public EResult m_eResult;

		// Token: 0x0400008D RID: 141
		public int m_cPlayersThatDontLikeCandidate;

		// Token: 0x0400008E RID: 142
		public int m_cPlayersThatCandidateDoesntLike;

		// Token: 0x0400008F RID: 143
		public int m_cClanPlayersThatDontLikeCandidate;

		// Token: 0x04000090 RID: 144
		public CSteamID m_SteamIDCandidate;
	}
}
