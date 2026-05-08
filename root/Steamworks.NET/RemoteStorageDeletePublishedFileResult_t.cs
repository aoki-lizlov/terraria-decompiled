using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AD RID: 173
	[CallbackIdentity(1311)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageDeletePublishedFileResult_t
	{
		// Token: 0x040001D9 RID: 473
		public const int k_iCallback = 1311;

		// Token: 0x040001DA RID: 474
		public EResult m_eResult;

		// Token: 0x040001DB RID: 475
		public PublishedFileId_t m_nPublishedFileId;
	}
}
