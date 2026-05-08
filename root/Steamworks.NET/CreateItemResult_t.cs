using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C8 RID: 200
	[CallbackIdentity(3403)]
	[StructLayout(0, Pack = 4)]
	public struct CreateItemResult_t
	{
		// Token: 0x0400025A RID: 602
		public const int k_iCallback = 3403;

		// Token: 0x0400025B RID: 603
		public EResult m_eResult;

		// Token: 0x0400025C RID: 604
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400025D RID: 605
		[MarshalAs(3)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
	}
}
