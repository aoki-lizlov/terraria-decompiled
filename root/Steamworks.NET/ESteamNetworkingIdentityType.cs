using System;

namespace Steamworks
{
	// Token: 0x02000165 RID: 357
	public enum ESteamNetworkingIdentityType
	{
		// Token: 0x0400092F RID: 2351
		k_ESteamNetworkingIdentityType_Invalid,
		// Token: 0x04000930 RID: 2352
		k_ESteamNetworkingIdentityType_SteamID = 16,
		// Token: 0x04000931 RID: 2353
		k_ESteamNetworkingIdentityType_XboxPairwiseID,
		// Token: 0x04000932 RID: 2354
		k_ESteamNetworkingIdentityType_SonyPSN,
		// Token: 0x04000933 RID: 2355
		k_ESteamNetworkingIdentityType_GoogleStadia,
		// Token: 0x04000934 RID: 2356
		k_ESteamNetworkingIdentityType_IPAddress = 1,
		// Token: 0x04000935 RID: 2357
		k_ESteamNetworkingIdentityType_GenericString,
		// Token: 0x04000936 RID: 2358
		k_ESteamNetworkingIdentityType_GenericBytes,
		// Token: 0x04000937 RID: 2359
		k_ESteamNetworkingIdentityType_UnknownType,
		// Token: 0x04000938 RID: 2360
		k_ESteamNetworkingIdentityType__Force32bit = 2147483647
	}
}
