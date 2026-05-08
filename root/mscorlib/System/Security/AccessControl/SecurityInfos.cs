using System;

namespace System.Security.AccessControl
{
	// Token: 0x0200051B RID: 1307
	[Flags]
	public enum SecurityInfos
	{
		// Token: 0x04002484 RID: 9348
		Owner = 1,
		// Token: 0x04002485 RID: 9349
		Group = 2,
		// Token: 0x04002486 RID: 9350
		DiscretionaryAcl = 4,
		// Token: 0x04002487 RID: 9351
		SystemAcl = 8
	}
}
