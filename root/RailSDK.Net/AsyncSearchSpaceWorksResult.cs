using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000176 RID: 374
	public class AsyncSearchSpaceWorksResult : EventBase
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x0001102A File Offset: 0x0000F22A
		public AsyncSearchSpaceWorksResult()
		{
		}

		// Token: 0x04000539 RID: 1337
		public uint total_available_works;

		// Token: 0x0400053A RID: 1338
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
