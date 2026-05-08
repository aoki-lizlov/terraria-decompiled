using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003F4 RID: 1012
	[ComVisible(true)]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup
	{
		// Token: 0x06002B04 RID: 11012 RVA: 0x00099FDE File Offset: 0x000981DE
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
			: base(membershipCondition, policy)
		{
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00099D07 File Offset: 0x00097F07
		internal UnionCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0009CD00 File Offset: 0x0009AF00
		public override CodeGroup Copy()
		{
			return this.Copy(true);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0009CD0C File Offset: 0x0009AF0C
		internal CodeGroup Copy(bool childs)
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(base.MembershipCondition, base.PolicyStatement);
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			if (childs)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					unionCodeGroup.AddChild(codeGroup.Copy());
				}
			}
			return unionCodeGroup;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0009CD98 File Offset: 0x0009AF98
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
			PermissionSet permissionSet = base.PolicyStatement.PermissionSet.Copy();
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0009CE50 File Offset: 0x0009B050
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
			CodeGroup codeGroup = this.Copy(false);
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
			}
			return codeGroup;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x00099DA0 File Offset: 0x00097FA0
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}
	}
}
