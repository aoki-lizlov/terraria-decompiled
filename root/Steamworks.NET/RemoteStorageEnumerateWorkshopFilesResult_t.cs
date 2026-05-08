using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B5 RID: 181
	[CallbackIdentity(1319)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x0400020E RID: 526
		public const int k_iCallback = 1319;

		// Token: 0x0400020F RID: 527
		public EResult m_eResult;

		// Token: 0x04000210 RID: 528
		public int m_nResultsReturned;

		// Token: 0x04000211 RID: 529
		public int m_nTotalResultCount;

		// Token: 0x04000212 RID: 530
		[MarshalAs(30, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x04000213 RID: 531
		[MarshalAs(30, SizeConst = 50)]
		public float[] m_rgScore;

		// Token: 0x04000214 RID: 532
		public AppId_t m_nAppId;

		// Token: 0x04000215 RID: 533
		public uint m_unStartIndex;
	}
}
