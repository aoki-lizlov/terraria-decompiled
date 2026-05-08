using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000511 RID: 1297
	[Flags]
	public enum PropagationFlags
	{
		// Token: 0x04002456 RID: 9302
		None = 0,
		// Token: 0x04002457 RID: 9303
		NoPropagateInherit = 1,
		// Token: 0x04002458 RID: 9304
		InheritOnly = 2
	}
}
