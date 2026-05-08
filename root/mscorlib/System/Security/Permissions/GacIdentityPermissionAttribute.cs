using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200040C RID: 1036
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002BB0 RID: 11184 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public GacIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0009A19E File Offset: 0x0009839E
		public override IPermission CreatePermission()
		{
			return new GacIdentityPermission();
		}
	}
}
