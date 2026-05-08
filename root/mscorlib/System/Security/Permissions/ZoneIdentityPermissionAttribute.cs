using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000435 RID: 1077
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002D5B RID: 11611 RVA: 0x000A3C47 File Offset: 0x000A1E47
		public ZoneIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.zone = SecurityZone.NoZone;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000A3C57 File Offset: 0x000A1E57
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000A3C5F File Offset: 0x000A1E5F
		public SecurityZone Zone
		{
			get
			{
				return this.zone;
			}
			set
			{
				this.zone = value;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000A3C68 File Offset: 0x000A1E68
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x04001F8D RID: 8077
		private SecurityZone zone;
	}
}
