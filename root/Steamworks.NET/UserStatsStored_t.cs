using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E9 RID: 233
	[CallbackIdentity(1102)]
	[StructLayout(0, Pack = 4)]
	public struct UserStatsStored_t
	{
		// Token: 0x040002D7 RID: 727
		public const int k_iCallback = 1102;

		// Token: 0x040002D8 RID: 728
		public ulong m_nGameID;

		// Token: 0x040002D9 RID: 729
		public EResult m_eResult;
	}
}
