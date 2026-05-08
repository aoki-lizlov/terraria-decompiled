using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000033 RID: 51
	[CallbackIdentity(335)]
	[StructLayout(0, Pack = 4)]
	public struct ClanOfficerListResponse_t
	{
		// Token: 0x04000028 RID: 40
		public const int k_iCallback = 335;

		// Token: 0x04000029 RID: 41
		public CSteamID m_steamIDClan;

		// Token: 0x0400002A RID: 42
		public int m_cOfficers;

		// Token: 0x0400002B RID: 43
		public byte m_bSuccess;
	}
}
