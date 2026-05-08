using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000048 RID: 72
	[CallbackIdentity(203)]
	[StructLayout(0, Pack = 4)]
	public struct GSClientKick_t
	{
		// Token: 0x0400006E RID: 110
		public const int k_iCallback = 203;

		// Token: 0x0400006F RID: 111
		public CSteamID m_SteamID;

		// Token: 0x04000070 RID: 112
		public EDenyReason m_eDenyReason;
	}
}
