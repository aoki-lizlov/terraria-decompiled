using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007E RID: 126
	[CallbackIdentity(510)]
	[StructLayout(0, Pack = 4)]
	public struct LobbyMatchList_t
	{
		// Token: 0x04000157 RID: 343
		public const int k_iCallback = 510;

		// Token: 0x04000158 RID: 344
		public uint m_nLobbiesMatching;
	}
}
