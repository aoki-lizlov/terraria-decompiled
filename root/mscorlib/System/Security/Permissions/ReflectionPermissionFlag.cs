using System;

namespace System.Security.Permissions
{
	// Token: 0x020003FE RID: 1022
	[Flags]
	public enum ReflectionPermissionFlag
	{
		// Token: 0x04001EBA RID: 7866
		[Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")]
		AllFlags = 7,
		// Token: 0x04001EBB RID: 7867
		MemberAccess = 2,
		// Token: 0x04001EBC RID: 7868
		NoFlags = 0,
		// Token: 0x04001EBD RID: 7869
		[Obsolete("This permission is no longer used by the CLR.")]
		ReflectionEmit = 4,
		// Token: 0x04001EBE RID: 7870
		RestrictedMemberAccess = 8,
		// Token: 0x04001EBF RID: 7871
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		TypeInformation = 1
	}
}
