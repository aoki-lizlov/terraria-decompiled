using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000C6 RID: 198
	[CallbackIdentity(3401)]
	[StructLayout(0, Pack = 4)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0000C47B File Offset: 0x0000A67B
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0000C488 File Offset: 0x0000A688
		public string m_rgchNextCursor
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchNextCursor_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchNextCursor_, 256);
			}
		}

		// Token: 0x04000250 RID: 592
		public const int k_iCallback = 3401;

		// Token: 0x04000251 RID: 593
		public UGCQueryHandle_t m_handle;

		// Token: 0x04000252 RID: 594
		public EResult m_eResult;

		// Token: 0x04000253 RID: 595
		public uint m_unNumResultsReturned;

		// Token: 0x04000254 RID: 596
		public uint m_unTotalMatchingResults;

		// Token: 0x04000255 RID: 597
		[MarshalAs(3)]
		public bool m_bCachedData;

		// Token: 0x04000256 RID: 598
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchNextCursor_;
	}
}
