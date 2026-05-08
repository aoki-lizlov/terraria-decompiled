using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FE RID: 254
	[CallbackIdentity(4624)]
	[StructLayout(0, Pack = 4)]
	public struct GetOPFSettingsResult_t
	{
		// Token: 0x04000317 RID: 791
		public const int k_iCallback = 4624;

		// Token: 0x04000318 RID: 792
		public EResult m_eResult;

		// Token: 0x04000319 RID: 793
		public AppId_t m_unVideoAppID;
	}
}
