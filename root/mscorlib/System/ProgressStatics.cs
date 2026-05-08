using System;
using System.Threading;

namespace System
{
	// Token: 0x02000138 RID: 312
	internal static class ProgressStatics
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003365B File Offset: 0x0003185B
		// Note: this type is marked as 'beforefieldinit'.
		static ProgressStatics()
		{
		}

		// Token: 0x04001147 RID: 4423
		internal static readonly SynchronizationContext DefaultContext = new SynchronizationContext();
	}
}
