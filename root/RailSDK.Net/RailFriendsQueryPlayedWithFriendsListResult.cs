using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000A7 RID: 167
	public class RailFriendsQueryPlayedWithFriendsListResult : EventBase
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x00010A4D File Offset: 0x0000EC4D
		public RailFriendsQueryPlayedWithFriendsListResult()
		{
		}

		// Token: 0x040001DC RID: 476
		public List<RailID> played_with_friends_list = new List<RailID>();
	}
}
