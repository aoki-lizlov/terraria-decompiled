using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000072 RID: 114
	[CallbackIdentity(4701)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInventoryFullUpdate_t
	{
		// Token: 0x04000124 RID: 292
		public const int k_iCallback = 4701;

		// Token: 0x04000125 RID: 293
		public SteamInventoryResult_t m_handle;
	}
}
