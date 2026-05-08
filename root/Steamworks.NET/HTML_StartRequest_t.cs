using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000055 RID: 85
	[CallbackIdentity(4503)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_StartRequest_t
	{
		// Token: 0x040000A8 RID: 168
		public const int k_iCallback = 4503;

		// Token: 0x040000A9 RID: 169
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000AA RID: 170
		public string pchURL;

		// Token: 0x040000AB RID: 171
		public string pchTarget;

		// Token: 0x040000AC RID: 172
		public string pchPostData;

		// Token: 0x040000AD RID: 173
		[MarshalAs(3)]
		public bool bIsRedirect;
	}
}
