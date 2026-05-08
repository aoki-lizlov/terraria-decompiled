using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000065 RID: 101
	[CallbackIdentity(4523)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_StatusText_t
	{
		// Token: 0x040000F3 RID: 243
		public const int k_iCallback = 4523;

		// Token: 0x040000F4 RID: 244
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000F5 RID: 245
		public string pchMsg;
	}
}
