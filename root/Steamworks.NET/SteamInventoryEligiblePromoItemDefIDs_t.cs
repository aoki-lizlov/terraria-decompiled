using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000074 RID: 116
	[CallbackIdentity(4703)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x04000127 RID: 295
		public const int k_iCallback = 4703;

		// Token: 0x04000128 RID: 296
		public EResult m_result;

		// Token: 0x04000129 RID: 297
		public CSteamID m_steamID;

		// Token: 0x0400012A RID: 298
		public int m_numEligiblePromoItemDefs;

		// Token: 0x0400012B RID: 299
		[MarshalAs(3)]
		public bool m_bCachedData;
	}
}
