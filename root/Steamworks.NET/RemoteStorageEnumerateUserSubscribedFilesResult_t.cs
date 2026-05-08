using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B0 RID: 176
	[CallbackIdentity(1314)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		// Token: 0x040001E4 RID: 484
		public const int k_iCallback = 1314;

		// Token: 0x040001E5 RID: 485
		public EResult m_eResult;

		// Token: 0x040001E6 RID: 486
		public int m_nResultsReturned;

		// Token: 0x040001E7 RID: 487
		public int m_nTotalResultCount;

		// Token: 0x040001E8 RID: 488
		[MarshalAs(30, SizeConst = 50)]
		public PublishedFileId_t[] m_rgPublishedFileId;

		// Token: 0x040001E9 RID: 489
		[MarshalAs(30, SizeConst = 50)]
		public uint[] m_rgRTimeSubscribed;
	}
}
