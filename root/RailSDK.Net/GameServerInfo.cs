using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000BD RID: 189
	public class GameServerInfo
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x00010B16 File Offset: 0x0000ED16
		public GameServerInfo()
		{
		}

		// Token: 0x040001FE RID: 510
		public List<RailKeyValue> server_kvs = new List<RailKeyValue>();

		// Token: 0x040001FF RID: 511
		public RailID owner_rail_id = new RailID();

		// Token: 0x04000200 RID: 512
		public string game_server_name;

		// Token: 0x04000201 RID: 513
		public string server_fullname;

		// Token: 0x04000202 RID: 514
		public bool is_dedicated;

		// Token: 0x04000203 RID: 515
		public string server_info;

		// Token: 0x04000204 RID: 516
		public string server_tags;

		// Token: 0x04000205 RID: 517
		public string spectator_host;

		// Token: 0x04000206 RID: 518
		public string server_description;

		// Token: 0x04000207 RID: 519
		public string server_host;

		// Token: 0x04000208 RID: 520
		public RailID game_server_rail_id = new RailID();

		// Token: 0x04000209 RID: 521
		public bool has_password;

		// Token: 0x0400020A RID: 522
		public string server_version;

		// Token: 0x0400020B RID: 523
		public List<string> server_mods = new List<string>();

		// Token: 0x0400020C RID: 524
		public uint bot_players;

		// Token: 0x0400020D RID: 525
		public string game_server_map;

		// Token: 0x0400020E RID: 526
		public uint max_players;

		// Token: 0x0400020F RID: 527
		public uint current_players;

		// Token: 0x04000210 RID: 528
		public bool is_friend_only;

		// Token: 0x04000211 RID: 529
		public ulong zone_id;
	}
}
