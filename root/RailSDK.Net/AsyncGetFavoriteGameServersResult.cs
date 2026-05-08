using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000B9 RID: 185
	public class AsyncGetFavoriteGameServersResult : EventBase
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x00010ADD File Offset: 0x0000ECDD
		public AsyncGetFavoriteGameServersResult()
		{
		}

		// Token: 0x040001F9 RID: 505
		public List<RailID> server_id_array = new List<RailID>();
	}
}
