using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000C5 RID: 197
	public class GetGameServerListResult : EventBase
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x00010B8E File Offset: 0x0000ED8E
		public GetGameServerListResult()
		{
		}

		// Token: 0x0400022D RID: 557
		public List<GameServerInfo> server_info = new List<GameServerInfo>();

		// Token: 0x0400022E RID: 558
		public uint total_num;

		// Token: 0x0400022F RID: 559
		public uint start_index;

		// Token: 0x04000230 RID: 560
		public uint end_index;
	}
}
