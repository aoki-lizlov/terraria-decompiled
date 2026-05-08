using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000086 RID: 134
	[CallbackIdentity(5213)]
	[StructLayout(0, Pack = 4)]
	public struct RequestPlayersForGameFinalResultCallback_t
	{
		// Token: 0x0400017E RID: 382
		public const int k_iCallback = 5213;

		// Token: 0x0400017F RID: 383
		public EResult m_eResult;

		// Token: 0x04000180 RID: 384
		public ulong m_ullSearchID;

		// Token: 0x04000181 RID: 385
		public ulong m_ullUniqueGameID;
	}
}
