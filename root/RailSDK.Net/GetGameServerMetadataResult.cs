using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000C6 RID: 198
	public class GetGameServerMetadataResult : EventBase
	{
		// Token: 0x0600170C RID: 5900 RVA: 0x00010BA1 File Offset: 0x0000EDA1
		public GetGameServerMetadataResult()
		{
		}

		// Token: 0x04000231 RID: 561
		public RailID game_server_id = new RailID();

		// Token: 0x04000232 RID: 562
		public List<RailKeyValue> key_value = new List<RailKeyValue>();
	}
}
