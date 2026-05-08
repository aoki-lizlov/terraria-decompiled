using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E0 RID: 224
	[CallbackIdentity(152)]
	[StructLayout(0, Pack = 4)]
	public struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x040002B2 RID: 690
		public const int k_iCallback = 152;

		// Token: 0x040002B3 RID: 691
		public uint m_unAppID;

		// Token: 0x040002B4 RID: 692
		public ulong m_ulOrderID;

		// Token: 0x040002B5 RID: 693
		public byte m_bAuthorized;
	}
}
