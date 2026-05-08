using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B2 RID: 178
	[CallbackIdentity(1316)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x040001ED RID: 493
		public const int k_iCallback = 1316;

		// Token: 0x040001EE RID: 494
		public EResult m_eResult;

		// Token: 0x040001EF RID: 495
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040001F0 RID: 496
		[MarshalAs(3)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
