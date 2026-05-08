using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004D2 RID: 1234
	public class AccessRule<T> : AccessRule where T : struct
	{
		// Token: 0x060032C1 RID: 12993 RVA: 0x000BC0E4 File Offset: 0x000BA2E4
		public AccessRule(string identity, T rights, AccessControlType type)
			: this(new NTAccount(identity), rights, type)
		{
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000BC0F4 File Offset: 0x000BA2F4
		public AccessRule(IdentityReference identity, T rights, AccessControlType type)
			: this(identity, rights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000BC101 File Offset: 0x000BA301
		public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(new NTAccount(identity), rights, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000BC115 File Offset: 0x000BA315
		public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000BC12F File Offset: 0x000BA32F
		internal AccessRule(IdentityReference identity, int rights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, rights, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000BC140 File Offset: 0x000BA340
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
