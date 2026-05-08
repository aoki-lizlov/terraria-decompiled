using System;

namespace rail
{
	// Token: 0x0200014C RID: 332
	public class AsyncWriteFileResult : EventBase
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncWriteFileResult()
		{
		}

		// Token: 0x040004D1 RID: 1233
		public uint written_length;

		// Token: 0x040004D2 RID: 1234
		public int offset;

		// Token: 0x040004D3 RID: 1235
		public uint try_write_length;

		// Token: 0x040004D4 RID: 1236
		public string filename;
	}
}
