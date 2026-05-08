using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005A RID: 90
	[CallbackIdentity(4508)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_ChangedTitle_t
	{
		// Token: 0x040000BE RID: 190
		public const int k_iCallback = 4508;

		// Token: 0x040000BF RID: 191
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000C0 RID: 192
		public string pchTitle;
	}
}
