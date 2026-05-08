using System;

namespace Steamworks
{
	// Token: 0x02000168 RID: 360
	public enum ESteamNetConnectionEnd
	{
		// Token: 0x0400094B RID: 2379
		k_ESteamNetConnectionEnd_Invalid,
		// Token: 0x0400094C RID: 2380
		k_ESteamNetConnectionEnd_App_Min = 1000,
		// Token: 0x0400094D RID: 2381
		k_ESteamNetConnectionEnd_App_Generic = 1000,
		// Token: 0x0400094E RID: 2382
		k_ESteamNetConnectionEnd_App_Max = 1999,
		// Token: 0x0400094F RID: 2383
		k_ESteamNetConnectionEnd_AppException_Min,
		// Token: 0x04000950 RID: 2384
		k_ESteamNetConnectionEnd_AppException_Generic = 2000,
		// Token: 0x04000951 RID: 2385
		k_ESteamNetConnectionEnd_AppException_Max = 2999,
		// Token: 0x04000952 RID: 2386
		k_ESteamNetConnectionEnd_Local_Min,
		// Token: 0x04000953 RID: 2387
		k_ESteamNetConnectionEnd_Local_OfflineMode,
		// Token: 0x04000954 RID: 2388
		k_ESteamNetConnectionEnd_Local_ManyRelayConnectivity,
		// Token: 0x04000955 RID: 2389
		k_ESteamNetConnectionEnd_Local_HostedServerPrimaryRelay,
		// Token: 0x04000956 RID: 2390
		k_ESteamNetConnectionEnd_Local_NetworkConfig,
		// Token: 0x04000957 RID: 2391
		k_ESteamNetConnectionEnd_Local_Rights,
		// Token: 0x04000958 RID: 2392
		k_ESteamNetConnectionEnd_Local_P2P_ICE_NoPublicAddresses,
		// Token: 0x04000959 RID: 2393
		k_ESteamNetConnectionEnd_Local_Max = 3999,
		// Token: 0x0400095A RID: 2394
		k_ESteamNetConnectionEnd_Remote_Min,
		// Token: 0x0400095B RID: 2395
		k_ESteamNetConnectionEnd_Remote_Timeout,
		// Token: 0x0400095C RID: 2396
		k_ESteamNetConnectionEnd_Remote_BadCrypt,
		// Token: 0x0400095D RID: 2397
		k_ESteamNetConnectionEnd_Remote_BadCert,
		// Token: 0x0400095E RID: 2398
		k_ESteamNetConnectionEnd_Remote_BadProtocolVersion = 4006,
		// Token: 0x0400095F RID: 2399
		k_ESteamNetConnectionEnd_Remote_P2P_ICE_NoPublicAddresses,
		// Token: 0x04000960 RID: 2400
		k_ESteamNetConnectionEnd_Remote_Max = 4999,
		// Token: 0x04000961 RID: 2401
		k_ESteamNetConnectionEnd_Misc_Min,
		// Token: 0x04000962 RID: 2402
		k_ESteamNetConnectionEnd_Misc_Generic,
		// Token: 0x04000963 RID: 2403
		k_ESteamNetConnectionEnd_Misc_InternalError,
		// Token: 0x04000964 RID: 2404
		k_ESteamNetConnectionEnd_Misc_Timeout,
		// Token: 0x04000965 RID: 2405
		k_ESteamNetConnectionEnd_Misc_SteamConnectivity = 5005,
		// Token: 0x04000966 RID: 2406
		k_ESteamNetConnectionEnd_Misc_NoRelaySessionsToClient,
		// Token: 0x04000967 RID: 2407
		k_ESteamNetConnectionEnd_Misc_P2P_Rendezvous = 5008,
		// Token: 0x04000968 RID: 2408
		k_ESteamNetConnectionEnd_Misc_P2P_NAT_Firewall,
		// Token: 0x04000969 RID: 2409
		k_ESteamNetConnectionEnd_Misc_PeerSentNoConnection,
		// Token: 0x0400096A RID: 2410
		k_ESteamNetConnectionEnd_Misc_Max = 5999,
		// Token: 0x0400096B RID: 2411
		k_ESteamNetConnectionEnd__Force32Bit = 2147483647
	}
}
