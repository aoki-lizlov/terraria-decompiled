using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000064 RID: 100
	[CallbackIdentity(4522)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_SetCursor_t
	{
		// Token: 0x040000F0 RID: 240
		public const int k_iCallback = 4522;

		// Token: 0x040000F1 RID: 241
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000F2 RID: 242
		public uint eMouseCursor;
	}
}
