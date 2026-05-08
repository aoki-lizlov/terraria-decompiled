using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F9 RID: 1273
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x060033C7 RID: 13255 RVA: 0x000BE43B File Offset: 0x000BC63B
		internal FileSystemSecurity(bool isContainer)
			: base(isContainer, ResourceType.FileObject)
		{
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000BE445 File Offset: 0x000BC645
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections)
			: base(isContainer, ResourceType.FileObject, name, includeSections)
		{
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000BE451 File Offset: 0x000BC651
		internal FileSystemSecurity(bool isContainer, SafeHandle handle, AccessControlSections includeSections)
			: base(isContainer, ResourceType.FileObject, handle, includeSections)
		{
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000BE45D File Offset: 0x000BC65D
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000BE469 File Offset: 0x000BC669
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x000BE475 File Offset: 0x000BC675
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000BE481 File Offset: 0x000BC681
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000BE491 File Offset: 0x000BC691
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, (FileSystemRights)accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
