using System;

namespace rail
{
	// Token: 0x0200014A RID: 330
	public class AsyncReadStreamFileResult : EventBase
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncReadStreamFileResult()
		{
		}

		// Token: 0x040004CB RID: 1227
		public uint try_read_length;

		// Token: 0x040004CC RID: 1228
		public int offset;

		// Token: 0x040004CD RID: 1229
		public string data;

		// Token: 0x040004CE RID: 1230
		public string filename;
	}
}
