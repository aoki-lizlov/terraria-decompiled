using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B3 RID: 179
	[CallbackIdentity(1317)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0000C3BB File Offset: 0x0000A5BB
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x040001F1 RID: 497
		public const int k_iCallback = 1317;

		// Token: 0x040001F2 RID: 498
		public EResult m_eResult;

		// Token: 0x040001F3 RID: 499
		public UGCHandle_t m_hFile;

		// Token: 0x040001F4 RID: 500
		public AppId_t m_nAppID;

		// Token: 0x040001F5 RID: 501
		public int m_nSizeInBytes;

		// Token: 0x040001F6 RID: 502
		[MarshalAs(30, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x040001F7 RID: 503
		public ulong m_ulSteamIDOwner;
	}
}
