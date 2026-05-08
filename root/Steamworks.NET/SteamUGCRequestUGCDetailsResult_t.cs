using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C7 RID: 199
	[CallbackIdentity(3402)]
	[StructLayout(0, Pack = 4)]
	public struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x04000257 RID: 599
		public const int k_iCallback = 3402;

		// Token: 0x04000258 RID: 600
		public SteamUGCDetails_t m_details;

		// Token: 0x04000259 RID: 601
		[MarshalAs(3)]
		public bool m_bCachedData;
	}
}
