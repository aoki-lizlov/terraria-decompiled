using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000C7 RID: 199
	public class GetGameServerPlayerListResult : EventBase
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x00010BBF File Offset: 0x0000EDBF
		public GetGameServerPlayerListResult()
		{
		}

		// Token: 0x04000233 RID: 563
		public RailID game_server_id = new RailID();

		// Token: 0x04000234 RID: 564
		public List<GameServerPlayerInfo> server_player_info = new List<GameServerPlayerInfo>();
	}
}
