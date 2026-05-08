using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000171 RID: 369
	public class AsyncGetMyFavoritesWorksResult : EventBase
	{
		// Token: 0x06001898 RID: 6296 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public AsyncGetMyFavoritesWorksResult()
		{
		}

		// Token: 0x0400052F RID: 1327
		public uint total_available_works;

		// Token: 0x04000530 RID: 1328
		public List<RailSpaceWorkDescriptor> spacework_descriptors = new List<RailSpaceWorkDescriptor>();
	}
}
