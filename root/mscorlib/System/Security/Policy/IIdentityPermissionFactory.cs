using System;

namespace System.Security.Policy
{
	// Token: 0x020003C9 RID: 969
	public interface IIdentityPermissionFactory
	{
		// Token: 0x0600295B RID: 10587
		IPermission CreateIdentityPermission(Evidence evidence);
	}
}
