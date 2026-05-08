using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000044 RID: 68
	[CallbackIdentity(1701)]
	[StructLayout(0, Pack = 4)]
	public struct GCMessageAvailable_t
	{
		// Token: 0x04000064 RID: 100
		public const int k_iCallback = 1701;

		// Token: 0x04000065 RID: 101
		public uint m_nMessageSize;
	}
}
