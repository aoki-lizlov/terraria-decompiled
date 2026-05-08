using System;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000129 RID: 297
	public class Lobby
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x004FBBBF File Offset: 0x004F9DBF
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x004FBBD1 File Offset: 0x004F9DD1
		private IRailGameServer RailServerHelper
		{
			get
			{
				if (this._gameServerInitSuccess)
				{
					return this._gameServer;
				}
				return null;
			}
			set
			{
				this._gameServer = value;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x004FBBDA File Offset: 0x004F9DDA
		public Lobby()
		{
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x004FBBED File Offset: 0x004F9DED
		private IRailGameServerHelper GetRailServerHelper()
		{
			return rail_api.RailFactory().RailGameServerHelper();
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x004FBBF9 File Offset: 0x004F9DF9
		private void RegisterGameServerEvent()
		{
			if (this._callbackHelper != null)
			{
				this._callbackHelper.RegisterCallback(3002, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x004FBC20 File Offset: 0x004F9E20
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
			if (id == 3002)
			{
				this.OnGameServerCreated((CreateGameServerResult)data);
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x004FBC79 File Offset: 0x004F9E79
		private void OnGameServerCreated(CreateGameServerResult result)
		{
			if (result.result == null)
			{
				this._gameServerInitSuccess = true;
				this._lobbyCreatedExternalCallback(result.game_server_id);
				this._server_id = result.game_server_id;
			}
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x004FBCA8 File Offset: 0x004F9EA8
		public void Create(bool inviteOnly)
		{
			if (this.State == LobbyState.Inactive)
			{
				this.RegisterGameServerEvent();
			}
			IRailGameServer railGameServer = rail_api.RailFactory().RailGameServerHelper().AsyncCreateGameServer(new CreateGameServerOptions
			{
				has_password = false,
				enable_team_voice = false
			}, "terraria", "terraria");
			this.RailServerHelper = railGameServer;
			this.State = LobbyState.Creating;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x004FBD00 File Offset: 0x004F9F00
		public void OpenInviteOverlay()
		{
			WeGameHelper.WriteDebugString("OpenInviteOverlay by wegame", new object[0]);
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x004FBD29 File Offset: 0x004F9F29
		public void Join(RailID local_peer, RailID remote_peer)
		{
			if (this.State != LobbyState.Inactive)
			{
				WeGameHelper.WriteDebugString("Lobby connection attempted while already in a lobby. This should never happen?", new object[0]);
				return;
			}
			this.State = LobbyState.Connecting;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00076333 File Offset: 0x00074533
		public byte[] GetMessage(int index)
		{
			return null;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public int GetUserCount()
		{
			return 0;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00076333 File Offset: 0x00074533
		public RailID GetUserByIndex(int index)
		{
			return null;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x004FBD4B File Offset: 0x004F9F4B
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool SendMessage(byte[] data, int length)
		{
			return false;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00009E46 File Offset: 0x00008046
		public void Set(RailID lobbyId)
		{
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00009E46 File Offset: 0x00008046
		public void SetPlayedWith(RailID userId)
		{
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x004FBD57 File Offset: 0x004F9F57
		public void Leave()
		{
			this.State = LobbyState.Inactive;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x004FBD60 File Offset: 0x004F9F60
		public IRailGameServer GetServer()
		{
			return this.RailServerHelper;
		}

		// Token: 0x04001596 RID: 5526
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x04001597 RID: 5527
		public LobbyState State;

		// Token: 0x04001598 RID: 5528
		private bool _gameServerInitSuccess;

		// Token: 0x04001599 RID: 5529
		private IRailGameServer _gameServer;

		// Token: 0x0400159A RID: 5530
		public Action<RailID> _lobbyCreatedExternalCallback;

		// Token: 0x0400159B RID: 5531
		private RailID _server_id;
	}
}
