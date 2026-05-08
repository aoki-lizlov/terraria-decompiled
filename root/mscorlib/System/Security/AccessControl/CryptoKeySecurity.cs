using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004EC RID: 1260
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		// Token: 0x0600335C RID: 13148 RVA: 0x000BD988 File Offset: 0x000BBB88
		public CryptoKeySecurity()
			: base(false, ResourceType.Unknown)
		{
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000BD992 File Offset: 0x000BBB92
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
			: base(securityDescriptor, ResourceType.Unknown)
		{
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000BD99C File Offset: 0x000BBB9C
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000BD9A8 File Offset: 0x000BBBA8
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000BD9B4 File Offset: 0x000BBBB4
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000BD9C0 File Offset: 0x000BBBC0
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, (CryptoKeyRights)accessMask, type);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000BD9CB File Offset: 0x000BBBCB
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000BD9D4 File Offset: 0x000BBBD4
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000BD9DD File Offset: 0x000BBBDD
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000BD9EF File Offset: 0x000BBBEF
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000BDA01 File Offset: 0x000BBC01
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, (CryptoKeyRights)accessMask, flags);
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000BDA15 File Offset: 0x000BBC15
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000BDA1E File Offset: 0x000BBC1E
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000BDA27 File Offset: 0x000BBC27
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000BDA30 File Offset: 0x000BBC30
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			base.SetAuditRule(rule);
		}
	}
}
