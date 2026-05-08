using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200041E RID: 1054
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002C56 RID: 11350 RVA: 0x000A0A09 File Offset: 0x0009EC09
		public PrincipalPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.authenticated = true;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000A0A19 File Offset: 0x0009EC19
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x000A0A21 File Offset: 0x0009EC21
		public bool Authenticated
		{
			get
			{
				return this.authenticated;
			}
			set
			{
				this.authenticated = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000A0A2A File Offset: 0x0009EC2A
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x000A0A32 File Offset: 0x0009EC32
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000A0A3B File Offset: 0x0009EC3B
		// (set) Token: 0x06002C5C RID: 11356 RVA: 0x000A0A43 File Offset: 0x0009EC43
		public string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000A0A4C File Offset: 0x0009EC4C
		public override IPermission CreatePermission()
		{
			PrincipalPermission principalPermission;
			if (base.Unrestricted)
			{
				principalPermission = new PrincipalPermission(PermissionState.Unrestricted);
			}
			else
			{
				principalPermission = new PrincipalPermission(this.name, this.role, this.authenticated);
			}
			return principalPermission;
		}

		// Token: 0x04001F3C RID: 7996
		private bool authenticated;

		// Token: 0x04001F3D RID: 7997
		private string name;

		// Token: 0x04001F3E RID: 7998
		private string role;
	}
}
