using System;

namespace rail
{
	// Token: 0x0200009B RID: 155
	public class RailFriendOnLineState
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x0001098C File Offset: 0x0000EB8C
		public RailFriendOnLineState()
		{
		}

		// Token: 0x040001C6 RID: 454
		public RailID friend_rail_id = new RailID();

		// Token: 0x040001C7 RID: 455
		public uint game_define_game_playing_state;

		// Token: 0x040001C8 RID: 456
		public EnumRailPlayerOnLineState friend_online_state;
	}
}
