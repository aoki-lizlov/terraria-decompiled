using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000CC RID: 204
	[CallbackIdentity(3407)]
	[StructLayout(0, Pack = 4)]
	public struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x0400026B RID: 619
		public const int k_iCallback = 3407;

		// Token: 0x0400026C RID: 620
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x0400026D RID: 621
		public EResult m_eResult;

		// Token: 0x0400026E RID: 622
		[MarshalAs(3)]
		public bool m_bWasAddRequest;
	}
}
