using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CE RID: 206
	[CallbackIdentity(3409)]
	[StructLayout(0, Pack = 4)]
	public struct GetUserItemVoteResult_t
	{
		// Token: 0x04000273 RID: 627
		public const int k_iCallback = 3409;

		// Token: 0x04000274 RID: 628
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000275 RID: 629
		public EResult m_eResult;

		// Token: 0x04000276 RID: 630
		[MarshalAs(3)]
		public bool m_bVotedUp;

		// Token: 0x04000277 RID: 631
		[MarshalAs(3)]
		public bool m_bVotedDown;

		// Token: 0x04000278 RID: 632
		[MarshalAs(3)]
		public bool m_bVoteSkipped;
	}
}
