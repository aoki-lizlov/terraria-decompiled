using System;

namespace rail
{
	// Token: 0x02000148 RID: 328
	public class AsyncQueryQuotaResult : EventBase
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncQueryQuotaResult()
		{
		}

		// Token: 0x040004C5 RID: 1221
		public ulong available_quota;

		// Token: 0x040004C6 RID: 1222
		public ulong total_quota;
	}
}
