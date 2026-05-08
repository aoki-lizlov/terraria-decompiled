using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002B1 RID: 689
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06001FD9 RID: 8153 RVA: 0x00075740 File Offset: 0x00073940
		[SecurityCritical]
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
