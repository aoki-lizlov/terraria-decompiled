using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B1 RID: 177
	[CallbackIdentity(1315)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x040001EA RID: 490
		public const int k_iCallback = 1315;

		// Token: 0x040001EB RID: 491
		public EResult m_eResult;

		// Token: 0x040001EC RID: 492
		public PublishedFileId_t m_nPublishedFileId;
	}
}
