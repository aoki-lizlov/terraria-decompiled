using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004EE RID: 1262
	public abstract class DirectoryObjectSecurity : ObjectSecurity
	{
		// Token: 0x06003375 RID: 13173 RVA: 0x000BDA84 File Offset: 0x000BBC84
		protected DirectoryObjectSecurity()
			: base(true, true)
		{
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000BD134 File Offset: 0x000BB334
		protected DirectoryObjectSecurity(CommonSecurityDescriptor securityDescriptor)
			: base(securityDescriptor)
		{
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0004E2CF File Offset: 0x0004C4CF
		private Exception GetNotImplementedException()
		{
			return new NotImplementedException();
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000BDA8E File Offset: 0x000BBC8E
		public virtual AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type, Guid objectType, Guid inheritedObjectType)
		{
			throw this.GetNotImplementedException();
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000BDA98 File Offset: 0x000BBC98
		internal override AccessRule InternalAccessRuleFactory(QualifiedAce ace, Type targetType, AccessControlType type)
		{
			ObjectAce objectAce = ace as ObjectAce;
			if (null == objectAce || objectAce.ObjectAceFlags == ObjectAceFlags.None)
			{
				return base.InternalAccessRuleFactory(ace, targetType, type);
			}
			return this.AccessRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type, objectAce.ObjectAceType, objectAce.InheritedObjectAceType);
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000BDA8E File Offset: 0x000BBC8E
		public virtual AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags, Guid objectType, Guid inheritedObjectType)
		{
			throw this.GetNotImplementedException();
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000BDB00 File Offset: 0x000BBD00
		internal override AuditRule InternalAuditRuleFactory(QualifiedAce ace, Type targetType)
		{
			ObjectAce objectAce = ace as ObjectAce;
			if (null == objectAce || objectAce.ObjectAceFlags == ObjectAceFlags.None)
			{
				return base.InternalAuditRuleFactory(ace, targetType);
			}
			return this.AuditRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType);
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000BD13D File Offset: 0x000BB33D
		public AuthorizationRuleCollection GetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAccessRules(includeExplicit, includeInherited, targetType);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000BD148 File Offset: 0x000BB348
		public AuthorizationRuleCollection GetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			return base.InternalGetAuditRules(includeExplicit, includeInherited, targetType);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000BDB6C File Offset: 0x000BBD6C
		protected void AddAccessRule(ObjectAccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Add, rule, out flag);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000BDB84 File Offset: 0x000BBD84
		protected bool RemoveAccessRule(ObjectAccessRule rule)
		{
			bool flag;
			return this.ModifyAccess(AccessControlModification.Remove, rule, out flag);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000BDB9C File Offset: 0x000BBD9C
		protected void RemoveAccessRuleAll(ObjectAccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveAll, rule, out flag);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000BDBB4 File Offset: 0x000BBDB4
		protected void RemoveAccessRuleSpecific(ObjectAccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000BDBCC File Offset: 0x000BBDCC
		protected void ResetAccessRule(ObjectAccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Reset, rule, out flag);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000BDBE4 File Offset: 0x000BBDE4
		protected void SetAccessRule(ObjectAccessRule rule)
		{
			bool flag;
			this.ModifyAccess(AccessControlModification.Set, rule, out flag);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000BDBFC File Offset: 0x000BBDFC
		protected override bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			ObjectAccessRule objectAccessRule = rule as ObjectAccessRule;
			if (objectAccessRule == null)
			{
				throw new ArgumentException("rule");
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
					this.descriptor.DiscretionaryAcl.SetAccess(objectAccessRule.AccessControlType, ObjectSecurity.SidFromIR(objectAccessRule.IdentityReference), objectAccessRule.AccessMask, objectAccessRule.InheritanceFlags, objectAccessRule.PropagationFlags, objectAccessRule.ObjectFlags, objectAccessRule.ObjectType, objectAccessRule.InheritedObjectType);
					goto IL_019D;
				case AccessControlModification.Reset:
					this.PurgeAccessRules(objectAccessRule.IdentityReference);
					break;
				case AccessControlModification.Remove:
					modified = this.descriptor.DiscretionaryAcl.RemoveAccess(objectAccessRule.AccessControlType, ObjectSecurity.SidFromIR(objectAccessRule.IdentityReference), rule.AccessMask, objectAccessRule.InheritanceFlags, objectAccessRule.PropagationFlags, objectAccessRule.ObjectFlags, objectAccessRule.ObjectType, objectAccessRule.InheritedObjectType);
					goto IL_019D;
				case AccessControlModification.RemoveAll:
					this.PurgeAccessRules(objectAccessRule.IdentityReference);
					goto IL_019D;
				case AccessControlModification.RemoveSpecific:
					this.descriptor.DiscretionaryAcl.RemoveAccessSpecific(objectAccessRule.AccessControlType, ObjectSecurity.SidFromIR(objectAccessRule.IdentityReference), objectAccessRule.AccessMask, objectAccessRule.InheritanceFlags, objectAccessRule.PropagationFlags, objectAccessRule.ObjectFlags, objectAccessRule.ObjectType, objectAccessRule.InheritedObjectType);
					goto IL_019D;
				default:
					throw new ArgumentOutOfRangeException("modification");
				}
				this.descriptor.DiscretionaryAcl.AddAccess(objectAccessRule.AccessControlType, ObjectSecurity.SidFromIR(objectAccessRule.IdentityReference), objectAccessRule.AccessMask, objectAccessRule.InheritanceFlags, objectAccessRule.PropagationFlags, objectAccessRule.ObjectFlags, objectAccessRule.ObjectType, objectAccessRule.InheritedObjectType);
				IL_019D:
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

		// Token: 0x06003385 RID: 13189 RVA: 0x000BDDD8 File Offset: 0x000BBFD8
		protected void AddAuditRule(ObjectAuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Add, rule, out flag);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000BDDF0 File Offset: 0x000BBFF0
		protected bool RemoveAuditRule(ObjectAuditRule rule)
		{
			bool flag;
			return this.ModifyAudit(AccessControlModification.Remove, rule, out flag);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000BDE08 File Offset: 0x000BC008
		protected void RemoveAuditRuleAll(ObjectAuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveAll, rule, out flag);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000BDE20 File Offset: 0x000BC020
		protected void RemoveAuditRuleSpecific(ObjectAuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.RemoveSpecific, rule, out flag);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000BDE38 File Offset: 0x000BC038
		protected void SetAuditRule(ObjectAuditRule rule)
		{
			bool flag;
			this.ModifyAudit(AccessControlModification.Set, rule, out flag);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000BDE50 File Offset: 0x000BC050
		protected override bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			ObjectAuditRule objectAuditRule = rule as ObjectAuditRule;
			if (objectAuditRule == null)
			{
				throw new ArgumentException("rule");
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
					this.descriptor.SystemAcl.AddAudit(objectAuditRule.AuditFlags, ObjectSecurity.SidFromIR(objectAuditRule.IdentityReference), objectAuditRule.AccessMask, objectAuditRule.InheritanceFlags, objectAuditRule.PropagationFlags, objectAuditRule.ObjectFlags, objectAuditRule.ObjectType, objectAuditRule.InheritedObjectType);
					break;
				case AccessControlModification.Set:
					if (this.descriptor.SystemAcl == null)
					{
						this.descriptor.SystemAcl = new SystemAcl(base.IsContainer, base.IsDS, 1);
					}
					this.descriptor.SystemAcl.SetAudit(objectAuditRule.AuditFlags, ObjectSecurity.SidFromIR(objectAuditRule.IdentityReference), objectAuditRule.AccessMask, objectAuditRule.InheritanceFlags, objectAuditRule.PropagationFlags, objectAuditRule.ObjectFlags, objectAuditRule.ObjectType, objectAuditRule.InheritedObjectType);
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
						modified = this.descriptor.SystemAcl.RemoveAudit(objectAuditRule.AuditFlags, ObjectSecurity.SidFromIR(objectAuditRule.IdentityReference), objectAuditRule.AccessMask, objectAuditRule.InheritanceFlags, objectAuditRule.PropagationFlags, objectAuditRule.ObjectFlags, objectAuditRule.ObjectType, objectAuditRule.InheritedObjectType);
					}
					break;
				case AccessControlModification.RemoveAll:
					this.PurgeAuditRules(objectAuditRule.IdentityReference);
					break;
				case AccessControlModification.RemoveSpecific:
					if (this.descriptor.SystemAcl != null)
					{
						this.descriptor.SystemAcl.RemoveAuditSpecific(objectAuditRule.AuditFlags, ObjectSecurity.SidFromIR(objectAuditRule.IdentityReference), objectAuditRule.AccessMask, objectAuditRule.InheritanceFlags, objectAuditRule.PropagationFlags, objectAuditRule.ObjectFlags, objectAuditRule.ObjectType, objectAuditRule.InheritedObjectType);
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
