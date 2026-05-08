using System;

namespace rail
{
	// Token: 0x0200009C RID: 156
	public class RailFriendPlayedGameInfo
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x0001099F File Offset: 0x0000EB9F
		public RailFriendPlayedGameInfo()
		{
		}

		// Token: 0x040001C9 RID: 457
		public bool in_room;

		// Token: 0x040001CA RID: 458
		public RailID friend_id = new RailID();

		// Token: 0x040001CB RID: 459
		public ulong game_server_id;

		// Token: 0x040001CC RID: 460
		public ulong room_id;

		// Token: 0x040001CD RID: 461
		public RailGameID game_id = new RailGameID();

		// Token: 0x040001CE RID: 462
		public bool in_game_server;

		// Token: 0x040001CF RID: 463
		public RailFriendPlayedGamePlayState friend_played_game_play_state;
	}
}
