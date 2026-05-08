using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000139 RID: 313
	public class RailSmallObjectDownloadResult : EventBase
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		public RailSmallObjectDownloadResult()
		{
		}

		// Token: 0x040004BA RID: 1210
		public List<RailSmallObjectDownloadInfo> download_infos = new List<RailSmallObjectDownloadInfo>();
	}
}
