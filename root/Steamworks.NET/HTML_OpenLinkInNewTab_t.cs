using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000059 RID: 89
	[CallbackIdentity(4507)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x040000BB RID: 187
		public const int k_iCallback = 4507;

		// Token: 0x040000BC RID: 188
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000BD RID: 189
		public string pchURL;
	}
}
