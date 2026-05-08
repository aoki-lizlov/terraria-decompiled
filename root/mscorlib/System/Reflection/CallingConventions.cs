using System;

namespace System.Reflection
{
	// Token: 0x02000864 RID: 2148
	[Flags]
	public enum CallingConventions
	{
		// Token: 0x04002DFE RID: 11774
		Standard = 1,
		// Token: 0x04002DFF RID: 11775
		VarArgs = 2,
		// Token: 0x04002E00 RID: 11776
		Any = 3,
		// Token: 0x04002E01 RID: 11777
		HasThis = 32,
		// Token: 0x04002E02 RID: 11778
		ExplicitThis = 64
	}
}
