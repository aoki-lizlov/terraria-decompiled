using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CD RID: 205
	[CallbackIdentity(3408)]
	[StructLayout(0, Pack = 4)]
	public struct SetUserItemVoteResult_t
	{
		// Token: 0x0400026F RID: 623
		public const int k_iCallback = 3408;

		// Token: 0x04000270 RID: 624
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000271 RID: 625
		public EResult m_eResult;

		// Token: 0x04000272 RID: 626
		[MarshalAs(3)]
		public bool m_bVoteUp;
	}
}
