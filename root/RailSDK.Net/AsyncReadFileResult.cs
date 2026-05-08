using System;

namespace rail
{
	// Token: 0x02000149 RID: 329
	public class AsyncReadFileResult : EventBase
	{
		// Token: 0x06001826 RID: 6182 RVA: 0x0000F049 File Offset: 0x0000D249
		public AsyncReadFileResult()
		{
		}

		// Token: 0x040004C7 RID: 1223
		public uint try_read_length;

		// Token: 0x040004C8 RID: 1224
		public int offset;

		// Token: 0x040004C9 RID: 1225
		public string data;

		// Token: 0x040004CA RID: 1226
		public string filename;
	}
}
