using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BB RID: 187
	[CallbackIdentity(1325)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x04000229 RID: 553
		public const int k_iCallback = 1325;

		// Token: 0x0400022A RID: 554
		public EResult m_eResult;

		// Token: 0x0400022B RID: 555
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400022C RID: 556
		public EWorkshopVote m_eVote;
	}
}
