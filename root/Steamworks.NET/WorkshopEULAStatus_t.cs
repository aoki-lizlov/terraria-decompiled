using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D8 RID: 216
	[CallbackIdentity(3420)]
	[StructLayout(0, Pack = 4)]
	public struct WorkshopEULAStatus_t
	{
		// Token: 0x04000298 RID: 664
		public const int k_iCallback = 3420;

		// Token: 0x04000299 RID: 665
		public EResult m_eResult;

		// Token: 0x0400029A RID: 666
		public AppId_t m_nAppID;

		// Token: 0x0400029B RID: 667
		public uint m_unVersion;

		// Token: 0x0400029C RID: 668
		public RTime32 m_rtAction;

		// Token: 0x0400029D RID: 669
		[MarshalAs(3)]
		public bool m_bAccepted;

		// Token: 0x0400029E RID: 670
		[MarshalAs(3)]
		public bool m_bNeedsAction;
	}
}
