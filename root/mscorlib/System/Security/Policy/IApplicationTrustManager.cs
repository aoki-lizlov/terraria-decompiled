using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003E4 RID: 996
	[ComVisible(true)]
	public interface IApplicationTrustManager : ISecurityEncodable
	{
		// Token: 0x06002A64 RID: 10852
		ApplicationTrust DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context);
	}
}
