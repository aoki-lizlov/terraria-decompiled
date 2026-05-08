using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C0 RID: 192
	[CallbackIdentity(1330)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x04000240 RID: 576
		public const int k_iCallback = 1330;

		// Token: 0x04000241 RID: 577
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000242 RID: 578
		public AppId_t m_nAppID;

		// Token: 0x04000243 RID: 579
		public ulong m_ulUnused;
	}
}
