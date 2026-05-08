using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A5 RID: 165
	[CallbackIdentity(1222)]
	[StructLayout(0, Pack = 4)]
	public struct SteamNetAuthenticationStatus_t
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0000C33B File Offset: 0x0000A53B
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0000C348 File Offset: 0x0000A548
		public string m_debugMsg
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_debugMsg_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_debugMsg_, 256);
			}
		}

		// Token: 0x040001C1 RID: 449
		public const int k_iCallback = 1222;

		// Token: 0x040001C2 RID: 450
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x040001C3 RID: 451
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
