using System;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x02000398 RID: 920
	public interface ISecurityPolicyEncodable
	{
		// Token: 0x0600280A RID: 10250
		void FromXml(SecurityElement e, PolicyLevel level);

		// Token: 0x0600280B RID: 10251
		SecurityElement ToXml(PolicyLevel level);
	}
}
