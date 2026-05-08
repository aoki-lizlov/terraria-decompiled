using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BD RID: 189
	[CallbackIdentity(1327)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x04000232 RID: 562
		public const int k_iCallback = 1327;

		// Token: 0x04000233 RID: 563
		public EResult m_eResult;

		// Token: 0x04000234 RID: 564
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000235 RID: 565
		public EWorkshopFileAction m_eAction;
	}
}
