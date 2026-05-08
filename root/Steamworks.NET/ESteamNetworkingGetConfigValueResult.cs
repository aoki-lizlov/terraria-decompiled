using System;

namespace Steamworks
{
	// Token: 0x0200016C RID: 364
	public enum ESteamNetworkingGetConfigValueResult
	{
		// Token: 0x040009C1 RID: 2497
		k_ESteamNetworkingGetConfigValue_BadValue = -1,
		// Token: 0x040009C2 RID: 2498
		k_ESteamNetworkingGetConfigValue_BadScopeObj = -2,
		// Token: 0x040009C3 RID: 2499
		k_ESteamNetworkingGetConfigValue_BufferTooSmall = -3,
		// Token: 0x040009C4 RID: 2500
		k_ESteamNetworkingGetConfigValue_OK = 1,
		// Token: 0x040009C5 RID: 2501
		k_ESteamNetworkingGetConfigValue_OKInherited,
		// Token: 0x040009C6 RID: 2502
		k_ESteamNetworkingGetConfigValueResult__Force32Bit = 2147483647
	}
}
