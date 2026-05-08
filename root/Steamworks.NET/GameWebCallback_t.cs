using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E3 RID: 227
	[CallbackIdentity(164)]
	[StructLayout(0, Pack = 4)]
	public struct GameWebCallback_t
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000C49B File Offset: 0x0000A69B
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 256);
			}
		}

		// Token: 0x040002BB RID: 699
		public const int k_iCallback = 164;

		// Token: 0x040002BC RID: 700
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_szURL_;
	}
}
