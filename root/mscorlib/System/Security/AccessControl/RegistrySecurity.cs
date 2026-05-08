using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000518 RID: 1304
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		// Token: 0x06003512 RID: 13586 RVA: 0x000C1212 File Offset: 0x000BF412
		public RegistrySecurity()
			: base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000C121C File Offset: 0x000BF41C
		internal RegistrySecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.RegistryKey, name, includeSections)
		{
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000C1228 File Offset: 0x000BF428
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000C1234 File Offset: 0x000BF434
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000C1240 File Offset: 0x000BF440
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000C124C File Offset: 0x000BF44C
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, (RegistryRights)accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000C125C File Offset: 0x000BF45C
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, (RegistryRights)accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
