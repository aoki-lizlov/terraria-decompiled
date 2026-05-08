using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CA RID: 202
	[CallbackIdentity(3405)]
	[StructLayout(0, Pack = 4)]
	public struct ItemInstalled_t
	{
		// Token: 0x04000262 RID: 610
		public const int k_iCallback = 3405;

		// Token: 0x04000263 RID: 611
		public AppId_t m_unAppID;

		// Token: 0x04000264 RID: 612
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000265 RID: 613
		public UGCHandle_t m_hLegacyContent;

		// Token: 0x04000266 RID: 614
		public ulong m_unManifestID;
	}
}
