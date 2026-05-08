using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005C RID: 92
	[CallbackIdentity(4510)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x040000C5 RID: 197
		public const int k_iCallback = 4510;

		// Token: 0x040000C6 RID: 198
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000C7 RID: 199
		[MarshalAs(3)]
		public bool bCanGoBack;

		// Token: 0x040000C8 RID: 200
		[MarshalAs(3)]
		public bool bCanGoForward;
	}
}
