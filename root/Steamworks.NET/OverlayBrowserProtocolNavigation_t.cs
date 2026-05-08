using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000041 RID: 65
	[CallbackIdentity(349)]
	[StructLayout(0, Pack = 4)]
	public struct OverlayBrowserProtocolNavigation_t
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0000C29F File Offset: 0x0000A49F
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		public string rgchURI
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.rgchURI_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.rgchURI_, 1024);
			}
		}

		// Token: 0x04000058 RID: 88
		public const int k_iCallback = 349;

		// Token: 0x04000059 RID: 89
		[MarshalAs(30, SizeConst = 1024)]
		private byte[] rgchURI_;
	}
}
