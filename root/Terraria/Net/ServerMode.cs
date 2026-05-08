using System;

namespace Terraria.Net
{
	// Token: 0x0200016C RID: 364
	[Flags]
	public enum ServerMode : byte
	{
		// Token: 0x0400166C RID: 5740
		None = 0,
		// Token: 0x0400166D RID: 5741
		Lobby = 1,
		// Token: 0x0400166E RID: 5742
		FriendsCanJoin = 2,
		// Token: 0x0400166F RID: 5743
		FriendsOfFriends = 4
	}
}
