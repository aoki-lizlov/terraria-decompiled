using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004D9 RID: 1241
	public class AuditRule<T> : AuditRule where T : struct
	{
		// Token: 0x060032CE RID: 13006 RVA: 0x000BC1F6 File Offset: 0x000BA3F6
		public AuditRule(string identity, T rights, AuditFlags flags)
			: this(new NTAccount(identity), rights, flags)
		{
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000BC206 File Offset: 0x000BA406
		public AuditRule(IdentityReference identity, T rights, AuditFlags flags)
			: this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000BC213 File Offset: 0x000BA413
		public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), rights, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000BC227 File Offset: 0x000BA427
		public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000BC241 File Offset: 0x000BA441
		internal AuditRule(IdentityReference identity, int rights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, rights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000BC140 File Offset: 0x000BA340
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
