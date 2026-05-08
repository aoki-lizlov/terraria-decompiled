using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000043 RID: 67
	[CallbackIdentity(351)]
	[StructLayout(0, Pack = 4)]
	public struct EquippedProfileItems_t
	{
		// Token: 0x0400005C RID: 92
		public const int k_iCallback = 351;

		// Token: 0x0400005D RID: 93
		public EResult m_eResult;

		// Token: 0x0400005E RID: 94
		public CSteamID m_steamID;

		// Token: 0x0400005F RID: 95
		[MarshalAs(3)]
		public bool m_bHasAnimatedAvatar;

		// Token: 0x04000060 RID: 96
		[MarshalAs(3)]
		public bool m_bHasAvatarFrame;

		// Token: 0x04000061 RID: 97
		[MarshalAs(3)]
		public bool m_bHasProfileModifier;

		// Token: 0x04000062 RID: 98
		[MarshalAs(3)]
		public bool m_bHasProfileBackground;

		// Token: 0x04000063 RID: 99
		[MarshalAs(3)]
		public bool m_bHasMiniProfileBackground;
	}
}
