using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002AB RID: 683
	internal static class ThreadPoolGlobals
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x00074B39 File Offset: 0x00072D39
		public static bool tpHosted
		{
			get
			{
				return ThreadPool.IsThreadPoolHosted();
			}
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00074B40 File Offset: 0x00072D40
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolGlobals()
		{
		}

		// Token: 0x040019F4 RID: 6644
		public const uint tpQuantum = 30U;

		// Token: 0x040019F5 RID: 6645
		public static int processorCount = Environment.ProcessorCount;

		// Token: 0x040019F6 RID: 6646
		public static volatile bool vmTpInitialized;

		// Token: 0x040019F7 RID: 6647
		public static bool enableWorkerTracking;

		// Token: 0x040019F8 RID: 6648
		[SecurityCritical]
		public static readonly ThreadPoolWorkQueue workQueue = new ThreadPoolWorkQueue();
	}
}
