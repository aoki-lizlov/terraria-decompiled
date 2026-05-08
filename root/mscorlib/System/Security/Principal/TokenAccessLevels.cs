using System;

namespace System.Security.Principal
{
	// Token: 0x020004AE RID: 1198
	[Flags]
	public enum TokenAccessLevels
	{
		// Token: 0x0400222B RID: 8747
		AssignPrimary = 1,
		// Token: 0x0400222C RID: 8748
		Duplicate = 2,
		// Token: 0x0400222D RID: 8749
		Impersonate = 4,
		// Token: 0x0400222E RID: 8750
		Query = 8,
		// Token: 0x0400222F RID: 8751
		QuerySource = 16,
		// Token: 0x04002230 RID: 8752
		AdjustPrivileges = 32,
		// Token: 0x04002231 RID: 8753
		AdjustGroups = 64,
		// Token: 0x04002232 RID: 8754
		AdjustDefault = 128,
		// Token: 0x04002233 RID: 8755
		AdjustSessionId = 256,
		// Token: 0x04002234 RID: 8756
		Read = 131080,
		// Token: 0x04002235 RID: 8757
		Write = 131296,
		// Token: 0x04002236 RID: 8758
		AllAccess = 983551,
		// Token: 0x04002237 RID: 8759
		MaximumAllowed = 33554432
	}
}
