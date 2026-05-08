using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E5 RID: 229
	[CallbackIdentity(166)]
	[StructLayout(0, Pack = 4)]
	public struct MarketEligibilityResponse_t
	{
		// Token: 0x040002BF RID: 703
		public const int k_iCallback = 166;

		// Token: 0x040002C0 RID: 704
		[MarshalAs(3)]
		public bool m_bAllowed;

		// Token: 0x040002C1 RID: 705
		public EMarketNotAllowedReasonFlags m_eNotAllowedReason;

		// Token: 0x040002C2 RID: 706
		public RTime32 m_rtAllowedAtTime;

		// Token: 0x040002C3 RID: 707
		public int m_cdaySteamGuardRequiredDays;

		// Token: 0x040002C4 RID: 708
		public int m_cdayNewDeviceCooldown;
	}
}
