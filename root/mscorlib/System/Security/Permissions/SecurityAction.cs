using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000425 RID: 1061
	[ComVisible(true)]
	[Serializable]
	public enum SecurityAction
	{
		// Token: 0x04001F55 RID: 8021
		Demand = 2,
		// Token: 0x04001F56 RID: 8022
		Assert,
		// Token: 0x04001F57 RID: 8023
		[Obsolete("This requests should not be used")]
		Deny,
		// Token: 0x04001F58 RID: 8024
		PermitOnly,
		// Token: 0x04001F59 RID: 8025
		LinkDemand,
		// Token: 0x04001F5A RID: 8026
		InheritanceDemand,
		// Token: 0x04001F5B RID: 8027
		[Obsolete("This requests should not be used")]
		RequestMinimum,
		// Token: 0x04001F5C RID: 8028
		[Obsolete("This requests should not be used")]
		RequestOptional,
		// Token: 0x04001F5D RID: 8029
		[Obsolete("This requests should not be used")]
		RequestRefuse
	}
}
