using System;

namespace Microsoft.Win32
{
	// Token: 0x02000084 RID: 132
	public enum RegistryValueKind
	{
		// Token: 0x04000E55 RID: 3669
		String = 1,
		// Token: 0x04000E56 RID: 3670
		ExpandString,
		// Token: 0x04000E57 RID: 3671
		Binary,
		// Token: 0x04000E58 RID: 3672
		DWord,
		// Token: 0x04000E59 RID: 3673
		MultiString = 7,
		// Token: 0x04000E5A RID: 3674
		QWord = 11,
		// Token: 0x04000E5B RID: 3675
		Unknown = 0,
		// Token: 0x04000E5C RID: 3676
		None = -1
	}
}
