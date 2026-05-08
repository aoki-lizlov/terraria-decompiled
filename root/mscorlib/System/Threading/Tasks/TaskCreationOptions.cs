using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000331 RID: 817
	[Flags]
	public enum TaskCreationOptions
	{
		// Token: 0x04001BD1 RID: 7121
		None = 0,
		// Token: 0x04001BD2 RID: 7122
		PreferFairness = 1,
		// Token: 0x04001BD3 RID: 7123
		LongRunning = 2,
		// Token: 0x04001BD4 RID: 7124
		AttachedToParent = 4,
		// Token: 0x04001BD5 RID: 7125
		DenyChildAttach = 8,
		// Token: 0x04001BD6 RID: 7126
		HideScheduler = 16,
		// Token: 0x04001BD7 RID: 7127
		RunContinuationsAsynchronously = 64
	}
}
