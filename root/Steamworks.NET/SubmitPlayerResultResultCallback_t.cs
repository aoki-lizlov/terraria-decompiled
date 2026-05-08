using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000087 RID: 135
	[CallbackIdentity(5214)]
	[StructLayout(0, Pack = 4)]
	public struct SubmitPlayerResultResultCallback_t
	{
		// Token: 0x04000182 RID: 386
		public const int k_iCallback = 5214;

		// Token: 0x04000183 RID: 387
		public EResult m_eResult;

		// Token: 0x04000184 RID: 388
		public ulong ullUniqueGameID;

		// Token: 0x04000185 RID: 389
		public CSteamID steamIDPlayer;
	}
}
