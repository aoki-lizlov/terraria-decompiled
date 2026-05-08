using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007A RID: 122
	[CallbackIdentity(505)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyDataUpdate_t
	{
		// Token: 0x04000144 RID: 324
		public const int k_iCallback = 505;

		// Token: 0x04000145 RID: 325
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000146 RID: 326
		public ulong m_ulSteamIDMember;

		// Token: 0x04000147 RID: 327
		public byte m_bSuccess;
	}
}
