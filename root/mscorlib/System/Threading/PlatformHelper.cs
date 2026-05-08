using System;

namespace System.Threading
{
	// Token: 0x02000279 RID: 633
	internal static class PlatformHelper
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000700D4 File Offset: 0x0006E2D4
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00070119 File Offset: 0x0006E319
		// Note: this type is marked as 'beforefieldinit'.
		static PlatformHelper()
		{
		}

		// Token: 0x04001957 RID: 6487
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x04001958 RID: 6488
		private static volatile int s_processorCount;

		// Token: 0x04001959 RID: 6489
		private static volatile int s_lastProcessorCountRefreshTicks;

		// Token: 0x0400195A RID: 6490
		internal static readonly bool IsSingleProcessor = PlatformHelper.ProcessorCount == 1;
	}
}
