using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000051 RID: 81
	[CallbackIdentity(1801)]
	[StructLayout(0, Pack = 4)]
	public struct GSStatsStored_t
	{
		// Token: 0x04000094 RID: 148
		public const int k_iCallback = 1801;

		// Token: 0x04000095 RID: 149
		public EResult m_eResult;

		// Token: 0x04000096 RID: 150
		public CSteamID m_steamIDUser;
	}
}
