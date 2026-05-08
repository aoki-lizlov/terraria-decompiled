using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003D RID: 61
	[CallbackIdentity(345)]
	[StructLayout(0, Pack = 4)]
	public struct FriendsIsFollowing_t
	{
		// Token: 0x0400004A RID: 74
		public const int k_iCallback = 345;

		// Token: 0x0400004B RID: 75
		public EResult m_eResult;

		// Token: 0x0400004C RID: 76
		public CSteamID m_steamID;

		// Token: 0x0400004D RID: 77
		[MarshalAs(3)]
		public bool m_bIsFollowing;
	}
}
