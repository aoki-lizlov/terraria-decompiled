using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000516 RID: 1302
	public sealed class RegistryAuditRule : AuditRule
	{
		// Token: 0x0600350E RID: 13582 RVA: 0x000C11EE File Offset: 0x000BF3EE
		public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000BC241 File Offset: 0x000BA441
		internal RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, (int)registryRights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000C11FE File Offset: 0x000BF3FE
		public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), registryRights, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000BD962 File Offset: 0x000BBB62
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
