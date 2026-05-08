using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004CF RID: 1231
	[Flags]
	public enum AccessControlSections
	{
		// Token: 0x0400238A RID: 9098
		None = 0,
		// Token: 0x0400238B RID: 9099
		Audit = 1,
		// Token: 0x0400238C RID: 9100
		Access = 2,
		// Token: 0x0400238D RID: 9101
		Owner = 4,
		// Token: 0x0400238E RID: 9102
		Group = 8,
		// Token: 0x0400238F RID: 9103
		All = 15
	}
}
