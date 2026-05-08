using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004D1 RID: 1233
	public abstract class AccessRule : AuthorizationRule
	{
		// Token: 0x060032BF RID: 12991 RVA: 0x000BC0AB File Offset: 0x000BA2AB
		protected AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (type < AccessControlType.Allow || type > AccessControlType.Deny)
			{
				throw new ArgumentException("Invalid access control type.", "type");
			}
			this.type = type;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000BC0DC File Offset: 0x000BA2DC
		public AccessControlType AccessControlType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04002393 RID: 9107
		private AccessControlType type;
	}
}
