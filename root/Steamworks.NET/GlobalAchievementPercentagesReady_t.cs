using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F1 RID: 241
	[CallbackIdentity(1110)]
	[StructLayout(0, Pack = 4)]
	public struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x040002F8 RID: 760
		public const int k_iCallback = 1110;

		// Token: 0x040002F9 RID: 761
		public ulong m_nGameID;

		// Token: 0x040002FA RID: 762
		public EResult m_eResult;
	}
}
