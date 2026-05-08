using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AA RID: 170
	[CallbackIdentity(5703)]
	[StructLayout(0, Pack = 4)]
	public struct SteamRemotePlayTogetherGuestInvite_t
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0000C37B File Offset: 0x0000A57B
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0000C388 File Offset: 0x0000A588
		public string m_szConnectURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectURL_, 1024);
			}
		}

		// Token: 0x040001CF RID: 463
		public const int k_iCallback = 5703;

		// Token: 0x040001D0 RID: 464
		[MarshalAs(30, SizeConst = 1024)]
		private byte[] m_szConnectURL_;
	}
}
