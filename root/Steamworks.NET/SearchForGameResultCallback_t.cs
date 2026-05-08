using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000083 RID: 131
	[CallbackIdentity(5202)]
	[StructLayout(0, Pack = 4)]
	public struct SearchForGameResultCallback_t
	{
		// Token: 0x04000169 RID: 361
		public const int k_iCallback = 5202;

		// Token: 0x0400016A RID: 362
		public ulong m_ullSearchID;

		// Token: 0x0400016B RID: 363
		public EResult m_eResult;

		// Token: 0x0400016C RID: 364
		public int m_nCountPlayersInGame;

		// Token: 0x0400016D RID: 365
		public int m_nCountAcceptedGame;

		// Token: 0x0400016E RID: 366
		public CSteamID m_steamIDHost;

		// Token: 0x0400016F RID: 367
		[MarshalAs(3)]
		public bool m_bFinalCallback;
	}
}
