using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000170 RID: 368
	[StructLayout(0, Pack = 4)]
	public struct FriendGameInfo_t
	{
		// Token: 0x040009DC RID: 2524
		public CGameID m_gameID;

		// Token: 0x040009DD RID: 2525
		public uint m_unGameIP;

		// Token: 0x040009DE RID: 2526
		public ushort m_usGamePort;

		// Token: 0x040009DF RID: 2527
		public ushort m_usQueryPort;

		// Token: 0x040009E0 RID: 2528
		public CSteamID m_steamIDLobby;
	}
}
