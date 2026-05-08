using System;

namespace System.Threading
{
	// Token: 0x0200026F RID: 623
	[Flags]
	public enum ThreadState
	{
		// Token: 0x0400192A RID: 6442
		Running = 0,
		// Token: 0x0400192B RID: 6443
		StopRequested = 1,
		// Token: 0x0400192C RID: 6444
		SuspendRequested = 2,
		// Token: 0x0400192D RID: 6445
		Background = 4,
		// Token: 0x0400192E RID: 6446
		Unstarted = 8,
		// Token: 0x0400192F RID: 6447
		Stopped = 16,
		// Token: 0x04001930 RID: 6448
		WaitSleepJoin = 32,
		// Token: 0x04001931 RID: 6449
		Suspended = 64,
		// Token: 0x04001932 RID: 6450
		AbortRequested = 128,
		// Token: 0x04001933 RID: 6451
		Aborted = 256
	}
}
