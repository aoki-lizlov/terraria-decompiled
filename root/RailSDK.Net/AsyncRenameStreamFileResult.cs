using System;

namespace rail
{
	// Token: 0x0200014B RID: 331
	public class AsyncRenameStreamFileResult : EventBase
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncRenameStreamFileResult()
		{
		}

		// Token: 0x040004CF RID: 1231
		public string old_filename;

		// Token: 0x040004D0 RID: 1232
		public string new_filename;
	}
}
