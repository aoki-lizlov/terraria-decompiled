using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000AB RID: 171
	[CallbackIdentity(1307)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageFileShareResult_t
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0000C39B File Offset: 0x0000A59B
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		public string m_rgchFilename
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchFilename_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchFilename_, 260);
			}
		}

		// Token: 0x040001D1 RID: 465
		public const int k_iCallback = 1307;

		// Token: 0x040001D2 RID: 466
		public EResult m_eResult;

		// Token: 0x040001D3 RID: 467
		public UGCHandle_t m_hFile;

		// Token: 0x040001D4 RID: 468
		[MarshalAs(30, SizeConst = 260)]
		private byte[] m_rgchFilename_;
	}
}
