using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C9 RID: 201
	[CallbackIdentity(3404)]
	[StructLayout(0, Pack = 4)]
	public struct SubmitItemUpdateResult_t
	{
		// Token: 0x0400025E RID: 606
		public const int k_iCallback = 3404;

		// Token: 0x0400025F RID: 607
		public EResult m_eResult;

		// Token: 0x04000260 RID: 608
		[MarshalAs(3)]
		public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x04000261 RID: 609
		public PublishedFileId_t m_nPublishedFileId;
	}
}
