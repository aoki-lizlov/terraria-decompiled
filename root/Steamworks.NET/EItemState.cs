using System;

namespace Steamworks
{
	// Token: 0x0200013B RID: 315
	[Flags]
	public enum EItemState
	{
		// Token: 0x04000742 RID: 1858
		k_EItemStateNone = 0,
		// Token: 0x04000743 RID: 1859
		k_EItemStateSubscribed = 1,
		// Token: 0x04000744 RID: 1860
		k_EItemStateLegacyItem = 2,
		// Token: 0x04000745 RID: 1861
		k_EItemStateInstalled = 4,
		// Token: 0x04000746 RID: 1862
		k_EItemStateNeedsUpdate = 8,
		// Token: 0x04000747 RID: 1863
		k_EItemStateDownloading = 16,
		// Token: 0x04000748 RID: 1864
		k_EItemStateDownloadPending = 32,
		// Token: 0x04000749 RID: 1865
		k_EItemStateDisabledLocally = 64
	}
}
