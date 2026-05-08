using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004A RID: 74
	[CallbackIdentity(115)]
	[StructLayout(0, Pack = 4)]
	public struct GSPolicyResponse_t
	{
		// Token: 0x04000075 RID: 117
		public const int k_iCallback = 115;

		// Token: 0x04000076 RID: 118
		public byte m_bSecure;
	}
}
