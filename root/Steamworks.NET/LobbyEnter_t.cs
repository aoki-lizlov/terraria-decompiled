using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000079 RID: 121
	[CallbackIdentity(504)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyEnter_t
	{
		// Token: 0x0400013F RID: 319
		public const int k_iCallback = 504;

		// Token: 0x04000140 RID: 320
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000141 RID: 321
		public uint m_rgfChatPermissions;

		// Token: 0x04000142 RID: 322
		[MarshalAs(3)]
		public bool m_bLocked;

		// Token: 0x04000143 RID: 323
		public uint m_EChatRoomEnterResponse;
	}
}
