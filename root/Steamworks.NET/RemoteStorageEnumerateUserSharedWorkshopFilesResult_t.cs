using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BC RID: 188
	[CallbackIdentity(1326)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x0400022D RID: 557
		public const int k_iCallback = 1326;

		// Token: 0x0400022E RID: 558
		public EResult m_eResult;

		// Token: 0x0400022F RID: 559
		public int m_nResultsReturned;

		// Token: 0x04000230 RID: 560
		public int m_nTotalResultCount;

		// Token: 0x04000231 RID: 561
		[MarshalAs(30, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
