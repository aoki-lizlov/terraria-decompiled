using System;

namespace System.Globalization
{
	// Token: 0x020009C5 RID: 2501
	internal class InternalGlobalizationHelper
	{
		// Token: 0x06005B5E RID: 23390 RVA: 0x0013736C File Offset: 0x0013556C
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, "TimeSpan overflowed because the duration is too long.");
			}
			return num * 10000000L;
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x000025BE File Offset: 0x000007BE
		public InternalGlobalizationHelper()
		{
		}

		// Token: 0x040036EA RID: 14058
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040036EB RID: 14059
		internal const long TicksPerTenthSecond = 1000000L;

		// Token: 0x040036EC RID: 14060
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040036ED RID: 14061
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x040036EE RID: 14062
		internal const long MinSeconds = -922337203685L;

		// Token: 0x040036EF RID: 14063
		private const int DaysPerYear = 365;

		// Token: 0x040036F0 RID: 14064
		private const int DaysPer4Years = 1461;

		// Token: 0x040036F1 RID: 14065
		private const int DaysPer100Years = 36524;

		// Token: 0x040036F2 RID: 14066
		private const int DaysPer400Years = 146097;

		// Token: 0x040036F3 RID: 14067
		private const int DaysTo10000 = 3652059;

		// Token: 0x040036F4 RID: 14068
		private const long TicksPerMinute = 600000000L;

		// Token: 0x040036F5 RID: 14069
		private const long TicksPerHour = 36000000000L;

		// Token: 0x040036F6 RID: 14070
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040036F7 RID: 14071
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x040036F8 RID: 14072
		internal const long MinTicks = 0L;

		// Token: 0x040036F9 RID: 14073
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x040036FA RID: 14074
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x040036FB RID: 14075
		internal const int StringBuilderDefaultCapacity = 16;

		// Token: 0x040036FC RID: 14076
		internal const long MaxOffset = 504000000000L;

		// Token: 0x040036FD RID: 14077
		internal const long MinOffset = -504000000000L;
	}
}
