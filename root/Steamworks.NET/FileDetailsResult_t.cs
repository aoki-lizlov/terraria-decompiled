using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002C RID: 44
	[CallbackIdentity(1023)]
	[StructLayout(0, Pack = 4)]
	public struct FileDetailsResult_t
	{
		// Token: 0x0400000B RID: 11
		public const int k_iCallback = 1023;

		// Token: 0x0400000C RID: 12
		public EResult m_eResult;

		// Token: 0x0400000D RID: 13
		public ulong m_ulFileSize;

		// Token: 0x0400000E RID: 14
		[MarshalAs(30, SizeConst = 20)]
		public byte[] m_FileSHA;

		// Token: 0x0400000F RID: 15
		public uint m_unFlags;
	}
}
