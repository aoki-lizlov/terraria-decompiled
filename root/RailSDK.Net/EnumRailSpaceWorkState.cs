using System;

namespace rail
{
	// Token: 0x0200017D RID: 381
	public enum EnumRailSpaceWorkState
	{
		// Token: 0x0400054F RID: 1359
		kRailSpaceWorkStateNone,
		// Token: 0x04000550 RID: 1360
		kRailSpaceWorkStateDownloaded,
		// Token: 0x04000551 RID: 1361
		kRailSpaceWorkStateNeedsSync,
		// Token: 0x04000552 RID: 1362
		kRailSpaceWorkStateDownloading = 4,
		// Token: 0x04000553 RID: 1363
		kRailSpaceWorkStateUploading = 8
	}
}
