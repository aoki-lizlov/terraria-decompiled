using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000404 RID: 1028
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum EnvironmentPermissionAccess
	{
		// Token: 0x04001ED4 RID: 7892
		NoAccess = 0,
		// Token: 0x04001ED5 RID: 7893
		Read = 1,
		// Token: 0x04001ED6 RID: 7894
		Write = 2,
		// Token: 0x04001ED7 RID: 7895
		AllAccess = 3
	}
}
