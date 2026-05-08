using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B6 RID: 182
	[CallbackIdentity(1320)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x04000216 RID: 534
		public const int k_iCallback = 1320;

		// Token: 0x04000217 RID: 535
		public EResult m_eResult;

		// Token: 0x04000218 RID: 536
		public PublishedFileId_t m_unPublishedFileId;

		// Token: 0x04000219 RID: 537
		public int m_nVotesFor;

		// Token: 0x0400021A RID: 538
		public int m_nVotesAgainst;

		// Token: 0x0400021B RID: 539
		public int m_nReports;

		// Token: 0x0400021C RID: 540
		public float m_fScore;
	}
}
