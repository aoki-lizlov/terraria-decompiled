using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000136 RID: 310
	public interface IRailSmallObjectServiceHelper
	{
		// Token: 0x060017E1 RID: 6113
		RailResult AsyncDownloadObjects(List<uint> indexes, string user_data);

		// Token: 0x060017E2 RID: 6114
		RailResult GetObjectContent(uint index, out string content);

		// Token: 0x060017E3 RID: 6115
		RailResult AsyncQueryObjectState(string user_data);
	}
}
