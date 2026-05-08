using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200060F RID: 1551
	[Flags]
	public enum ComponentGuaranteesOptions
	{
		// Token: 0x04002676 RID: 9846
		None = 0,
		// Token: 0x04002677 RID: 9847
		Exchange = 1,
		// Token: 0x04002678 RID: 9848
		Stable = 2,
		// Token: 0x04002679 RID: 9849
		SideBySide = 4
	}
}
