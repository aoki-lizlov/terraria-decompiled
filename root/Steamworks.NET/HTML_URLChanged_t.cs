using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000057 RID: 87
	[CallbackIdentity(4505)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_URLChanged_t
	{
		// Token: 0x040000B0 RID: 176
		public const int k_iCallback = 4505;

		// Token: 0x040000B1 RID: 177
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000B2 RID: 178
		public string pchURL;

		// Token: 0x040000B3 RID: 179
		public string pchPostData;

		// Token: 0x040000B4 RID: 180
		[MarshalAs(3)]
		public bool bIsRedirect;

		// Token: 0x040000B5 RID: 181
		public string pchPageTitle;

		// Token: 0x040000B6 RID: 182
		[MarshalAs(3)]
		public bool bNewNavigation;
	}
}
