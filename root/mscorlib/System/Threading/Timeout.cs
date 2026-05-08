using System;

namespace System.Threading
{
	// Token: 0x02000271 RID: 625
	public static class Timeout
	{
		// Token: 0x06001D82 RID: 7554 RVA: 0x0006F293 File Offset: 0x0006D493
		// Note: this type is marked as 'beforefieldinit'.
		static Timeout()
		{
		}

		// Token: 0x04001934 RID: 6452
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x04001935 RID: 6453
		public const int Infinite = -1;

		// Token: 0x04001936 RID: 6454
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
