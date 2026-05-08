using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CB RID: 203
	[CallbackIdentity(3406)]
	[StructLayout(0, Pack = 4)]
	public struct DownloadItemResult_t
	{
		// Token: 0x04000267 RID: 615
		public const int k_iCallback = 3406;

		// Token: 0x04000268 RID: 616
		public AppId_t m_unAppID;

		// Token: 0x04000269 RID: 617
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400026A RID: 618
		public EResult m_eResult;
	}
}
