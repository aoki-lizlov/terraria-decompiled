using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000066 RID: 102
	[CallbackIdentity(4524)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_ShowToolTip_t
	{
		// Token: 0x040000F6 RID: 246
		public const int k_iCallback = 4524;

		// Token: 0x040000F7 RID: 247
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000F8 RID: 248
		public string pchMsg;
	}
}
