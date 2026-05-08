using System;

namespace rail
{
	// Token: 0x0200009F RID: 159
	public class RailFriendsAddFriendResult : EventBase
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x000109D0 File Offset: 0x0000EBD0
		public RailFriendsAddFriendResult()
		{
		}

		// Token: 0x040001D4 RID: 468
		public RailID target_rail_id = new RailID();
	}
}
