using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007C RID: 124
	[CallbackIdentity(507)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyChatMsg_t
	{
		// Token: 0x0400014D RID: 333
		public const int k_iCallback = 507;

		// Token: 0x0400014E RID: 334
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400014F RID: 335
		public ulong m_ulSteamIDUser;

		// Token: 0x04000150 RID: 336
		public byte m_eChatEntryType;

		// Token: 0x04000151 RID: 337
		public uint m_iChatID;
	}
}
