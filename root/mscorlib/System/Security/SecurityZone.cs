using System;

namespace System.Security
{
	// Token: 0x0200039B RID: 923
	public enum SecurityZone
	{
		// Token: 0x04001D5F RID: 7519
		Internet = 3,
		// Token: 0x04001D60 RID: 7520
		Intranet = 1,
		// Token: 0x04001D61 RID: 7521
		MyComputer = 0,
		// Token: 0x04001D62 RID: 7522
		NoZone = -1,
		// Token: 0x04001D63 RID: 7523
		Trusted = 2,
		// Token: 0x04001D64 RID: 7524
		Untrusted = 4
	}
}
