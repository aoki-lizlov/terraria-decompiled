using System;

namespace Steamworks
{
	// Token: 0x02000129 RID: 297
	[Flags]
	public enum ERemoteStoragePlatform
	{
		// Token: 0x040006B0 RID: 1712
		k_ERemoteStoragePlatformNone = 0,
		// Token: 0x040006B1 RID: 1713
		k_ERemoteStoragePlatformWindows = 1,
		// Token: 0x040006B2 RID: 1714
		k_ERemoteStoragePlatformOSX = 2,
		// Token: 0x040006B3 RID: 1715
		k_ERemoteStoragePlatformPS3 = 4,
		// Token: 0x040006B4 RID: 1716
		k_ERemoteStoragePlatformLinux = 8,
		// Token: 0x040006B5 RID: 1717
		k_ERemoteStoragePlatformSwitch = 16,
		// Token: 0x040006B6 RID: 1718
		k_ERemoteStoragePlatformAndroid = 32,
		// Token: 0x040006B7 RID: 1719
		k_ERemoteStoragePlatformIOS = 64,
		// Token: 0x040006B8 RID: 1720
		k_ERemoteStoragePlatformAll = -1
	}
}
