using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004D4 RID: 1236
	[Flags]
	public enum AceFlags : byte
	{
		// Token: 0x04002397 RID: 9111
		None = 0,
		// Token: 0x04002398 RID: 9112
		ObjectInherit = 1,
		// Token: 0x04002399 RID: 9113
		ContainerInherit = 2,
		// Token: 0x0400239A RID: 9114
		NoPropagateInherit = 4,
		// Token: 0x0400239B RID: 9115
		InheritOnly = 8,
		// Token: 0x0400239C RID: 9116
		InheritanceFlags = 15,
		// Token: 0x0400239D RID: 9117
		Inherited = 16,
		// Token: 0x0400239E RID: 9118
		SuccessfulAccess = 64,
		// Token: 0x0400239F RID: 9119
		FailedAccess = 128,
		// Token: 0x040023A0 RID: 9120
		AuditFlags = 192
	}
}
