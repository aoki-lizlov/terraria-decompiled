using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000047 RID: 71
	[CallbackIdentity(202)]
	[StructLayout(0, Pack = 4)]
	public struct GSClientDeny_t
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		public string m_rgchOptionalText
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchOptionalText_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchOptionalText_, 128);
			}
		}

		// Token: 0x0400006A RID: 106
		public const int k_iCallback = 202;

		// Token: 0x0400006B RID: 107
		public CSteamID m_SteamID;

		// Token: 0x0400006C RID: 108
		public EDenyReason m_eDenyReason;

		// Token: 0x0400006D RID: 109
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_rgchOptionalText_;
	}
}
