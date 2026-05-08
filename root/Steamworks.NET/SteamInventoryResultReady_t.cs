using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000071 RID: 113
	[CallbackIdentity(4700)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInventoryResultReady_t
	{
		// Token: 0x04000121 RID: 289
		public const int k_iCallback = 4700;

		// Token: 0x04000122 RID: 290
		public SteamInventoryResult_t m_handle;

		// Token: 0x04000123 RID: 291
		public EResult m_result;
	}
}
