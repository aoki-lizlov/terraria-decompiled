using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E4 RID: 228
	[CallbackIdentity(165)]
	[StructLayout(0, Pack = 4)]
	public struct StoreAuthURLResponse_t
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0000C4BB File Offset: 0x0000A6BB
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public string m_szURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szURL_, 512);
			}
		}

		// Token: 0x040002BD RID: 701
		public const int k_iCallback = 165;

		// Token: 0x040002BE RID: 702
		[MarshalAs(30, SizeConst = 512)]
		private byte[] m_szURL_;
	}
}
