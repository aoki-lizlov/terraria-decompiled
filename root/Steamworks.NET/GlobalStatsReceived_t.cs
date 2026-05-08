using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F3 RID: 243
	[CallbackIdentity(1112)]
	[StructLayout(0, Pack = 4)]
	public struct GlobalStatsReceived_t
	{
		// Token: 0x040002FE RID: 766
		public const int k_iCallback = 1112;

		// Token: 0x040002FF RID: 767
		public ulong m_nGameID;

		// Token: 0x04000300 RID: 768
		public EResult m_eResult;
	}
}
