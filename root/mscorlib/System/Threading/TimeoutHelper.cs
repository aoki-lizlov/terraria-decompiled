using System;

namespace System.Threading
{
	// Token: 0x0200027A RID: 634
	internal static class TimeoutHelper
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x00070128 File Offset: 0x0006E328
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00070130 File Offset: 0x0006E330
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
