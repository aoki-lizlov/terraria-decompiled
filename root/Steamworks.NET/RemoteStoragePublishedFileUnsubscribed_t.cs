using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B8 RID: 184
	[CallbackIdentity(1322)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x04000220 RID: 544
		public const int k_iCallback = 1322;

		// Token: 0x04000221 RID: 545
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000222 RID: 546
		public AppId_t m_nAppID;
	}
}
