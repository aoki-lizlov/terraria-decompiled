using System;
using System.Diagnostics;

namespace ReLogic.Threading
{
	// Token: 0x0200000E RID: 14
	public static class ThreadUtilities
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003B24 File Offset: 0x00001D24
		public static void HighPrecisionSleep(double timeInMs)
		{
			double num = (double)Stopwatch.Frequency / 1000.0;
			long num2 = Stopwatch.GetTimestamp() + (long)(timeInMs * num);
			while (Stopwatch.GetTimestamp() < num2)
			{
			}
		}
	}
}
