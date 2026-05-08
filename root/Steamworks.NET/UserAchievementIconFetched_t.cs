using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F0 RID: 240
	[CallbackIdentity(1109)]
	[StructLayout(0, Pack = 4)]
	public struct UserAchievementIconFetched_t
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0000C4FB File Offset: 0x0000A6FB
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0000C508 File Offset: 0x0000A708
		public string m_rgchAchievementName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchAchievementName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchAchievementName_, 128);
			}
		}

		// Token: 0x040002F3 RID: 755
		public const int k_iCallback = 1109;

		// Token: 0x040002F4 RID: 756
		public CGameID m_nGameID;

		// Token: 0x040002F5 RID: 757
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x040002F6 RID: 758
		[MarshalAs(3)]
		public bool m_bAchieved;

		// Token: 0x040002F7 RID: 759
		public int m_nIconHandle;
	}
}
