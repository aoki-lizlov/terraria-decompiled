using System;

namespace System.Security.Principal
{
	// Token: 0x020004B0 RID: 1200
	public interface IPrincipal
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06003191 RID: 12689
		IIdentity Identity { get; }

		// Token: 0x06003192 RID: 12690
		bool IsInRole(string role);
	}
}
