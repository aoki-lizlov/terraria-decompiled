using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000515 RID: 1301
	public sealed class RegistryAccessRule : AccessRule
	{
		// Token: 0x06003508 RID: 13576 RVA: 0x000C11AD File Offset: 0x000BF3AD
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type)
			: this(identity, registryRights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000C11BA File Offset: 0x000BF3BA
		public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type)
			: this(new NTAccount(identity), registryRights, type)
		{
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000C11CA File Offset: 0x000BF3CA
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(identity, registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000BC12F File Offset: 0x000BA32F
		internal RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, (int)registryRights, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000C11DA File Offset: 0x000BF3DA
		public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(new NTAccount(identity), registryRights, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000BD962 File Offset: 0x000BBB62
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
