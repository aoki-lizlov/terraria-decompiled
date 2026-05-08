using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000433 RID: 1075
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002D4B RID: 11595 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public UrlIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000A3A04 File Offset: 0x000A1C04
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000A3A0C File Offset: 0x000A1C0C
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000A3A15 File Offset: 0x000A1C15
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		// Token: 0x04001F8A RID: 8074
		private string url;
	}
}
