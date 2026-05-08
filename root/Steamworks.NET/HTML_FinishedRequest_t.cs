using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000058 RID: 88
	[CallbackIdentity(4506)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_FinishedRequest_t
	{
		// Token: 0x040000B7 RID: 183
		public const int k_iCallback = 4506;

		// Token: 0x040000B8 RID: 184
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000B9 RID: 185
		public string pchURL;

		// Token: 0x040000BA RID: 186
		public string pchPageTitle;
	}
}
