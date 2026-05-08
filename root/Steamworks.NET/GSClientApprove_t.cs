using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000046 RID: 70
	[CallbackIdentity(201)]
	[StructLayout(0, Pack = 4)]
	public struct GSClientApprove_t
	{
		// Token: 0x04000067 RID: 103
		public const int k_iCallback = 201;

		// Token: 0x04000068 RID: 104
		public CSteamID m_SteamID;

		// Token: 0x04000069 RID: 105
		public CSteamID m_OwnerSteamID;
	}
}
