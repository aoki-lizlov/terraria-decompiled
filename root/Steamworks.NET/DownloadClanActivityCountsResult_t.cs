using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000039 RID: 57
	[CallbackIdentity(341)]
	[StructLayout(0, Pack = 4)]
	public struct DownloadClanActivityCountsResult_t
	{
		// Token: 0x0400003E RID: 62
		public const int k_iCallback = 341;

		// Token: 0x0400003F RID: 63
		[MarshalAs(3)]
		public bool m_bSuccess;
	}
}
