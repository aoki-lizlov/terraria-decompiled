using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DD RID: 221
	[CallbackIdentity(117)]
	[StructLayout(0, Pack = 4)]
	public struct IPCFailure_t
	{
		// Token: 0x040002AB RID: 683
		public const int k_iCallback = 117;

		// Token: 0x040002AC RID: 684
		public byte m_eFailureType;
	}
}
