using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000042 RID: 66
	[CallbackIdentity(350)]
	[StructLayout(0, Pack = 4)]
	public struct EquippedProfileItemsChanged_t
	{
		// Token: 0x0400005A RID: 90
		public const int k_iCallback = 350;

		// Token: 0x0400005B RID: 91
		public CSteamID m_steamID;
	}
}
