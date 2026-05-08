using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004E4 RID: 1252
	public abstract class CommonObjectSecurity : ObjectSecurity
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x000BD12A File Offset: 0x000BB32A
		protected CommonObjectSecurity(bool isContainer)
			: base(isContainer, false)
		{
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000BD134 File Offset: 0x000BB334
		internal CommonObjectSecurity(CommonSecurityDescriptor securityDescriptor)
			: base(securityDescriptor)
		{
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000BD13D File Offset: 0x000BB33D
		public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAccessRules(includeExplicit, includeInherited, targetType);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000BD148 File Offset: 0x000BB348
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAuditRules(includeExplicit, includeInherited, targetType);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000BD154 File Offset: 0x000BB354
		protected void AddAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Add, rule, out flag);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000BD16C File Offset: 0x000BB36C
		protected bool RemoveAccessRule(AccessRule rule)
		{
			bool flag;
			return this.ModifyAccess(AccessControlModification.Remove, rule, out flag);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000BD184 File Offset: 0x000BB384
		protected void RemoveAccessRuleAll(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveAll, rule, out flag);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000BD19C File Offset: 0x000BB39C
		protected void RemoveAccessRuleSpecific(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000BD1B4 File Offset: 0x000BB3B4
		protected void ResetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Reset, rule, out flag);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000BD1CC File Offset: 0x000BB3CC
		protected void SetAccessRule(AccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Set, rule, out flag);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000BD1E4 File Offset: 0x000BB3E4
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					break;
				case AccessControlModification.Set:
					this.descriptor.DiscretionaryAcl.SetAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				case AccessControlModification.Reset:
					this.PurgeAccessRules(rule.IdentityReference);
					break;
				case AccessControlModification.Remove:
					modified = this.descriptor.DiscretionaryAcl.RemoveAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				case AccessControlModification.RemoveAll:
					this.PurgeAccessRules(rule.IdentityReference);
					goto IL_013D;
				case AccessControlModification.RemoveSpecific:
					this.descriptor.DiscretionaryAcl.RemoveAccessSpecific(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					goto IL_013D;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				this.descriptor.DiscretionaryAcl.AddAccess(rule.AccessControlType, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
				IL_013D:
				if (modified)
				{
					base.AccessRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000BD360 File Offset: 0x000BB560
		protected void AddAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Add, rule, out flag);
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000BD378 File Offset: 0x000BB578
		protected bool RemoveAuditRule(AuditRule rule)
		{
			bool flag;
			return this.ModifyAudit(AccessControlModification.Remove, rule, out flag);
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000BD390 File Offset: 0x000BB590
		protected void RemoveAuditRuleAll(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveAll, rule, out flag);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000BD3A8 File Offset: 0x000BB5A8
		protected void RemoveAuditRuleSpecific(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000BD3C0 File Offset: 0x000BB5C0
		protected void SetAuditRule(AuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Set, rule, out flag);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000BD3D8 File Offset: 0x000BB5D8
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			modified = true;
			base.WriteLock();
			try
			{
				switch (modification)
				{
				case AccessControlModification.Add:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.AddAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Set:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.SetAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					break;
				case AccessControlModification.Reset:
					break;
				case AccessControlModification.Remove:
					if (this.descriptor.SystemAcl == null)
					{
						modified = false;
					}
					else
					{
						modified = this.descriptor.SystemAcl.RemoveAudit(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				case AccessControlModification.RemoveAll:
					this.PurgeAuditRules(rule.IdentityReference);
					break;
				case AccessControlModification.RemoveSpecific:
					if (this.descriptor.SystemAcl != null)
					{
						this.descriptor.SystemAcl.RemoveAuditSpecific(rule.AuditFlags, ObjectSecurity.SidFromIR(rule.IdentityReference), rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				if (modified)
				{
					base.AuditRulesModified = true;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
			return modified;
		}
	}
}
