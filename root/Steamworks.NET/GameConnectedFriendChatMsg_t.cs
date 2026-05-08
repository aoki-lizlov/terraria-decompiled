using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200003B RID: 59
	[CallbackIdentity(343)]
	[StructLayout(0, Pack = 4)]
	public struct GameConnectedFriendChatMsg_t
	{
		// Token: 0x04000043 RID: 67
		public const int k_iCallback = 343;

		// Token: 0x04000044 RID: 68
		public CSteamID m_steamIDUser;

		// Token: 0x04000045 RID: 69
		public int m_iMessageID;
	}
}
