using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B9 RID: 185
	[CallbackIdentity(1323)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishedFileDeleted_t
	{
		// Token: 0x04000223 RID: 547
		public const int k_iCallback = 1323;

		// Token: 0x04000224 RID: 548
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000225 RID: 549
		public AppId_t m_nAppID;
	}
}
