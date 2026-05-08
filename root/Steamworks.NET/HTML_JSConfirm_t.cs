using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000061 RID: 97
	[CallbackIdentity(4515)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_JSConfirm_t
	{
		// Token: 0x040000E1 RID: 225
		public const int k_iCallback = 4515;

		// Token: 0x040000E2 RID: 226
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000E3 RID: 227
		public string pchMessage;
	}
}
