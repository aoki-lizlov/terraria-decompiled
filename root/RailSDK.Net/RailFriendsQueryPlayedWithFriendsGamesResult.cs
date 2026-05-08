using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000A6 RID: 166
	public class RailFriendsQueryPlayedWithFriendsGamesResult : EventBase
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x00010A3A File Offset: 0x0000EC3A
		public RailFriendsQueryPlayedWithFriendsGamesResult()
		{
		}

		// Token: 0x040001DB RID: 475
		public List<RailPlayedWithFriendsGameItem> played_with_friends_game_list = new List<RailPlayedWithFriendsGameItem>();
	}
}
