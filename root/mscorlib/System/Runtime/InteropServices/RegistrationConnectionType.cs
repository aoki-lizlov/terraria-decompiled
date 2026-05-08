using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200069E RID: 1694
	[Flags]
	public enum RegistrationConnectionType
	{
		// Token: 0x0400298D RID: 10637
		SingleUse = 0,
		// Token: 0x0400298E RID: 10638
		MultipleUse = 1,
		// Token: 0x0400298F RID: 10639
		MultiSeparate = 2,
		// Token: 0x04002990 RID: 10640
		Suspended = 4,
		// Token: 0x04002991 RID: 10641
		Surrogate = 8
	}
}
