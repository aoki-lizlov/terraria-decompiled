using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000BE RID: 190
	public class GameServerListFilter
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00010B4A File Offset: 0x0000ED4A
		public GameServerListFilter()
		{
		}

		// Token: 0x04000212 RID: 530
		public string tags_contained;

		// Token: 0x04000213 RID: 531
		public string tags_not_contained;

		// Token: 0x04000214 RID: 532
		public EnumRailOptionalValue filter_password;

		// Token: 0x04000215 RID: 533
		public string filter_game_server_name;

		// Token: 0x04000216 RID: 534
		public ulong filter_zone_id;

		// Token: 0x04000217 RID: 535
		public EnumRailOptionalValue filter_friends_created;

		// Token: 0x04000218 RID: 536
		public EnumRailOptionalValue filter_dedicated_server;

		// Token: 0x04000219 RID: 537
		public List<GameServerListFilterKey> filters = new List<GameServerListFilterKey>();

		// Token: 0x0400021A RID: 538
		public string filter_game_server_map;

		// Token: 0x0400021B RID: 539
		public string filter_game_server_host;

		// Token: 0x0400021C RID: 540
		public RailID owner_id = new RailID();
	}
}
