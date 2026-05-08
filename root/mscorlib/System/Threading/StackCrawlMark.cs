using System;

namespace System.Threading
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x040019F0 RID: 6640
		LookForMe,
		// Token: 0x040019F1 RID: 6641
		LookForMyCaller,
		// Token: 0x040019F2 RID: 6642
		LookForMyCallersCaller,
		// Token: 0x040019F3 RID: 6643
		LookForThread
	}
}
