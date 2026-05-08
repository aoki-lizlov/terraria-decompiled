using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000053 RID: 83
	[CallbackIdentity(4501)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x04000099 RID: 153
		public const int k_iCallback = 4501;

		// Token: 0x0400009A RID: 154
		public HHTMLBrowser unBrowserHandle;
	}
}
