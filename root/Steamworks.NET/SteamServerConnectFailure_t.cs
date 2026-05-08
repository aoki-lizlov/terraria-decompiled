using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DA RID: 218
	[CallbackIdentity(102)]
	[StructLayout(0, Pack = 4)]
	public struct SteamServerConnectFailure_t
	{
		// Token: 0x040002A0 RID: 672
		public const int k_iCallback = 102;

		// Token: 0x040002A1 RID: 673
		public EResult m_eResult;

		// Token: 0x040002A2 RID: 674
		[MarshalAs(3)]
		public bool m_bStillRetrying;
	}
}
