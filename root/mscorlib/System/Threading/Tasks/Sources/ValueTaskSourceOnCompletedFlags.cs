using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000357 RID: 855
	[Flags]
	public enum ValueTaskSourceOnCompletedFlags
	{
		// Token: 0x04001C40 RID: 7232
		None = 0,
		// Token: 0x04001C41 RID: 7233
		UseSchedulingContext = 1,
		// Token: 0x04001C42 RID: 7234
		FlowExecutionContext = 2
	}
}
