using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C4 RID: 196
	[CallbackIdentity(2301)]
	[StructLayout(0, Pack = 4)]
	public struct ScreenshotReady_t
	{
		// Token: 0x0400024C RID: 588
		public const int k_iCallback = 2301;

		// Token: 0x0400024D RID: 589
		public ScreenshotHandle m_hLocal;

		// Token: 0x0400024E RID: 590
		public EResult m_eResult;
	}
}
