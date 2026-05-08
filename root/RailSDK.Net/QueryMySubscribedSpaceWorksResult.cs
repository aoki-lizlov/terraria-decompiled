using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000183 RID: 387
	public class QueryMySubscribedSpaceWorksResult
	{
		// Token: 0x060018A1 RID: 6305 RVA: 0x00011081 File Offset: 0x0000F281
		public QueryMySubscribedSpaceWorksResult()
		{
		}

		// Token: 0x0400056C RID: 1388
		public uint total_available_works;

		// Token: 0x0400056D RID: 1389
		public EnumRailSpaceWorkType spacework_type;

		// Token: 0x0400056E RID: 1390
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
