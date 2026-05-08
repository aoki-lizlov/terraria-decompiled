using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000501 RID: 1281
	[Flags]
	public enum MutexRights
	{
		// Token: 0x04002436 RID: 9270
		Modify = 1,
		// Token: 0x04002437 RID: 9271
		Delete = 65536,
		// Token: 0x04002438 RID: 9272
		ReadPermissions = 131072,
		// Token: 0x04002439 RID: 9273
		ChangePermissions = 262144,
		// Token: 0x0400243A RID: 9274
		TakeOwnership = 524288,
		// Token: 0x0400243B RID: 9275
		Synchronize = 1048576,
		// Token: 0x0400243C RID: 9276
		FullControl = 2031617
	}
}
