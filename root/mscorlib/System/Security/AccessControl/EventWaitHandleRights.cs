using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004F3 RID: 1267
	[Flags]
	public enum EventWaitHandleRights
	{
		// Token: 0x0400240B RID: 9227
		Modify = 2,
		// Token: 0x0400240C RID: 9228
		Delete = 65536,
		// Token: 0x0400240D RID: 9229
		ReadPermissions = 131072,
		// Token: 0x0400240E RID: 9230
		ChangePermissions = 262144,
		// Token: 0x0400240F RID: 9231
		TakeOwnership = 524288,
		// Token: 0x04002410 RID: 9232
		Synchronize = 1048576,
		// Token: 0x04002411 RID: 9233
		FullControl = 2031619
	}
}
