using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000030 RID: 48
	[CallbackIdentity(332)]
	[StructLayout(0, Pack = 4)]
	public struct GameServerChangeRequested_t
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0000C245 File Offset: 0x0000A445
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0000C252 File Offset: 0x0000A452
		public string m_rgchServer
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchServer_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchServer_, 64);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0000C262 File Offset: 0x0000A462
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0000C26F File Offset: 0x0000A46F
		public string m_rgchPassword
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchPassword_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchPassword_, 64);
			}
		}

		// Token: 0x0400001D RID: 29
		public const int k_iCallback = 332;

		// Token: 0x0400001E RID: 30
		[MarshalAs(30, SizeConst = 64)]
		private byte[] m_rgchServer_;

		// Token: 0x0400001F RID: 31
		[MarshalAs(30, SizeConst = 64)]
		private byte[] m_rgchPassword_;
	}
}
