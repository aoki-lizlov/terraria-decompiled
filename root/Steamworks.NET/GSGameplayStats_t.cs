using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004B RID: 75
	[CallbackIdentity(207)]
	[StructLayout(0, Pack = 4)]
	public struct GSGameplayStats_t
	{
		// Token: 0x04000077 RID: 119
		public const int k_iCallback = 207;

		// Token: 0x04000078 RID: 120
		public EResult m_eResult;

		// Token: 0x04000079 RID: 121
		public int m_nRank;

		// Token: 0x0400007A RID: 122
		public uint m_unTotalConnects;

		// Token: 0x0400007B RID: 123
		public uint m_unTotalMinutesPlayed;
	}
}
