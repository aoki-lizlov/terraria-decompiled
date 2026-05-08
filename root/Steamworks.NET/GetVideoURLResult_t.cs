using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FD RID: 253
	[CallbackIdentity(4611)]
	[StructLayout(0, Pack = 4)]
	public struct GetVideoURLResult_t
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0000C51B File Offset: 0x0000A71B
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0000C528 File Offset: 0x0000A728
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x04000313 RID: 787
		public const int k_iCallback = 4611;

		// Token: 0x04000314 RID: 788
		public EResult m_eResult;

		// Token: 0x04000315 RID: 789
		public AppId_t m_unVideoAppID;

		// Token: 0x04000316 RID: 790
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchURL_;
	}
}
