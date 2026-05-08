using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017B RID: 379
	[StructLayout(0, Pack = 4)]
	public struct CallbackMsg_t
	{
		// Token: 0x04000A23 RID: 2595
		public int m_hSteamUser;

		// Token: 0x04000A24 RID: 2596
		public int m_iCallback;

		// Token: 0x04000A25 RID: 2597
		public IntPtr m_pubParam;

		// Token: 0x04000A26 RID: 2598
		public int m_cubParam;
	}
}
