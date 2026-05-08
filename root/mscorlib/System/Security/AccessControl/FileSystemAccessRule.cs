using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F6 RID: 1270
	public sealed class FileSystemAccessRule : AccessRule
	{
		// Token: 0x060033BB RID: 13243 RVA: 0x000BE3B7 File Offset: 0x000BC5B7
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, AccessControlType type)
			: this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000BE3C4 File Offset: 0x000BC5C4
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, AccessControlType type)
			: this(new NTAccount(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000BE3D6 File Offset: 0x000BC5D6
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(identity, fileSystemRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000BC12F File Offset: 0x000BA32F
		internal FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, (int)fileSystemRights, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000BE3E6 File Offset: 0x000BC5E6
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(new NTAccount(identity), fileSystemRights, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000BD962 File Offset: 0x000BBB62
		public FileSystemRights FileSystemRights
		{
			get
			{
				return (FileSystemRights)base.AccessMask;
			}
		}
	}
}
