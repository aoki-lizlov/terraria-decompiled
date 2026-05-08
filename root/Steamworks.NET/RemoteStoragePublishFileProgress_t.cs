using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000BF RID: 191
	[CallbackIdentity(1329)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStoragePublishFileProgress_t
	{
		// Token: 0x0400023D RID: 573
		public const int k_iCallback = 1329;

		// Token: 0x0400023E RID: 574
		public double m_dPercentFile;

		// Token: 0x0400023F RID: 575
		[MarshalAs(3)]
		public bool m_bPreview;
	}
}
