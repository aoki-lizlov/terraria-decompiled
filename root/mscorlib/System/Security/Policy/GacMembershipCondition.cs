using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003E1 RID: 993
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002A3A RID: 10810 RVA: 0x0009A1E6 File Offset: 0x000983E6
		public GacMembershipCondition()
		{
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x0009A1F8 File Offset: 0x000983F8
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is GacInstalled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0009A22B File Offset: 0x0009842B
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0009A232 File Offset: 0x00098432
		public override bool Equals(object o)
		{
			return o != null && o is GacMembershipCondition;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0009A242 File Offset: 0x00098442
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0009A24C File Offset: 0x0009844C
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x0000408A File Offset: 0x0000228A
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0009A266 File Offset: 0x00098466
		public override string ToString()
		{
			return "GAC";
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0009A26D File Offset: 0x0009846D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x0009A276 File Offset: 0x00098476
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(GacMembershipCondition), this.version);
		}

		// Token: 0x04001E62 RID: 7778
		private readonly int version = 1;
	}
}
