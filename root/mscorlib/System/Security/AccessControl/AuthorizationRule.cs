using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004DA RID: 1242
	public abstract class AuthorizationRule
	{
		// Token: 0x060032D4 RID: 13012 RVA: 0x000025BE File Offset: 0x000007BE
		internal AuthorizationRule()
		{
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000BC254 File Offset: 0x000BA454
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			if (!(identity is SecurityIdentifier) && !(identity is NTAccount))
			{
				throw new ArgumentException("identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException("accessMask");
			}
			if ((inheritanceFlags & ~(InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit)) != InheritanceFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			if ((propagationFlags & ~(PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly)) != PropagationFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.identity = identity;
			this.accessMask = accessMask;
			this.isInherited = isInherited;
			this.inheritanceFlags = inheritanceFlags;
			this.propagationFlags = propagationFlags;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000BC2E3 File Offset: 0x000BA4E3
		public IdentityReference IdentityReference
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000BC2EB File Offset: 0x000BA4EB
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this.inheritanceFlags;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000BC2F3 File Offset: 0x000BA4F3
		public bool IsInherited
		{
			get
			{
				return this.isInherited;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000BC2FB File Offset: 0x000BA4FB
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this.propagationFlags;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000BC303 File Offset: 0x000BA503
		protected internal int AccessMask
		{
			get
			{
				return this.accessMask;
			}
		}

		// Token: 0x040023BE RID: 9150
		private IdentityReference identity;

		// Token: 0x040023BF RID: 9151
		private int accessMask;

		// Token: 0x040023C0 RID: 9152
		private bool isInherited;

		// Token: 0x040023C1 RID: 9153
		private InheritanceFlags inheritanceFlags;

		// Token: 0x040023C2 RID: 9154
		private PropagationFlags propagationFlags;
	}
}
