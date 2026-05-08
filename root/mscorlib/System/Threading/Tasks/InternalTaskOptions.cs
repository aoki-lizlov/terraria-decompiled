using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000332 RID: 818
	[Flags]
	internal enum InternalTaskOptions
	{
		// Token: 0x04001BD9 RID: 7129
		None = 0,
		// Token: 0x04001BDA RID: 7130
		InternalOptionsMask = 65280,
		// Token: 0x04001BDB RID: 7131
		ContinuationTask = 512,
		// Token: 0x04001BDC RID: 7132
		PromiseTask = 1024,
		// Token: 0x04001BDD RID: 7133
		LazyCancellation = 4096,
		// Token: 0x04001BDE RID: 7134
		QueuedByRuntime = 8192,
		// Token: 0x04001BDF RID: 7135
		DoNotDispose = 16384
	}
}
