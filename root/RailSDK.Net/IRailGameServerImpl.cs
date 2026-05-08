using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000015 RID: 21
	public class IRailGameServerImpl : RailObject, IRailGameServer, IRailComponent
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailGameServerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00003B9C File Offset: 0x00001D9C
		~IRailGameServerImpl()
		{
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public virtual RailID GetGameServerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServer_GetGameServerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00003BEC File Offset: 0x00001DEC
		public virtual RailResult GetGameServerName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetGameServerName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00003C30 File Offset: 0x00001E30
		public virtual RailResult GetGameServerFullName(out string full_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetGameServerFullName(this.swigCPtr_, intPtr);
			}
			finally
			{
				full_name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00003C74 File Offset: 0x00001E74
		public virtual RailID GetOwnerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServer_GetOwnerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00003C99 File Offset: 0x00001E99
		public virtual bool SetZoneID(ulong zone_id)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetZoneID(this.swigCPtr_, zone_id);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00003CA7 File Offset: 0x00001EA7
		public virtual ulong GetZoneID()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetZoneID(this.swigCPtr_);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public virtual bool SetHost(string game_server_host)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetHost(this.swigCPtr_, game_server_host);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public virtual bool GetHost(out string game_server_host)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetHost(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_host = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00003D08 File Offset: 0x00001F08
		public virtual bool SetMapName(string game_server_map)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetMapName(this.swigCPtr_, game_server_map);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00003D18 File Offset: 0x00001F18
		public virtual bool GetMapName(out string game_server_map)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetMapName(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_map = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00003D5C File Offset: 0x00001F5C
		public virtual bool SetPasswordProtect(bool has_password)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetPasswordProtect(this.swigCPtr_, has_password);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00003D6A File Offset: 0x00001F6A
		public virtual bool GetPasswordProtect()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetPasswordProtect(this.swigCPtr_);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00003D77 File Offset: 0x00001F77
		public virtual bool SetMaxPlayers(uint max_player_count)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetMaxPlayers(this.swigCPtr_, max_player_count);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00003D85 File Offset: 0x00001F85
		public virtual uint GetMaxPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetMaxPlayers(this.swigCPtr_);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00003D92 File Offset: 0x00001F92
		public virtual bool SetBotPlayers(uint bot_player_count)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetBotPlayers(this.swigCPtr_, bot_player_count);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public virtual uint GetBotPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetBotPlayers(this.swigCPtr_);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00003DAD File Offset: 0x00001FAD
		public virtual bool SetGameServerDescription(string game_server_description)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerDescription(this.swigCPtr_, game_server_description);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00003DBC File Offset: 0x00001FBC
		public virtual bool GetGameServerDescription(out string game_server_description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerDescription(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_description = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00003E00 File Offset: 0x00002000
		public virtual bool SetGameServerTags(string game_server_tags)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerTags(this.swigCPtr_, game_server_tags);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00003E10 File Offset: 0x00002010
		public virtual bool GetGameServerTags(out string game_server_tags)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_server_tags = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00003E54 File Offset: 0x00002054
		public virtual bool SetMods(List<string> server_mods)
		{
			IntPtr intPtr = ((server_mods == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (server_mods != null)
			{
				RailConverter.Csharp2Cpp(server_mods, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_SetMods(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00003EA4 File Offset: 0x000020A4
		public virtual bool GetMods(List<string> server_mods)
		{
			IntPtr intPtr = ((server_mods == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetMods(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (server_mods != null)
				{
					RailConverter.Cpp2Csharp(intPtr, server_mods);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00003EF4 File Offset: 0x000020F4
		public virtual bool SetSpectatorHost(string spectator_host)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetSpectatorHost(this.swigCPtr_, spectator_host);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00003F04 File Offset: 0x00002104
		public virtual bool GetSpectatorHost(out string spectator_host)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetSpectatorHost(this.swigCPtr_, intPtr);
			}
			finally
			{
				spectator_host = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00003F48 File Offset: 0x00002148
		public virtual bool SetGameServerVersion(string version)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetGameServerVersion(this.swigCPtr_, version);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00003F58 File Offset: 0x00002158
		public virtual bool GetGameServerVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetGameServerVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00003F9C File Offset: 0x0000219C
		public virtual bool SetIsFriendOnly(bool is_friend_only)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetIsFriendOnly(this.swigCPtr_, is_friend_only);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00003FAA File Offset: 0x000021AA
		public virtual bool GetIsFriendOnly()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetIsFriendOnly(this.swigCPtr_);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool ClearAllMetadata()
		{
			return RAIL_API_PINVOKE.IRailGameServer_ClearAllMetadata(this.swigCPtr_);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00003FC4 File Offset: 0x000021C4
		public virtual RailResult GetMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00004008 File Offset: 0x00002208
		public virtual RailResult SetMetadata(string key, string value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_SetMetadata(this.swigCPtr_, key, value);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00004018 File Offset: 0x00002218
		public virtual RailResult AsyncSetMetadata(List<RailKeyValue> key_values, string user_data)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (key_values != null)
			{
				RailConverter.Csharp2Cpp(key_values, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncSetMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00004068 File Offset: 0x00002268
		public virtual RailResult AsyncGetMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncGetMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000040B8 File Offset: 0x000022B8
		public virtual RailResult AsyncGetAllMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncGetAllMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000040C6 File Offset: 0x000022C6
		public virtual RailResult AsyncAcquireGameServerSessionTicket(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncAcquireGameServerSessionTicket(this.swigCPtr_, user_data);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000040D4 File Offset: 0x000022D4
		public virtual RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data)
		{
			IntPtr intPtr = ((player_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (player_ticket != null)
			{
				RailConverter.Csharp2Cpp(player_ticket, intPtr);
			}
			IntPtr intPtr2 = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_AsyncStartSessionWithPlayer(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00004154 File Offset: 0x00002354
		public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
		{
			IntPtr intPtr = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailGameServer_TerminateSessionOfPlayer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000041B0 File Offset: 0x000023B0
		public virtual void AbandonGameServerSessionTicket(RailSessionTicket session_ticket)
		{
			IntPtr intPtr = ((session_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (session_ticket != null)
			{
				RailConverter.Csharp2Cpp(session_ticket, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailGameServer_AbandonGameServerSessionTicket(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00004200 File Offset: 0x00002400
		public virtual RailResult ReportPlayerJoinGameServer(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_ReportPlayerJoinGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00004250 File Offset: 0x00002450
		public virtual RailResult ReportPlayerQuitGameServer(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_ReportPlayerQuitGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000042A0 File Offset: 0x000024A0
		public virtual RailResult UpdateGameServerPlayerList(List<GameServerPlayerInfo> player_infos)
		{
			IntPtr intPtr = ((player_infos == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerPlayerInfo__SWIG_0());
			if (player_infos != null)
			{
				RailConverter.Csharp2Cpp(player_infos, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_UpdateGameServerPlayerList(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerPlayerInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000042F0 File Offset: 0x000024F0
		public virtual uint GetCurrentPlayers()
		{
			return RAIL_API_PINVOKE.IRailGameServer_GetCurrentPlayers(this.swigCPtr_);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000042FD File Offset: 0x000024FD
		public virtual void RemoveAllPlayers()
		{
			RAIL_API_PINVOKE.IRailGameServer_RemoveAllPlayers(this.swigCPtr_);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0000430A File Offset: 0x0000250A
		public virtual RailResult RegisterToGameServerList()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_RegisterToGameServerList(this.swigCPtr_);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00004317 File Offset: 0x00002517
		public virtual RailResult UnregisterFromGameServerList()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_UnregisterFromGameServerList(this.swigCPtr_);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00004324 File Offset: 0x00002524
		public virtual RailResult CloseGameServer()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_CloseGameServer(this.swigCPtr_);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00004334 File Offset: 0x00002534
		public virtual RailResult GetFriendsInGameServer(List<RailID> friend_ids)
		{
			IntPtr intPtr = ((friend_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServer_GetFriendsInGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (friend_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, friend_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00004384 File Offset: 0x00002584
		public virtual bool IsUserInGameServer(RailID user_rail_id)
		{
			IntPtr intPtr = ((user_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (user_rail_id != null)
			{
				RailConverter.Csharp2Cpp(user_rail_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_IsUserInGameServer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000043E0 File Offset: 0x000025E0
		public virtual bool SetServerInfo(string server_info)
		{
			return RAIL_API_PINVOKE.IRailGameServer_SetServerInfo(this.swigCPtr_, server_info);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000043F0 File Offset: 0x000025F0
		public virtual bool GetServerInfo(out string server_info)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailGameServer_GetServerInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				server_info = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00004434 File Offset: 0x00002634
		public virtual RailResult EnableTeamVoice(bool enable)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServer_EnableTeamVoice(this.swigCPtr_, enable);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
