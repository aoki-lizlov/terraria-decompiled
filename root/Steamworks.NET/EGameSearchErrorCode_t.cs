using System;

namespace Steamworks
{
	// Token: 0x0200015E RID: 350
	public enum EGameSearchErrorCode_t
	{
		// Token: 0x040008D4 RID: 2260
		k_EGameSearchErrorCode_OK = 1,
		// Token: 0x040008D5 RID: 2261
		k_EGameSearchErrorCode_Failed_Search_Already_In_Progress,
		// Token: 0x040008D6 RID: 2262
		k_EGameSearchErrorCode_Failed_No_Search_In_Progress,
		// Token: 0x040008D7 RID: 2263
		k_EGameSearchErrorCode_Failed_Not_Lobby_Leader,
		// Token: 0x040008D8 RID: 2264
		k_EGameSearchErrorCode_Failed_No_Host_Available,
		// Token: 0x040008D9 RID: 2265
		k_EGameSearchErrorCode_Failed_Search_Params_Invalid,
		// Token: 0x040008DA RID: 2266
		k_EGameSearchErrorCode_Failed_Offline,
		// Token: 0x040008DB RID: 2267
		k_EGameSearchErrorCode_Failed_NotAuthorized,
		// Token: 0x040008DC RID: 2268
		k_EGameSearchErrorCode_Failed_Unknown_Error
	}
}
