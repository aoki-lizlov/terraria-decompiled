using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AF RID: 175
	[CallbackIdentity(1313)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x040001E1 RID: 481
		public const int k_iCallback = 1313;

		// Token: 0x040001E2 RID: 482
		public EResult m_eResult;

		// Token: 0x040001E3 RID: 483
		public PublishedFileId_t m_nPublishedFileId;
	}
}
