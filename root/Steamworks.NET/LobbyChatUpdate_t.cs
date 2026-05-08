using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007B RID: 123
	[CallbackIdentity(506)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyChatUpdate_t
	{
		// Token: 0x04000148 RID: 328
		public const int k_iCallback = 506;

		// Token: 0x04000149 RID: 329
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400014A RID: 330
		public ulong m_ulSteamIDUserChanged;

		// Token: 0x0400014B RID: 331
		public ulong m_ulSteamIDMakingChange;

		// Token: 0x0400014C RID: 332
		public uint m_rgfChatMemberStateChange;
	}
}
