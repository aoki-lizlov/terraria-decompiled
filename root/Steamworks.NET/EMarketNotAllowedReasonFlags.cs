using System;

namespace Steamworks
{
	// Token: 0x02000159 RID: 345
	[Flags]
	public enum EMarketNotAllowedReasonFlags
	{
		// Token: 0x040008A6 RID: 2214
		k_EMarketNotAllowedReason_None = 0,
		// Token: 0x040008A7 RID: 2215
		k_EMarketNotAllowedReason_TemporaryFailure = 1,
		// Token: 0x040008A8 RID: 2216
		k_EMarketNotAllowedReason_AccountDisabled = 2,
		// Token: 0x040008A9 RID: 2217
		k_EMarketNotAllowedReason_AccountLockedDown = 4,
		// Token: 0x040008AA RID: 2218
		k_EMarketNotAllowedReason_AccountLimited = 8,
		// Token: 0x040008AB RID: 2219
		k_EMarketNotAllowedReason_TradeBanned = 16,
		// Token: 0x040008AC RID: 2220
		k_EMarketNotAllowedReason_AccountNotTrusted = 32,
		// Token: 0x040008AD RID: 2221
		k_EMarketNotAllowedReason_SteamGuardNotEnabled = 64,
		// Token: 0x040008AE RID: 2222
		k_EMarketNotAllowedReason_SteamGuardOnlyRecentlyEnabled = 128,
		// Token: 0x040008AF RID: 2223
		k_EMarketNotAllowedReason_RecentPasswordReset = 256,
		// Token: 0x040008B0 RID: 2224
		k_EMarketNotAllowedReason_NewPaymentMethod = 512,
		// Token: 0x040008B1 RID: 2225
		k_EMarketNotAllowedReason_InvalidCookie = 1024,
		// Token: 0x040008B2 RID: 2226
		k_EMarketNotAllowedReason_UsingNewDevice = 2048,
		// Token: 0x040008B3 RID: 2227
		k_EMarketNotAllowedReason_RecentSelfRefund = 4096,
		// Token: 0x040008B4 RID: 2228
		k_EMarketNotAllowedReason_NewPaymentMethodCannotBeVerified = 8192,
		// Token: 0x040008B5 RID: 2229
		k_EMarketNotAllowedReason_NoRecentPurchases = 16384,
		// Token: 0x040008B6 RID: 2230
		k_EMarketNotAllowedReason_AcceptedWalletGift = 32768
	}
}
