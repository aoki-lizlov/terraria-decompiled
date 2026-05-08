using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C2 RID: 194
	[CallbackIdentity(1332)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x04000246 RID: 582
		public const int k_iCallback = 1332;

		// Token: 0x04000247 RID: 583
		public SteamAPICall_t m_hFileReadAsync;

		// Token: 0x04000248 RID: 584
		public EResult m_eResult;

		// Token: 0x04000249 RID: 585
		public uint m_nOffset;

		// Token: 0x0400024A RID: 586
		public uint m_cubRead;
	}
}
