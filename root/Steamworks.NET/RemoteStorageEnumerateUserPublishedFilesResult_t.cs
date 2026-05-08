using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AE RID: 174
	[CallbackIdentity(1312)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x040001DC RID: 476
		public const int k_iCallback = 1312;

		// Token: 0x040001DD RID: 477
		public EResult m_eResult;

		// Token: 0x040001DE RID: 478
		public int m_nResultsReturned;

		// Token: 0x040001DF RID: 479
		public int m_nTotalResultCount;

		// Token: 0x040001E0 RID: 480
		[MarshalAs(30, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;
	}
}
