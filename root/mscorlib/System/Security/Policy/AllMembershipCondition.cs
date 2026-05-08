using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003CE RID: 974
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002973 RID: 10611 RVA: 0x00097F9C File Offset: 0x0009619C
		public AllMembershipCondition()
		{
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool Check(Evidence evidence)
		{
			return true;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00097FAB File Offset: 0x000961AB
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00097FB2 File Offset: 0x000961B2
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00097FBD File Offset: 0x000961BD
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00097FC7 File Offset: 0x000961C7
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00097FE1 File Offset: 0x000961E1
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00097FF2 File Offset: 0x000961F2
		public override string ToString()
		{
			return "All code";
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x00097FF9 File Offset: 0x000961F9
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00098002 File Offset: 0x00096202
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(AllMembershipCondition), this.version);
		}

		// Token: 0x04001E1D RID: 7709
		private readonly int version = 1;
	}
}
