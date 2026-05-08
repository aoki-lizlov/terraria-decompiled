using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000186 RID: 390
	public class RailSpaceWorkFilter
	{
		// Token: 0x060018A4 RID: 6308 RVA: 0x000110BD File Offset: 0x0000F2BD
		public RailSpaceWorkFilter()
		{
		}

		// Token: 0x0400057C RID: 1404
		public List<EnumRailWorkFileClass> classes = new List<EnumRailWorkFileClass>();

		// Token: 0x0400057D RID: 1405
		public List<EnumRailSpaceWorkType> type = new List<EnumRailSpaceWorkType>();

		// Token: 0x0400057E RID: 1406
		public List<RailID> collector_list = new List<RailID>();

		// Token: 0x0400057F RID: 1407
		public List<RailID> subscriber_list = new List<RailID>();

		// Token: 0x04000580 RID: 1408
		public List<RailID> creator_list = new List<RailID>();
	}
}
