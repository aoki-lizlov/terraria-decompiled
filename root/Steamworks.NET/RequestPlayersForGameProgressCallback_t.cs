using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000084 RID: 132
	[CallbackIdentity(5211)]
	[StructLayout(0, Pack = 4)]
	public struct RequestPlayersForGameProgressCallback_t
	{
		// Token: 0x04000170 RID: 368
		public const int k_iCallback = 5211;

		// Token: 0x04000171 RID: 369
		public EResult m_eResult;

		// Token: 0x04000172 RID: 370
		public ulong m_ullSearchID;
	}
}
