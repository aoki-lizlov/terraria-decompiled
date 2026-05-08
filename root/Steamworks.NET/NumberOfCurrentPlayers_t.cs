using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EE RID: 238
	[CallbackIdentity(1107)]
	[StructLayout(0, Pack = 4)]
	public struct NumberOfCurrentPlayers_t
	{
		// Token: 0x040002EE RID: 750
		public const int k_iCallback = 1107;

		// Token: 0x040002EF RID: 751
		public byte m_bSuccess;

		// Token: 0x040002F0 RID: 752
		public int m_cPlayers;
	}
}
