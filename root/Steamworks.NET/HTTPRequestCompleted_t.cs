using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006A RID: 106
	[CallbackIdentity(2101)]
	[StructLayout(0, Pack = 4)]
	public struct HTTPRequestCompleted_t
	{
		// Token: 0x04000101 RID: 257
		public const int k_iCallback = 2101;

		// Token: 0x04000102 RID: 258
		public HTTPRequestHandle m_hRequest;

		// Token: 0x04000103 RID: 259
		public ulong m_ulContextValue;

		// Token: 0x04000104 RID: 260
		[MarshalAs(3)]
		public bool m_bRequestSuccessful;

		// Token: 0x04000105 RID: 261
		public EHTTPStatusCode m_eStatusCode;

		// Token: 0x04000106 RID: 262
		public uint m_unBodySize;
	}
}
