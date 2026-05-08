using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000A8 RID: 168
	public class RailFriendsQueryPlayedWithFriendsTimeResult : EventBase
	{
		// Token: 0x060016A2 RID: 5794 RVA: 0x00010A60 File Offset: 0x0000EC60
		public RailFriendsQueryPlayedWithFriendsTimeResult()
		{
		}

		// Token: 0x040001DD RID: 477
		public List<RailPlayedWithFriendsTimeItem> played_with_friends_time_list = new List<RailPlayedWithFriendsTimeItem>();
	}
}
