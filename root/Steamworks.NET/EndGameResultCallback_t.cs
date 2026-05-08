using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000088 RID: 136
	[CallbackIdentity(5215)]
	[StructLayout(0, Pack = 4)]
	public struct EndGameResultCallback_t
	{
		// Token: 0x04000186 RID: 390
		public const int k_iCallback = 5215;

		// Token: 0x04000187 RID: 391
		public EResult m_eResult;

		// Token: 0x04000188 RID: 392
		public ulong ullUniqueGameID;
	}
}
