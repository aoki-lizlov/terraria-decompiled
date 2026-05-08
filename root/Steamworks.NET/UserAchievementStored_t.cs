using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EA RID: 234
	[CallbackIdentity(1103)]
	[StructLayout(0, Pack = 4)]
	public struct UserAchievementStored_t
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0000C4DB File Offset: 0x0000A6DB
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
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

		// Token: 0x040002DA RID: 730
		public const int k_iCallback = 1103;

		// Token: 0x040002DB RID: 731
		public ulong m_nGameID;

		// Token: 0x040002DC RID: 732
		[MarshalAs(3)]
		public bool m_bGroupAchievement;

		// Token: 0x040002DD RID: 733
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_rgchAchievementName_;

		// Token: 0x040002DE RID: 734
		public uint m_nCurProgress;

		// Token: 0x040002DF RID: 735
		public uint m_nMaxProgress;
	}
}
