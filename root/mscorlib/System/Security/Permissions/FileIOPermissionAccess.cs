using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000409 RID: 1033
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum FileIOPermissionAccess
	{
		// Token: 0x04001EE9 RID: 7913
		NoAccess = 0,
		// Token: 0x04001EEA RID: 7914
		Read = 1,
		// Token: 0x04001EEB RID: 7915
		Write = 2,
		// Token: 0x04001EEC RID: 7916
		Append = 4,
		// Token: 0x04001EED RID: 7917
		PathDiscovery = 8,
		// Token: 0x04001EEE RID: 7918
		AllAccess = 15
	}
}
