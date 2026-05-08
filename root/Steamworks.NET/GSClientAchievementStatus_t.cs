using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000049 RID: 73
	[CallbackIdentity(206)]
	[StructLayout(0, Pack = 4)]
	public struct GSClientAchievementStatus_t
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000C2DF File Offset: 0x0000A4DF
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public string m_pchAchievement
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchAchievement_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchAchievement_, 128);
			}
		}

		// Token: 0x04000071 RID: 113
		public const int k_iCallback = 206;

		// Token: 0x04000072 RID: 114
		public ulong m_SteamID;

		// Token: 0x04000073 RID: 115
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_pchAchievement_;

		// Token: 0x04000074 RID: 116
		[MarshalAs(3)]
		public bool m_bUnlocked;
	}
}
