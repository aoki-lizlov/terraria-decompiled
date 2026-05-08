using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000174 RID: 372
	public class AsyncQuerySpaceWorksResult : EventBase
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x00011004 File Offset: 0x0000F204
		public AsyncQuerySpaceWorksResult()
		{
		}

		// Token: 0x04000536 RID: 1334
		public uint total_available_works;

		// Token: 0x04000537 RID: 1335
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
