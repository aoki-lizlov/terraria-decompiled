using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F7 RID: 1271
	public sealed class FileSystemAuditRule : AuditRule
	{
		// Token: 0x060033C1 RID: 13249 RVA: 0x000BE3FA File Offset: 0x000BC5FA
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags)
			: this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000BE407 File Offset: 0x000BC607
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags)
			: this(new NTAccount(identity), fileSystemRights, flags)
		{
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000BE417 File Offset: 0x000BC617
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, fileSystemRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000BC241 File Offset: 0x000BA441
		internal FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, (int)fileSystemRights, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000BE427 File Offset: 0x000BC627
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), fileSystemRights, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000BD962 File Offset: 0x000BBB62
		public FileSystemRights FileSystemRights
		{
			get
			{
				return (FileSystemRights)base.AccessMask;
			}
		}
	}
}
