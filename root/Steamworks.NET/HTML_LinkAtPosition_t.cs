using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005F RID: 95
	[CallbackIdentity(4513)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_LinkAtPosition_t
	{
		// Token: 0x040000D7 RID: 215
		public const int k_iCallback = 4513;

		// Token: 0x040000D8 RID: 216
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000D9 RID: 217
		public uint x;

		// Token: 0x040000DA RID: 218
		public uint y;

		// Token: 0x040000DB RID: 219
		public string pchURL;

		// Token: 0x040000DC RID: 220
		[MarshalAs(3)]
		public bool bInput;

		// Token: 0x040000DD RID: 221
		[MarshalAs(3)]
		public bool bLiveLink;
	}
}
