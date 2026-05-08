using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000B5 RID: 181
	public interface IRailGameServer : IRailComponent
	{
		// Token: 0x060016BF RID: 5823
		RailID GetGameServerRailID();

		// Token: 0x060016C0 RID: 5824
		RailResult GetGameServerName(out string name);

		// Token: 0x060016C1 RID: 5825
		RailResult GetGameServerFullName(out string full_name);

		// Token: 0x060016C2 RID: 5826
		RailID GetOwnerRailID();

		// Token: 0x060016C3 RID: 5827
		bool SetZoneID(ulong zone_id);

		// Token: 0x060016C4 RID: 5828
		ulong GetZoneID();

		// Token: 0x060016C5 RID: 5829
		bool SetHost(string game_server_host);

		// Token: 0x060016C6 RID: 5830
		bool GetHost(out string game_server_host);

		// Token: 0x060016C7 RID: 5831
		bool SetMapName(string game_server_map);

		// Token: 0x060016C8 RID: 5832
		bool GetMapName(out string game_server_map);

		// Token: 0x060016C9 RID: 5833
		bool SetPasswordProtect(bool has_password);

		// Token: 0x060016CA RID: 5834
		bool GetPasswordProtect();

		// Token: 0x060016CB RID: 5835
		bool SetMaxPlayers(uint max_player_count);

		// Token: 0x060016CC RID: 5836
		uint GetMaxPlayers();

		// Token: 0x060016CD RID: 5837
		bool SetBotPlayers(uint bot_player_count);

		// Token: 0x060016CE RID: 5838
		uint GetBotPlayers();

		// Token: 0x060016CF RID: 5839
		bool SetGameServerDescription(string game_server_description);

		// Token: 0x060016D0 RID: 5840
		bool GetGameServerDescription(out string game_server_description);

		// Token: 0x060016D1 RID: 5841
		bool SetGameServerTags(string game_server_tags);

		// Token: 0x060016D2 RID: 5842
		bool GetGameServerTags(out string game_server_tags);

		// Token: 0x060016D3 RID: 5843
		bool SetMods(List<string> server_mods);

		// Token: 0x060016D4 RID: 5844
		bool GetMods(List<string> server_mods);

		// Token: 0x060016D5 RID: 5845
		bool SetSpectatorHost(string spectator_host);

		// Token: 0x060016D6 RID: 5846
		bool GetSpectatorHost(out string spectator_host);

		// Token: 0x060016D7 RID: 5847
		bool SetGameServerVersion(string version);

		// Token: 0x060016D8 RID: 5848
		bool GetGameServerVersion(out string version);

		// Token: 0x060016D9 RID: 5849
		bool SetIsFriendOnly(bool is_friend_only);

		// Token: 0x060016DA RID: 5850
		bool GetIsFriendOnly();

		// Token: 0x060016DB RID: 5851
		bool ClearAllMetadata();

		// Token: 0x060016DC RID: 5852
		RailResult GetMetadata(string key, out string value);

		// Token: 0x060016DD RID: 5853
		RailResult SetMetadata(string key, string value);

		// Token: 0x060016DE RID: 5854
		RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data);

		// Token: 0x060016DF RID: 5855
		RailResult AsyncGetMetadata(List<string> keys, string user_data);

		// Token: 0x060016E0 RID: 5856
		RailResult AsyncGetAllMetadata(string user_data);

		// Token: 0x060016E1 RID: 5857
		RailResult AsyncAcquireGameServerSessionTicket(string user_data);

		// Token: 0x060016E2 RID: 5858
		RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data);

		// Token: 0x060016E3 RID: 5859
		void TerminateSessionOfPlayer(RailID player_rail_id);

		// Token: 0x060016E4 RID: 5860
		void AbandonGameServerSessionTicket(RailSessionTicket session_ticket);

		// Token: 0x060016E5 RID: 5861
		RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos);

		// Token: 0x060016E6 RID: 5862
		RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos);

		// Token: 0x060016E7 RID: 5863
		RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos);

		// Token: 0x060016E8 RID: 5864
		uint GetCurrentPlayers();

		// Token: 0x060016E9 RID: 5865
		void RemoveAllPlayers();

		// Token: 0x060016EA RID: 5866
		RailResult RegisterToGameServerList();

		// Token: 0x060016EB RID: 5867
		RailResult UnregisterFromGameServerList();

		// Token: 0x060016EC RID: 5868
		RailResult CloseGameServer();

		// Token: 0x060016ED RID: 5869
		RailResult GetFriendsInGameServer(List<RailID> friend_ids);

		// Token: 0x060016EE RID: 5870
		bool IsUserInGameServer(RailID user_rail_id);

		// Token: 0x060016EF RID: 5871
		bool SetServerInfo(string server_info);

		// Token: 0x060016F0 RID: 5872
		bool GetServerInfo(out string server_info);

		// Token: 0x060016F1 RID: 5873
		RailResult EnableTeamVoice(bool enable);
	}
}
