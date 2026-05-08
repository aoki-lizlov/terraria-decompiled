using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000076 RID: 118
	[CallbackIdentity(4705)]
	[StructLayout(0, Pack = 4)]
	public struct SteamInventoryRequestPricesResult_t
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000C2FF File Offset: 0x0000A4FF
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0000C30C File Offset: 0x0000A50C
		public string m_rgchCurrency
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchCurrency_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchCurrency_, 4);
			}
		}

		// Token: 0x04000130 RID: 304
		public const int k_iCallback = 4705;

		// Token: 0x04000131 RID: 305
		public EResult m_result;

		// Token: 0x04000132 RID: 306
		[MarshalAs(30, SizeConst = 4)]
		private byte[] m_rgchCurrency_;
	}
}
