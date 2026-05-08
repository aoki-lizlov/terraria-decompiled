using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D2 RID: 210
	[CallbackIdentity(3413)]
	[StructLayout(0, Pack = 4)]
	public struct RemoveUGCDependencyResult_t
	{
		// Token: 0x04000281 RID: 641
		public const int k_iCallback = 3413;

		// Token: 0x04000282 RID: 642
		public EResult m_eResult;

		// Token: 0x04000283 RID: 643
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000284 RID: 644
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
