using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AC RID: 172
	[CallbackIdentity(1309)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x040001D5 RID: 469
		public const int k_iCallback = 1309;

		// Token: 0x040001D6 RID: 470
		public EResult m_eResult;

		// Token: 0x040001D7 RID: 471
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040001D8 RID: 472
		[MarshalAs(3)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
