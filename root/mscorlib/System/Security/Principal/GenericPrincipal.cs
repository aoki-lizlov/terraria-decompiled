using System;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace System.Security.Principal
{
	// Token: 0x020004B3 RID: 1203
	[ComVisible(true)]
	[Serializable]
	public class GenericPrincipal : ClaimsPrincipal
	{
		// Token: 0x06003193 RID: 12691 RVA: 0x000B75F4 File Offset: 0x000B57F4
		public GenericPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identity = identity;
			if (roles != null)
			{
				this.m_roles = new string[roles.Length];
				for (int i = 0; i < roles.Length; i++)
				{
					this.m_roles[i] = roles[i];
				}
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000B7646 File Offset: 0x000B5846
		internal string[] Roles
		{
			get
			{
				return this.m_roles;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x000B764E File Offset: 0x000B584E
		public override IIdentity Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000B7658 File Offset: 0x000B5858
		public override bool IsInRole(string role)
		{
			if (this.m_roles == null)
			{
				return false;
			}
			int length = role.Length;
			foreach (string text in this.m_roles)
			{
				if (text != null && length == text.Length && string.Compare(role, 0, text, 0, length, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002242 RID: 8770
		private IIdentity m_identity;

		// Token: 0x04002243 RID: 8771
		private string[] m_roles;
	}
}
