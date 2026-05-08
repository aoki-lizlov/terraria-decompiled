using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A28 RID: 2600
	public enum ContractFailureKind
	{
		// Token: 0x040039DA RID: 14810
		Precondition,
		// Token: 0x040039DB RID: 14811
		Postcondition,
		// Token: 0x040039DC RID: 14812
		PostconditionOnException,
		// Token: 0x040039DD RID: 14813
		Invariant,
		// Token: 0x040039DE RID: 14814
		Assert,
		// Token: 0x040039DF RID: 14815
		Assume
	}
}
