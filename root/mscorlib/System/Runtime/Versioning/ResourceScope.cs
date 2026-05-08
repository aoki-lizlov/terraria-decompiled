using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000612 RID: 1554
	[Flags]
	public enum ResourceScope
	{
		// Token: 0x0400267E RID: 9854
		None = 0,
		// Token: 0x0400267F RID: 9855
		Machine = 1,
		// Token: 0x04002680 RID: 9856
		Process = 2,
		// Token: 0x04002681 RID: 9857
		AppDomain = 4,
		// Token: 0x04002682 RID: 9858
		Library = 8,
		// Token: 0x04002683 RID: 9859
		Private = 16,
		// Token: 0x04002684 RID: 9860
		Assembly = 32
	}
}
