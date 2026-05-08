using System;

namespace Steamworks
{
	// Token: 0x02000155 RID: 341
	public enum EChatRoomEnterResponse
	{
		// Token: 0x04000875 RID: 2165
		k_EChatRoomEnterResponseSuccess = 1,
		// Token: 0x04000876 RID: 2166
		k_EChatRoomEnterResponseDoesntExist,
		// Token: 0x04000877 RID: 2167
		k_EChatRoomEnterResponseNotAllowed,
		// Token: 0x04000878 RID: 2168
		k_EChatRoomEnterResponseFull,
		// Token: 0x04000879 RID: 2169
		k_EChatRoomEnterResponseError,
		// Token: 0x0400087A RID: 2170
		k_EChatRoomEnterResponseBanned,
		// Token: 0x0400087B RID: 2171
		k_EChatRoomEnterResponseLimited,
		// Token: 0x0400087C RID: 2172
		k_EChatRoomEnterResponseClanDisabled,
		// Token: 0x0400087D RID: 2173
		k_EChatRoomEnterResponseCommunityBan,
		// Token: 0x0400087E RID: 2174
		k_EChatRoomEnterResponseMemberBlockedYou,
		// Token: 0x0400087F RID: 2175
		k_EChatRoomEnterResponseYouBlockedMember,
		// Token: 0x04000880 RID: 2176
		k_EChatRoomEnterResponseRatelimitExceeded = 15
	}
}
