using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	// Token: 0x02000502 RID: 1282
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		// Token: 0x06003427 RID: 13351 RVA: 0x000BE33F File Offset: 0x000BC53F
		public MutexSecurity()
			: base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000BF2A6 File Offset: 0x000BD4A6
		public MutexSecurity(string name, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity.MutexExceptionFromErrorCode), null)
		{
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000BF2BF File Offset: 0x000BD4BF
		internal MutexSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity.MutexExceptionFromErrorCode), null)
		{
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000BF2D8 File Offset: 0x000BD4D8
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000BF2E4 File Offset: 0x000BD4E4
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000BF2F0 File Offset: 0x000BD4F0
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000BF2FC File Offset: 0x000BD4FC
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, (MutexRights)accessMask, type);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000BF307 File Offset: 0x000BD507
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, (MutexRights)accessMask, flags);
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000BF312 File Offset: 0x000BD512
		private static Exception MutexExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 2)
			{
				return new WaitHandleCannotBeOpenedException();
			}
			return NativeObjectSecurity.DefaultExceptionFromErrorCode(errorCode, name, handle, context);
		}
	}
}
