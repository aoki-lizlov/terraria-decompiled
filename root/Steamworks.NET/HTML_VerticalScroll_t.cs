using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005E RID: 94
	[CallbackIdentity(4512)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_VerticalScroll_t
	{
		// Token: 0x040000D0 RID: 208
		public const int k_iCallback = 4512;

		// Token: 0x040000D1 RID: 209
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000D2 RID: 210
		public uint unScrollMax;

		// Token: 0x040000D3 RID: 211
		public uint unScrollCurrent;

		// Token: 0x040000D4 RID: 212
		public float flPageScale;

		// Token: 0x040000D5 RID: 213
		[MarshalAs(3)]
		public bool bVisible;

		// Token: 0x040000D6 RID: 214
		public uint unPageSize;
	}
}
