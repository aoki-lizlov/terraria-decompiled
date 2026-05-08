using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006B RID: 107
	[CallbackIdentity(2102)]
	[StructLayout(0, Pack = 4)]
	public struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x04000107 RID: 263
		public const int k_iCallback = 2102;

		// Token: 0x04000108 RID: 264
		public HTTPRequestHandle m_hRequest;

		// Token: 0x04000109 RID: 265
		public ulong m_ulContextValue;
	}
}
