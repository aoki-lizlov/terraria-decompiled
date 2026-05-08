using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004C RID: 76
	[CallbackIdentity(208)]
	[StructLayout(0, Pack = 1)]
	public struct GSClientGroupStatus_t
	{
		// Token: 0x0400007C RID: 124
		public const int k_iCallback = 208;

		// Token: 0x0400007D RID: 125
		public CSteamID m_SteamIDUser;

		// Token: 0x0400007E RID: 126
		public CSteamID m_SteamIDGroup;

		// Token: 0x0400007F RID: 127
		[MarshalAs(3)]
		public bool m_bMember;

		// Token: 0x04000080 RID: 128
		[MarshalAs(3)]
		public bool m_bOfficer;
	}
}
