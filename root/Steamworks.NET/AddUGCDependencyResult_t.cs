using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D1 RID: 209
	[CallbackIdentity(3412)]
	[StructLayout(0, Pack = 4)]
	public struct AddUGCDependencyResult_t
	{
		// Token: 0x0400027D RID: 637
		public const int k_iCallback = 3412;

		// Token: 0x0400027E RID: 638
		public EResult m_eResult;

		// Token: 0x0400027F RID: 639
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000280 RID: 640
		public PublishedFileId_t m_nChildPublishedFileId;
	}
}
