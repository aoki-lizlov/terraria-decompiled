using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000510 RID: 1296
	public abstract class ObjectSecurity<T> : NativeObjectSecurity where T : struct
	{
		// Token: 0x060034C5 RID: 13509 RVA: 0x000C0781 File Offset: 0x000BE981
		protected ObjectSecurity(bool isContainer, ResourceType resourceType)
			: base(isContainer, resourceType)
		{
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000C078B File Offset: 0x000BE98B
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections)
			: base(isContainer, resourceType, safeHandle, includeSections)
		{
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000C0798 File Offset: 0x000BE998
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: base(isContainer, resourceType, name, includeSections)
		{
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x000C07A5 File Offset: 0x000BE9A5
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, safeHandle, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000C07B6 File Offset: 0x000BE9B6
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, name, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000C07C7 File Offset: 0x000BE9C7
		public override Type AccessRightType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000C07D3 File Offset: 0x000BE9D3
		public override Type AccessRuleType
		{
			get
			{
				return typeof(AccessRule<T>);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000C07DF File Offset: 0x000BE9DF
		public override Type AuditRuleType
		{
			get
			{
				return typeof(AuditRule<T>);
			}
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000C07EB File Offset: 0x000BE9EB
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new AccessRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public virtual void AddAccessRule(AccessRule<T> rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public virtual bool RemoveAccessRule(AccessRule<T> rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public virtual void RemoveAccessRuleAll(AccessRule<T> rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public virtual void RemoveAccessRuleSpecific(AccessRule<T> rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public virtual void ResetAccessRule(AccessRule<T> rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public virtual void SetAccessRule(AccessRule<T> rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000C07FB File Offset: 0x000BE9FB
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new AuditRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public virtual void AddAuditRule(AuditRule<T> rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public virtual bool RemoveAuditRule(AuditRule<T> rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public virtual void RemoveAuditRuleAll(AuditRule<T> rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public virtual void RemoveAuditRuleSpecific(AuditRule<T> rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public virtual void SetAuditRule(AuditRule<T> rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000C080C File Offset: 0x000BEA0C
		protected void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000C0848 File Offset: 0x000BEA48
		protected void Persist(string name)
		{
			base.WriteLock();
			try
			{
				this.Persist(name, base.AccessControlSectionsModified);
			}
			finally
			{
				base.WriteUnlock();
			}
		}
	}
}
