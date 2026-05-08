using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DB RID: 219
	[CallbackIdentity(103)]
	[StructLayout(0, Pack = 4)]
	public struct SteamServersDisconnected_t
	{
		// Token: 0x040002A3 RID: 675
		public const int k_iCallback = 103;

		// Token: 0x040002A4 RID: 676
		public EResult m_eResult;
	}
}
