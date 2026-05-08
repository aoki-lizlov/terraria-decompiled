using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000068 RID: 104
	[CallbackIdentity(4526)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_HideToolTip_t
	{
		// Token: 0x040000FC RID: 252
		public const int k_iCallback = 4526;

		// Token: 0x040000FD RID: 253
		public HHTMLBrowser unBrowserHandle;
	}
}
