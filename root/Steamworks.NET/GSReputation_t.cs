using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004D RID: 77
	[CallbackIdentity(209)]
	[StructLayout(0, Pack = 4)]
	public struct GSReputation_t
	{
		// Token: 0x04000081 RID: 129
		public const int k_iCallback = 209;

		// Token: 0x04000082 RID: 130
		public EResult m_eResult;

		// Token: 0x04000083 RID: 131
		public uint m_unReputationScore;

		// Token: 0x04000084 RID: 132
		[MarshalAs(3)]
		public bool m_bBanned;

		// Token: 0x04000085 RID: 133
		public uint m_unBannedIP;

		// Token: 0x04000086 RID: 134
		public ushort m_usBannedPort;

		// Token: 0x04000087 RID: 135
		public ulong m_ulBannedGameID;

		// Token: 0x04000088 RID: 136
		public uint m_unBanExpires;
	}
}
