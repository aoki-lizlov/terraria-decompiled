using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000062 RID: 98
	[CallbackIdentity(4516)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_FileOpenDialog_t
	{
		// Token: 0x040000E4 RID: 228
		public const int k_iCallback = 4516;

		// Token: 0x040000E5 RID: 229
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000E6 RID: 230
		public string pchTitle;

		// Token: 0x040000E7 RID: 231
		public string pchInitialFile;
	}
}
