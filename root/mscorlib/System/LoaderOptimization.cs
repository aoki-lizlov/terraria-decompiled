using System;

namespace System
{
	// Token: 0x020001A0 RID: 416
	public enum LoaderOptimization
	{
		// Token: 0x04001336 RID: 4918
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DisallowBindings = 4,
		// Token: 0x04001337 RID: 4919
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DomainMask = 3,
		// Token: 0x04001338 RID: 4920
		MultiDomain = 2,
		// Token: 0x04001339 RID: 4921
		MultiDomainHost,
		// Token: 0x0400133A RID: 4922
		NotSpecified = 0,
		// Token: 0x0400133B RID: 4923
		SingleDomain
	}
}
