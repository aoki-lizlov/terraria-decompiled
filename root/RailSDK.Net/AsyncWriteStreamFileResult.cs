using System;

namespace rail
{
	// Token: 0x0200014D RID: 333
	public class AsyncWriteStreamFileResult : EventBase
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncWriteStreamFileResult()
		{
		}

		// Token: 0x040004D5 RID: 1237
		public uint written_length;

		// Token: 0x040004D6 RID: 1238
		public int offset;

		// Token: 0x040004D7 RID: 1239
		public uint try_write_length;

		// Token: 0x040004D8 RID: 1240
		public string filename;
	}
}
