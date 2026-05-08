using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007F RID: 127
	[CallbackIdentity(512)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyKicked_t
	{
		// Token: 0x04000159 RID: 345
		public const int k_iCallback = 512;

		// Token: 0x0400015A RID: 346
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400015B RID: 347
		public ulong m_ulSteamIDAdmin;

		// Token: 0x0400015C RID: 348
		public byte m_bKickedDueToDisconnect;
	}
}
