using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004D8 RID: 1240
	public abstract class AuditRule : AuthorizationRule
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x000BC1BF File Offset: 0x000BA3BF
		protected AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags)
		{
			if (auditFlags != ((AuditFlags.Success | AuditFlags.Failure) & auditFlags))
			{
				throw new ArgumentException("Invalid audit flags.", "auditFlags");
			}
			this.auditFlags = auditFlags;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000BC1EE File Offset: 0x000BA3EE
		public AuditFlags AuditFlags
		{
			get
			{
				return this.auditFlags;
			}
		}

		// Token: 0x040023BD RID: 9149
		private AuditFlags auditFlags;
	}
}
