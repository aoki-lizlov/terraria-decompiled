using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F4 RID: 1268
	public sealed class EventWaitHandleSecurity : NativeObjectSecurity
	{
		// Token: 0x060033A5 RID: 13221 RVA: 0x000BE33F File Offset: 0x000BC53F
		public EventWaitHandleSecurity()
			: base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000BE349 File Offset: 0x000BC549
		internal EventWaitHandleSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections)
		{
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000BE355 File Offset: 0x000BC555
		public override Type AccessRightType
		{
			get
			{
				return typeof(EventWaitHandleRights);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000BE361 File Offset: 0x000BC561
		public override Type AccessRuleType
		{
			get
			{
				return typeof(EventWaitHandleAccessRule);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000BE36D File Offset: 0x000BC56D
		public override Type AuditRuleType
		{
			get
			{
				return typeof(EventWaitHandleAuditRule);
			}
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000BE379 File Offset: 0x000BC579
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new EventWaitHandleAccessRule(identityReference, (EventWaitHandleRights)accessMask, type);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public void AddAccessRule(EventWaitHandleAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public void ResetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000BE384 File Offset: 0x000BC584
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new EventWaitHandleAuditRule(identityReference, (EventWaitHandleRights)accessMask, flags);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public void AddAuditRule(EventWaitHandleAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public void SetAuditRule(EventWaitHandleAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000BE38F File Offset: 0x000BC58F
		internal void Persist(SafeHandle handle)
		{
			base.PersistModifications(handle);
		}
	}
}
