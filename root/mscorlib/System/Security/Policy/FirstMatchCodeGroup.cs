using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003DF RID: 991
	[ComVisible(true)]
	[Serializable]
	public sealed class FirstMatchCodeGroup : CodeGroup
	{
		// Token: 0x06002A2A RID: 10794 RVA: 0x00099FDE File Offset: 0x000981DE
		public FirstMatchCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
			: base(membershipCondition, policy)
		{
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00099D07 File Offset: 0x00097F07
		internal FirstMatchCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x00099FE8 File Offset: 0x000981E8
		public override string MergeLogic
		{
			get
			{
				return "First Match";
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00099FF0 File Offset: 0x000981F0
		public override CodeGroup Copy()
		{
			FirstMatchCodeGroup firstMatchCodeGroup = this.CopyNoChildren();
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				firstMatchCodeGroup.AddChild(codeGroup.Copy());
			}
			return firstMatchCodeGroup;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x0009A058 File Offset: 0x00098258
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			foreach (object obj in base.Children)
			{
				PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
				if (policyStatement != null)
				{
					return policyStatement;
				}
			}
			return base.PolicyStatement;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0009A0E0 File Offset: 0x000982E0
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				if (codeGroup.Resolve(evidence) != null)
				{
					return codeGroup.Copy();
				}
			}
			return this.CopyNoChildren();
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0009A16C File Offset: 0x0009836C
		private FirstMatchCodeGroup CopyNoChildren()
		{
			return new FirstMatchCodeGroup(base.MembershipCondition, base.PolicyStatement)
			{
				Name = base.Name,
				Description = base.Description
			};
		}
	}
}
