using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B7 RID: 183
	[CallbackIdentity(1321)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x0400021D RID: 541
		public const int k_iCallback = 1321;

		// Token: 0x0400021E RID: 542
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400021F RID: 543
		public AppId_t m_nAppID;
	}
}
