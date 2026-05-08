using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000172 RID: 370
	public class AsyncGetMySubscribedWorksResult : EventBase
	{
		// Token: 0x06001899 RID: 6297 RVA: 0x00010FD3 File Offset: 0x0000F1D3
		public AsyncGetMySubscribedWorksResult()
		{
		}

		// Token: 0x04000531 RID: 1329
		public uint total_available_works;

		// Token: 0x04000532 RID: 1330
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
