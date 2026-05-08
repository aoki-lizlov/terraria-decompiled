using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000517 RID: 1303
	[Flags]
	public enum RegistryRights
	{
		// Token: 0x04002463 RID: 9315
		QueryValues = 1,
		// Token: 0x04002464 RID: 9316
		SetValue = 2,
		// Token: 0x04002465 RID: 9317
		CreateSubKey = 4,
		// Token: 0x04002466 RID: 9318
		EnumerateSubKeys = 8,
		// Token: 0x04002467 RID: 9319
		Notify = 16,
		// Token: 0x04002468 RID: 9320
		CreateLink = 32,
		// Token: 0x04002469 RID: 9321
		Delete = 65536,
		// Token: 0x0400246A RID: 9322
		ReadPermissions = 131072,
		// Token: 0x0400246B RID: 9323
		WriteKey = 131078,
		// Token: 0x0400246C RID: 9324
		ReadKey = 131097,
		// Token: 0x0400246D RID: 9325
		ExecuteKey = 131097,
		// Token: 0x0400246E RID: 9326
		ChangePermissions = 262144,
		// Token: 0x0400246F RID: 9327
		TakeOwnership = 524288,
		// Token: 0x04002470 RID: 9328
		FullControl = 983103
	}
}
