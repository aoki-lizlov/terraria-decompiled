using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005D RID: 93
	[CallbackIdentity(4511)]
	[StructLayout(0, Pack = 4)]
	public struct HTML_HorizontalScroll_t
	{
		// Token: 0x040000C9 RID: 201
		public const int k_iCallback = 4511;

		// Token: 0x040000CA RID: 202
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000CB RID: 203
		public uint unScrollMax;

		// Token: 0x040000CC RID: 204
		public uint unScrollCurrent;

		// Token: 0x040000CD RID: 205
		public float flPageScale;

		// Token: 0x040000CE RID: 206
		[MarshalAs(3)]
		public bool bVisible;

		// Token: 0x040000CF RID: 207
		public uint unPageSize;
	}
}
