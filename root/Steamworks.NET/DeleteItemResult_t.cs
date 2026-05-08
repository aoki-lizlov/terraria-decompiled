using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D6 RID: 214
	[CallbackIdentity(3417)]
	[StructLayout(0, Pack = 4)]
	public struct DeleteItemResult_t
	{
		// Token: 0x04000293 RID: 659
		public const int k_iCallback = 3417;

		// Token: 0x04000294 RID: 660
		public EResult m_eResult;

		// Token: 0x04000295 RID: 661
		public PublishedFileId_t m_nPublishedFileId;
	}
}
