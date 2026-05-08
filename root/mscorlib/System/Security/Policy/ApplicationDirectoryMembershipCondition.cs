using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Security;

namespace System.Security.Policy
{
	// Token: 0x020003D0 RID: 976
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectoryMembershipCondition : IConstantMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002987 RID: 10631 RVA: 0x00098136 File Offset: 0x00096336
		public ApplicationDirectoryMembershipCondition()
		{
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x00098148 File Offset: 0x00096348
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			string codeBase = Assembly.GetCallingAssembly().CodeBase;
			Uri uri = new Uri(codeBase);
			Url url = new Url(codeBase);
			bool flag = false;
			bool flag2 = false;
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				if (!flag && obj is ApplicationDirectory)
				{
					string directory = (obj as ApplicationDirectory).Directory;
					flag = string.Compare(directory, 0, uri.ToString(), 0, directory.Length, true, CultureInfo.InvariantCulture) == 0;
				}
				else if (!flag2 && obj is Url)
				{
					flag2 = url.Equals(obj);
				}
				if (flag && flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000981EA File Offset: 0x000963EA
		public IMembershipCondition Copy()
		{
			return new ApplicationDirectoryMembershipCondition();
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000981F1 File Offset: 0x000963F1
		public override bool Equals(object o)
		{
			return o is ApplicationDirectoryMembershipCondition;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000981FC File Offset: 0x000963FC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x00098206 File Offset: 0x00096406
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x00098220 File Offset: 0x00096420
		public override int GetHashCode()
		{
			return typeof(ApplicationDirectoryMembershipCondition).GetHashCode();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00098231 File Offset: 0x00096431
		public override string ToString()
		{
			return "ApplicationDirectory";
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00098238 File Offset: 0x00096438
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00098241 File Offset: 0x00096441
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(ApplicationDirectoryMembershipCondition), this.version);
		}

		// Token: 0x04001E1F RID: 7711
		private readonly int version = 1;
	}
}
