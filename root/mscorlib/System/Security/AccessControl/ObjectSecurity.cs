using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	// Token: 0x0200050F RID: 1295
	public abstract class ObjectSecurity
	{
		// Token: 0x06003488 RID: 13448 RVA: 0x000025BE File Offset: 0x000007BE
		protected ObjectSecurity()
		{
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000BFED4 File Offset: 0x000BE0D4
		protected ObjectSecurity(CommonSecurityDescriptor securityDescriptor)
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this.descriptor = securityDescriptor;
			this.rw_lock = new ReaderWriterLock();
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000BFEFC File Offset: 0x000BE0FC
		protected ObjectSecurity(bool isContainer, bool isDS)
			: this(new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, new DiscretionaryAcl(isContainer, isDS, 0)))
		{
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600348B RID: 13451
		public abstract Type AccessRightType { get; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600348C RID: 13452
		public abstract Type AccessRuleType { get; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600348D RID: 13453
		public abstract Type AuditRuleType { get; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000BFF24 File Offset: 0x000BE124
		public bool AreAccessRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isDiscretionaryAclCanonical;
				try
				{
					isDiscretionaryAclCanonical = this.descriptor.IsDiscretionaryAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isDiscretionaryAclCanonical;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000BFF60 File Offset: 0x000BE160
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x000BFFA4 File Offset: 0x000BE1A4
		public bool AreAuditRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isSystemAclCanonical;
				try
				{
					isSystemAclCanonical = this.descriptor.IsSystemAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isSystemAclCanonical;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000BFFE0 File Offset: 0x000BE1E0
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool flag;
				try
				{
					flag = (this.descriptor.ControlFlags & ControlFlags.SystemAclProtected) > ControlFlags.None;
				}
				finally
				{
					this.ReadUnlock();
				}
				return flag;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000C0024 File Offset: 0x000BE224
		// (set) Token: 0x06003493 RID: 13459 RVA: 0x000C0032 File Offset: 0x000BE232
		internal AccessControlSections AccessControlSectionsModified
		{
			get
			{
				this.Reading();
				return this.sections_modified;
			}
			set
			{
				this.Writing();
				this.sections_modified = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000C0041 File Offset: 0x000BE241
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x000C004A File Offset: 0x000BE24A
		protected bool AccessRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Access);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Access, value);
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000C0054 File Offset: 0x000BE254
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x000C005D File Offset: 0x000BE25D
		protected bool AuditRulesModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Audit);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Audit, value);
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000C0067 File Offset: 0x000BE267
		// (set) Token: 0x06003499 RID: 13465 RVA: 0x000C0070 File Offset: 0x000BE270
		protected bool GroupModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Group);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Group, value);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000C007A File Offset: 0x000BE27A
		protected bool IsContainer
		{
			get
			{
				return this.descriptor.IsContainer;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000C0087 File Offset: 0x000BE287
		protected bool IsDS
		{
			get
			{
				return this.descriptor.IsDS;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000C0094 File Offset: 0x000BE294
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000C009D File Offset: 0x000BE29D
		protected bool OwnerModified
		{
			get
			{
				return this.AreAccessControlSectionsModified(AccessControlSections.Owner);
			}
			set
			{
				this.SetAccessControlSectionsModified(AccessControlSections.Owner, value);
			}
		}

		// Token: 0x0600349E RID: 13470
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		// Token: 0x0600349F RID: 13471
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		// Token: 0x060034A0 RID: 13472 RVA: 0x000C00A8 File Offset: 0x000BE2A8
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Group == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000C0100 File Offset: 0x000BE300
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference identityReference;
			try
			{
				if (this.descriptor.Owner == null)
				{
					identityReference = null;
				}
				else
				{
					identityReference = this.descriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return identityReference;
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000C0158 File Offset: 0x000BE358
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			this.ReadLock();
			byte[] array2;
			try
			{
				byte[] array = new byte[this.descriptor.BinaryLength];
				this.descriptor.GetBinaryForm(array, 0);
				array2 = array;
			}
			finally
			{
				this.ReadUnlock();
			}
			return array2;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000C01A8 File Offset: 0x000BE3A8
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			this.ReadLock();
			string sddlForm;
			try
			{
				sddlForm = this.descriptor.GetSddlForm(includeSections);
			}
			finally
			{
				this.ReadUnlock();
			}
			return sddlForm;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000C01E4 File Offset: 0x000BE3E4
		public static bool IsSddlConversionSupported()
		{
			return GenericSecurityDescriptor.IsSddlConversionSupported();
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000C01EB File Offset: 0x000BE3EB
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAccess(modification, rule, out modified);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000C0222 File Offset: 0x000BE422
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException("rule");
			}
			return this.ModifyAudit(modification, rule, out modified);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000C025C File Offset: 0x000BE45C
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAccessControl(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000C02B0 File Offset: 0x000BE4B0
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this.descriptor.PurgeAudit(ObjectSecurity.SidFromIR(identity));
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000C0304 File Offset: 0x000BE504
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000C0340 File Offset: 0x000BE540
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this.descriptor.SetSystemAclProtection(isProtected, preserveInheritance);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000C037C File Offset: 0x000BE57C
		public void SetGroup(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Group = ObjectSecurity.SidFromIR(identity);
				this.GroupModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000C03C0 File Offset: 0x000BE5C0
		public void SetOwner(IdentityReference identity)
		{
			this.WriteLock();
			try
			{
				this.descriptor.Owner = ObjectSecurity.SidFromIR(identity);
				this.OwnerModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000C0404 File Offset: 0x000BE604
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000C040F File Offset: 0x000BE60F
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, binaryForm, 0), includeSections);
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000C042B File Offset: 0x000BE62B
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000C0436 File Offset: 0x000BE636
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			this.CopySddlForm(new CommonSecurityDescriptor(this.IsContainer, this.IsDS, sddlForm), includeSections);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000C0454 File Offset: 0x000BE654
		private void CopySddlForm(CommonSecurityDescriptor sourceDescriptor, AccessControlSections includeSections)
		{
			this.WriteLock();
			try
			{
				this.AccessControlSectionsModified |= includeSections;
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					this.descriptor.SystemAcl = sourceDescriptor.SystemAcl;
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
				{
					this.descriptor.DiscretionaryAcl = sourceDescriptor.DiscretionaryAcl;
				}
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
				{
					this.descriptor.Owner = sourceDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
				{
					this.descriptor.Group = sourceDescriptor.Group;
				}
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x060034B2 RID: 13490
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		// Token: 0x060034B3 RID: 13491
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		// Token: 0x060034B4 RID: 13492 RVA: 0x0004E2CF File Offset: 0x0004C4CF
		private Exception GetNotImplementedException()
		{
			return new NotImplementedException();
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000C04E8 File Offset: 0x000BE6E8
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000C04E8 File Offset: 0x000BE6E8
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw this.GetNotImplementedException();
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000C04F0 File Offset: 0x000BE6F0
		private void Reading()
		{
			if (!this.rw_lock.IsReaderLockHeld && !this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Either a read or a write lock must be held.");
			}
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000C0517 File Offset: 0x000BE717
		protected void ReadLock()
		{
			this.rw_lock.AcquireReaderLock(-1);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000C0525 File Offset: 0x000BE725
		protected void ReadUnlock()
		{
			this.rw_lock.ReleaseReaderLock();
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000C0532 File Offset: 0x000BE732
		private void Writing()
		{
			if (!this.rw_lock.IsWriterLockHeld)
			{
				throw new InvalidOperationException("Write lock must be held.");
			}
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000C054C File Offset: 0x000BE74C
		protected void WriteLock()
		{
			this.rw_lock.AcquireWriterLock(-1);
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000C055A File Offset: 0x000BE75A
		protected void WriteUnlock()
		{
			this.rw_lock.ReleaseWriterLock();
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000C0568 File Offset: 0x000BE768
		internal AuthorizationRuleCollection InternalGetAccessRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				foreach (GenericAce genericAce in this.descriptor.DiscretionaryAcl)
				{
					QualifiedAce qualifiedAce = genericAce as QualifiedAce;
					if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit))
					{
						AccessControlType accessControlType;
						if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
						{
							accessControlType = AccessControlType.Allow;
						}
						else
						{
							if (AceQualifier.AccessDenied != qualifiedAce.AceQualifier)
							{
								continue;
							}
							accessControlType = AccessControlType.Deny;
						}
						AccessRule accessRule = this.InternalAccessRuleFactory(qualifiedAce, targetType, accessControlType);
						list.Add(accessRule);
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000C0618 File Offset: 0x000BE818
		internal virtual AccessRule InternalAccessRuleFactory(QualifiedAce ace, Type targetType, AccessControlType type)
		{
			return this.AccessRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, type);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000C0648 File Offset: 0x000BE848
		internal AuthorizationRuleCollection InternalGetAuditRules(bool includeExplicit, bool includeInherited, Type targetType)
		{
			List<AuthorizationRule> list = new List<AuthorizationRule>();
			this.ReadLock();
			try
			{
				if (this.descriptor.SystemAcl != null)
				{
					foreach (GenericAce genericAce in this.descriptor.SystemAcl)
					{
						QualifiedAce qualifiedAce = genericAce as QualifiedAce;
						if (!(null == qualifiedAce) && (!qualifiedAce.IsInherited || includeInherited) && (qualifiedAce.IsInherited || includeExplicit) && AceQualifier.SystemAudit == qualifiedAce.AceQualifier)
						{
							AuditRule auditRule = this.InternalAuditRuleFactory(qualifiedAce, targetType);
							list.Add(auditRule);
						}
					}
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return new AuthorizationRuleCollection(list.ToArray());
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000C06F4 File Offset: 0x000BE8F4
		internal virtual AuditRule InternalAuditRuleFactory(QualifiedAce ace, Type targetType)
		{
			return this.AuditRuleFactory(ace.SecurityIdentifier.Translate(targetType), ace.AccessMask, ace.IsInherited, ace.InheritanceFlags, ace.PropagationFlags, ace.AuditFlags);
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000C0726 File Offset: 0x000BE926
		internal static SecurityIdentifier SidFromIR(IdentityReference identity)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			return (SecurityIdentifier)identity.Translate(typeof(SecurityIdentifier));
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000C0751 File Offset: 0x000BE951
		private bool AreAccessControlSectionsModified(AccessControlSections mask)
		{
			return (this.AccessControlSectionsModified & mask) > AccessControlSections.None;
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000C075E File Offset: 0x000BE95E
		private void SetAccessControlSectionsModified(AccessControlSections mask, bool modified)
		{
			if (modified)
			{
				this.AccessControlSectionsModified |= mask;
				return;
			}
			this.AccessControlSectionsModified &= ~mask;
		}

		// Token: 0x04002452 RID: 9298
		internal CommonSecurityDescriptor descriptor;

		// Token: 0x04002453 RID: 9299
		private AccessControlSections sections_modified;

		// Token: 0x04002454 RID: 9300
		private ReaderWriterLock rw_lock;
	}
}
