using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000052 RID: 82
	[CallbackIdentity(1108)]
	[StructLayout(0, Pack = 4)]
	public struct GSStatsUnloaded_t
	{
		// Token: 0x04000097 RID: 151
		public const int k_iCallback = 1108;

		// Token: 0x04000098 RID: 152
		public CSteamID m_steamIDUser;
	}
}
