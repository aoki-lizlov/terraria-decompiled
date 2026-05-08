using System;

namespace Steamworks
{
	// Token: 0x02000167 RID: 359
	public enum ESteamNetworkingConnectionState
	{
		// Token: 0x04000940 RID: 2368
		k_ESteamNetworkingConnectionState_None,
		// Token: 0x04000941 RID: 2369
		k_ESteamNetworkingConnectionState_Connecting,
		// Token: 0x04000942 RID: 2370
		k_ESteamNetworkingConnectionState_FindingRoute,
		// Token: 0x04000943 RID: 2371
		k_ESteamNetworkingConnectionState_Connected,
		// Token: 0x04000944 RID: 2372
		k_ESteamNetworkingConnectionState_ClosedByPeer,
		// Token: 0x04000945 RID: 2373
		k_ESteamNetworkingConnectionState_ProblemDetectedLocally,
		// Token: 0x04000946 RID: 2374
		k_ESteamNetworkingConnectionState_FinWait = -1,
		// Token: 0x04000947 RID: 2375
		k_ESteamNetworkingConnectionState_Linger = -2,
		// Token: 0x04000948 RID: 2376
		k_ESteamNetworkingConnectionState_Dead = -3,
		// Token: 0x04000949 RID: 2377
		k_ESteamNetworkingConnectionState__Force32Bit = 2147483647
	}
}
