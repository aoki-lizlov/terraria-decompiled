using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000031 RID: 49
	[CallbackIdentity(333)]
	[StructLayout(0, Pack = 4)]
	public struct GameLobbyJoinRequested_t
	{
		// Token: 0x04000020 RID: 32
		public const int k_iCallback = 333;

		// Token: 0x04000021 RID: 33
		public CSteamID m_steamIDLobby;

		// Token: 0x04000022 RID: 34
		public CSteamID m_steamIDFriend;
	}
}
