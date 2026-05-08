using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E8 RID: 232
	[CallbackIdentity(1101)]
	[StructLayout(2, Pack = 4)]
	public struct UserStatsReceived_t
	{
		// Token: 0x040002D3 RID: 723
		public const int k_iCallback = 1101;

		// Token: 0x040002D4 RID: 724
		[FieldOffset(0)]
		public ulong m_nGameID;

		// Token: 0x040002D5 RID: 725
		[FieldOffset(8)]
		public EResult m_eResult;

		// Token: 0x040002D6 RID: 726
		[FieldOffset(12)]
		public CSteamID m_steamIDUser;
	}
}
