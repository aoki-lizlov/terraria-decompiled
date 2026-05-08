using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000056 RID: 86
	[CallbackIdentity(4504)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_CloseBrowser_t
	{
		// Token: 0x040000AE RID: 174
		public const int k_iCallback = 4504;

		// Token: 0x040000AF RID: 175
		public HHTMLBrowser unBrowserHandle;
	}
}
