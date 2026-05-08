using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000A5 RID: 165
	public class RailFriendsQueryFriendPlayedGamesResult : EventBase
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x00010A27 File Offset: 0x0000EC27
		public RailFriendsQueryFriendPlayedGamesResult()
		{
		}

		// Token: 0x040001DA RID: 474
		public List<RailFriendPlayedGameInfo> friend_played_games_info_list = new List<RailFriendPlayedGameInfo>();
	}
}
