using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000067 RID: 103
	[CallbackIdentity(4525)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_UpdateToolTip_t
	{
		// Token: 0x040000F9 RID: 249
		public const int k_iCallback = 4525;

		// Token: 0x040000FA RID: 250
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000FB RID: 251
		public string pchMsg;
	}
}
