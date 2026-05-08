using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000147 RID: 327
	public class AsyncListFileResult : EventBase
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x00010ECE File Offset: 0x0000F0CE
		public AsyncListFileResult()
		{
		}

		// Token: 0x040004C1 RID: 1217
		public List<RailStreamFileInfo> file_list = new List<RailStreamFileInfo>();

		// Token: 0x040004C2 RID: 1218
		public uint try_list_file_num;

		// Token: 0x040004C3 RID: 1219
		public uint all_file_num;

		// Token: 0x040004C4 RID: 1220
		public uint start_index;
	}
}
