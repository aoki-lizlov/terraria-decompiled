using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000B6 RID: 182
	public interface IRailGameServerHelper
	{
		// Token: 0x060016F2 RID: 5874
		RailResult AsyncGetGameServerPlayerList(RailID gameserver_rail_id, string user_data);

		// Token: 0x060016F3 RID: 5875
		RailResult AsyncGetGameServerList(uint start_index, uint end_index, List<GameServerListFilter> alternative_filters, List<GameServerListSorter> sorter, string user_data);

		// Token: 0x060016F4 RID: 5876
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name, string user_data);

		// Token: 0x060016F5 RID: 5877
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name);

		// Token: 0x060016F6 RID: 5878
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options);

		// Token: 0x060016F7 RID: 5879
		IRailGameServer AsyncCreateGameServer();

		// Token: 0x060016F8 RID: 5880
		RailResult AsyncGetFavoriteGameServers(string user_data);

		// Token: 0x060016F9 RID: 5881
		RailResult AsyncGetFavoriteGameServers();

		// Token: 0x060016FA RID: 5882
		RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data);

		// Token: 0x060016FB RID: 5883
		RailResult AsyncAddFavoriteGameServer(RailID game_server_id);

		// Token: 0x060016FC RID: 5884
		RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data);

		// Token: 0x060016FD RID: 5885
		RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id);
	}
}
