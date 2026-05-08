using System;

namespace rail
{
	// Token: 0x020000A4 RID: 164
	public class RailFriendsOnlineStateChanged : EventBase
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00010A14 File Offset: 0x0000EC14
		public RailFriendsOnlineStateChanged()
		{
		}

		// Token: 0x040001D9 RID: 473
		public RailFriendOnLineState friend_online_state = new RailFriendOnLineState();
	}
}
