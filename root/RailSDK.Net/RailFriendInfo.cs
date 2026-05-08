using System;

namespace rail
{
	// Token: 0x0200009A RID: 154
	public class RailFriendInfo
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0001096E File Offset: 0x0000EB6E
		public RailFriendInfo()
		{
		}

		// Token: 0x040001C3 RID: 451
		public RailID friend_rail_id = new RailID();

		// Token: 0x040001C4 RID: 452
		public EnumRailFriendType friend_type;

		// Token: 0x040001C5 RID: 453
		public RailFriendOnLineState online_state = new RailFriendOnLineState();
	}
}
