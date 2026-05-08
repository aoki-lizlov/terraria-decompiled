using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F6 RID: 246
	[CallbackIdentity(703)]
	[StructLayout(0, Pack = 4)]
	public struct SteamAPICallCompleted_t
	{
		// Token: 0x04000304 RID: 772
		public const int k_iCallback = 703;

		// Token: 0x04000305 RID: 773
		public SteamAPICall_t m_hAsyncCall;

		// Token: 0x04000306 RID: 774
		public int m_iCallback;

		// Token: 0x04000307 RID: 775
		public uint m_cubParam;
	}
}
