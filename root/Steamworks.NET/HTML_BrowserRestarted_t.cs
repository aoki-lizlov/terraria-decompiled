using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000069 RID: 105
	[CallbackIdentity(4527)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_BrowserRestarted_t
	{
		// Token: 0x040000FE RID: 254
		public const int k_iCallback = 4527;

		// Token: 0x040000FF RID: 255
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000100 RID: 256
		public HHTMLBrowser unOldBrowserHandle;
	}
}
