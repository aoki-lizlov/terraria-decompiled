using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	// Token: 0x020003CD RID: 973
	[Serializable]
	public sealed class PublisherMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IMembershipCondition
	{
		// Token: 0x06002967 RID: 10599 RVA: 0x000025BE File Offset: 0x000007BE
		public PublisherMembershipCondition(X509Certificate certificate)
		{
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x00097F8B File Offset: 0x0009618B
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x00097F93 File Offset: 0x00096193
		public X509Certificate Certificate
		{
			[CompilerGenerated]
			get
			{
				return this.<Certificate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Certificate>k__BackingField = value;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0000408A File Offset: 0x0000228A
		public bool Check(Evidence evidence)
		{
			return false;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000025CE File Offset: 0x000007CE
		public IMembershipCondition Copy()
		{
			return this;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00097F7A File Offset: 0x0009617A
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00004088 File Offset: 0x00002288
		public void FromXml(SecurityElement e)
		{
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00004088 File Offset: 0x00002288
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00093238 File Offset: 0x00091438
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00097F83 File Offset: 0x00096183
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public SecurityElement ToXml()
		{
			return null;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public SecurityElement ToXml(PolicyLevel level)
		{
			return null;
		}

		// Token: 0x04001E1C RID: 7708
		[CompilerGenerated]
		private X509Certificate <Certificate>k__BackingField;
	}
}
