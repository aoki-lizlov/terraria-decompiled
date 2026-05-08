using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000060 RID: 96
	[CallbackIdentity(4514)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_JSAlert_t
	{
		// Token: 0x040000DE RID: 222
		public const int k_iCallback = 4514;

		// Token: 0x040000DF RID: 223
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000E0 RID: 224
		public string pchMessage;
	}
}
