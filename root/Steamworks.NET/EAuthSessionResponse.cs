using System;

namespace Steamworks
{
	// Token: 0x02000151 RID: 337
	public enum EAuthSessionResponse
	{
		// Token: 0x0400084B RID: 2123
		k_EAuthSessionResponseOK,
		// Token: 0x0400084C RID: 2124
		k_EAuthSessionResponseUserNotConnectedToSteam,
		// Token: 0x0400084D RID: 2125
		k_EAuthSessionResponseNoLicenseOrExpired,
		// Token: 0x0400084E RID: 2126
		k_EAuthSessionResponseVACBanned,
		// Token: 0x0400084F RID: 2127
		k_EAuthSessionResponseLoggedInElseWhere,
		// Token: 0x04000850 RID: 2128
		k_EAuthSessionResponseVACCheckTimedOut,
		// Token: 0x04000851 RID: 2129
		k_EAuthSessionResponseAuthTicketCanceled,
		// Token: 0x04000852 RID: 2130
		k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed,
		// Token: 0x04000853 RID: 2131
		k_EAuthSessionResponseAuthTicketInvalid,
		// Token: 0x04000854 RID: 2132
		k_EAuthSessionResponsePublisherIssuedBan,
		// Token: 0x04000855 RID: 2133
		k_EAuthSessionResponseAuthTicketNetworkIdentityFailure
	}
}
