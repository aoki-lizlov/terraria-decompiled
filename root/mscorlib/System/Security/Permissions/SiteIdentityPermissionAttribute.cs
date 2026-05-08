using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200042B RID: 1067
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002CF5 RID: 11509 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public SiteIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x000A25AC File Offset: 0x000A07AC
		// (set) Token: 0x06002CF7 RID: 11511 RVA: 0x000A25B4 File Offset: 0x000A07B4
		public string Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000A25C0 File Offset: 0x000A07C0
		public override IPermission CreatePermission()
		{
			SiteIdentityPermission siteIdentityPermission;
			if (base.Unrestricted)
			{
				siteIdentityPermission = new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			else if (this.site == null)
			{
				siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			}
			else
			{
				siteIdentityPermission = new SiteIdentityPermission(this.site);
			}
			return siteIdentityPermission;
		}

		// Token: 0x04001F77 RID: 8055
		private string site;
	}
}
