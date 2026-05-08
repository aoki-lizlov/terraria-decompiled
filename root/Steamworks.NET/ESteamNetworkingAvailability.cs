using System;

namespace Steamworks
{
	// Token: 0x02000164 RID: 356
	public enum ESteamNetworkingAvailability
	{
		// Token: 0x04000924 RID: 2340
		k_ESteamNetworkingAvailability_CannotTry = -102,
		// Token: 0x04000925 RID: 2341
		k_ESteamNetworkingAvailability_Failed,
		// Token: 0x04000926 RID: 2342
		k_ESteamNetworkingAvailability_Previously,
		// Token: 0x04000927 RID: 2343
		k_ESteamNetworkingAvailability_Retrying = -10,
		// Token: 0x04000928 RID: 2344
		k_ESteamNetworkingAvailability_NeverTried = 1,
		// Token: 0x04000929 RID: 2345
		k_ESteamNetworkingAvailability_Waiting,
		// Token: 0x0400092A RID: 2346
		k_ESteamNetworkingAvailability_Attempting,
		// Token: 0x0400092B RID: 2347
		k_ESteamNetworkingAvailability_Current = 100,
		// Token: 0x0400092C RID: 2348
		k_ESteamNetworkingAvailability_Unknown = 0,
		// Token: 0x0400092D RID: 2349
		k_ESteamNetworkingAvailability__Force32bit = 2147483647
	}
}
