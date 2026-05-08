using System;

namespace System.Security.Policy
{
	// Token: 0x020003CA RID: 970
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x0600295C RID: 10588
		bool Check(Evidence evidence);

		// Token: 0x0600295D RID: 10589
		IMembershipCondition Copy();

		// Token: 0x0600295E RID: 10590
		bool Equals(object obj);

		// Token: 0x0600295F RID: 10591
		string ToString();
	}
}
