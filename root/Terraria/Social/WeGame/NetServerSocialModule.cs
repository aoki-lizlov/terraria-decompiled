using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using rail;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200012B RID: 299
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x004FCBDF File Offset: 0x004FADDF
		public NetServerSocialModule()
		{
			this._lobby._lobbyCreatedExternalCallback = new Action<RailID>(this.OnLobbyCreated);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00009E46 File Offset: 0x00008046
		private void BroadcastConnectedUsers()
		{
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x004FCC20 File Offset: 0x004FAE20
		private bool AcceptAnUserSession(RailID local_peer, RailID remote_peer)
		{
			bool flag = false;
			WeGameHelper.WriteDebugString("AcceptAnUserSession server:" + local_peer.id_.ToString() + " remote:" + remote_peer.id_.ToString(), new object[0]);
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork != null)
			{
				flag = railNetwork.AcceptSessionRequest(local_peer, remote_peer) == 0;
			}
			return flag;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x004FCC7C File Offset: 0x004FAE7C
		private void TerminateRemotePlayerSession(RailID remote_id)
		{
			IRailPlayer railPlayer = rail_api.RailFactory().RailPlayer();
			if (railPlayer != null)
			{
				railPlayer.TerminateSessionOfPlayer(remote_id);
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x004FCCA0 File Offset: 0x004FAEA0
		private bool CloseNetWorkSession(RailID remote_peer)
		{
			bool flag = false;
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork != null)
			{
				flag = railNetwork.CloseSession(this._serverID, remote_peer) == 0;
			}
			return flag;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x004FCCD0 File Offset: 0x004FAED0
		private RailID GetServerID()
		{
			RailID railID = null;
			IRailGameServer server = this._lobby.GetServer();
			if (server != null)
			{
				railID = server.GetGameServerRailID();
			}
			return railID ?? new RailID();
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x004FCD00 File Offset: 0x004FAF00
		private void CloseAndUpdateUserState(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			WeGameHelper.WriteDebugString("CloseAndUpdateUserState, remote:{0}", new object[] { remote_peer.id_ });
			this.TerminateRemotePlayerSession(remote_peer);
			this.CloseNetWorkSession(remote_peer);
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Inactive;
			this._reader.ClearUser(remote_peer);
			this._writer.ClearUser(remote_peer);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x004FCD6E File Offset: 0x004FAF6E
		public void OnConnected()
		{
			this._serverConnected = true;
			if (this._ipcConnetedAction != null)
			{
				this._ipcConnetedAction();
			}
			this._ipcConnetedAction = null;
			WeGameHelper.WriteDebugString("IPC connected", new object[0]);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x004FCDA4 File Offset: 0x004FAFA4
		private void OnCreateSessionRequest(CreateSessionRequest data)
		{
			if (!this._acceptingClients)
			{
				WeGameHelper.WriteDebugString(" - Ignoring connection from " + data.remote_peer.id_ + " while _acceptionClients is false.", new object[0]);
				return;
			}
			if ((this._mode & ServerMode.FriendsOfFriends) == ServerMode.None && !this.IsWeGameFriend(data.remote_peer))
			{
				WeGameHelper.WriteDebugString("Ignoring connection from " + data.remote_peer.id_ + ". Friends of friends is disabled.", new object[0]);
				return;
			}
			WeGameHelper.WriteDebugString("pass wegame friend check", new object[0]);
			this.AcceptAnUserSession(data.local_peer, data.remote_peer);
			this._connectionStateMap[data.remote_peer] = NetSocialModule.ConnectionState.Authenticating;
			if (this._connectionAcceptedCallback != null)
			{
				this._connectionAcceptedCallback(new SocialSocket(new WeGameAddress(data.remote_peer, "")));
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x004FCE84 File Offset: 0x004FB084
		private void OnCreateSessionFailed(CreateSessionFailed data)
		{
			WeGameHelper.WriteDebugString("CreateSessionFailed, local:{0}, remote:{1}", new object[]
			{
				data.local_peer.id_,
				data.remote_peer.id_
			});
			this.CloseAndUpdateUserState(data.remote_peer);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x004FCED4 File Offset: 0x004FB0D4
		private bool GetRailFriendList(List<RailFriendInfo> list)
		{
			bool flag = false;
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				flag = railFriends.GetFriendsList(list) == 0;
			}
			return flag;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x004FCF00 File Offset: 0x004FB100
		private void OnWegameMessage(IPCMessage message)
		{
			IPCMessageType cmd = message.GetCmd();
			if (cmd == IPCMessageType.IPCMessageTypeNotifyFriendList)
			{
				WeGameFriendListInfo weGameFriendListInfo;
				message.Parse<WeGameFriendListInfo>(out weGameFriendListInfo);
				this.UpdateFriendList(weGameFriendListInfo);
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x004FCF27 File Offset: 0x004FB127
		private void UpdateFriendList(WeGameFriendListInfo friendListInfo)
		{
			this._wegameFriendList = friendListInfo._friendList;
			WeGameHelper.WriteDebugString("On update friend list - " + this.DumpFriendListString(friendListInfo._friendList), new object[0]);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x004FCF58 File Offset: 0x004FB158
		private bool IsWeGameFriend(RailID id)
		{
			bool flag = false;
			if (this._wegameFriendList != null)
			{
				using (List<RailFriendInfo>.Enumerator enumerator = this._wegameFriendList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.friend_rail_id == id)
						{
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x004FCFC0 File Offset: 0x004FB1C0
		private string DumpFriendListString(List<RailFriendInfo> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailFriendInfo railFriendInfo in list)
			{
				stringBuilder.AppendLine(string.Format("friend_id: {0}, type: {1}, online: {2}, playing: {3}", new object[]
				{
					railFriendInfo.friend_rail_id.id_,
					railFriendInfo.friend_type,
					railFriendInfo.online_state.friend_online_state.ToString(),
					railFriendInfo.online_state.game_define_game_playing_state
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x004FD078 File Offset: 0x004FB278
		private bool IsActiveUser(RailID user)
		{
			return this._connectionStateMap.ContainsKey(user) && this._connectionStateMap[user] > NetSocialModule.ConnectionState.Inactive;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x004FD09C File Offset: 0x004FB29C
		private void UpdateUserStateBySessionAuthResult(GameServerStartSessionWithPlayerResponse data)
		{
			RailID remote_rail_id = data.remote_rail_id;
			RailResult result = data.result;
			if (this._connectionStateMap.ContainsKey(remote_rail_id))
			{
				if (result == null)
				{
					WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Success", new object[0]);
					this.BroadcastConnectedUsers();
					return;
				}
				WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Failed", new object[0]);
				this.CloseAndUpdateUserState(remote_rail_id);
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x004FD0F8 File Offset: 0x004FB2F8
		private bool TryAuthUserByRecvData(RailID user, byte[] data, int length)
		{
			WeGameHelper.WriteDebugString("TryAuthUserByRecvData user:{0}", new object[] { user.id_ });
			if (length < 3)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Too short. (Length: " + length + ")", new object[0]);
				return false;
			}
			int num = ((int)data[1] << 8) | (int)data[0];
			if (num != length)
			{
				WeGameHelper.WriteDebugString(string.Concat(new object[] { "Failed to validate authentication packet: Packet size mismatch. (", num, "!=", length, ")" }), new object[0]);
				return false;
			}
			if (data[2] != 93)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Packet type is not correct. (Type: " + data[2] + ")", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x004FD1C8 File Offset: 0x004FB3C8
		private bool OnPacketRead(byte[] data, int size, RailID user)
		{
			if (!this.IsActiveUser(user))
			{
				WeGameHelper.WriteDebugString("OnPacketRead IsActiveUser false", new object[0]);
				return false;
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[user];
			if (connectionState == NetSocialModule.ConnectionState.Authenticating)
			{
				if (!this.TryAuthUserByRecvData(user, data, size))
				{
					this.CloseAndUpdateUserState(user);
				}
				else
				{
					this.OnAuthSuccess(user);
				}
				return false;
			}
			return connectionState == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x004FD224 File Offset: 0x004FB424
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Connected;
			int num = 3;
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)((num >> 8) & 255);
			array[2] = 93;
			rail_api.RailFactory().RailNetworkHelper().SendReliableData(this._serverID, remote_peer, array, (uint)num);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x004FD28C File Offset: 0x004FB48C
		public void OnRailEvent(RAILEventID event_id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + event_id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
			if (event_id == 3006)
			{
				this.UpdateUserStateBySessionAuthResult((GameServerStartSessionWithPlayerResponse)data);
				return;
			}
			if (event_id == 16001)
			{
				this.OnCreateSessionRequest((CreateSessionRequest)data);
				return;
			}
			if (event_id != 16002)
			{
				return;
			}
			this.OnCreateSessionFailed((CreateSessionFailed)data);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x004FD310 File Offset: 0x004FB510
		private void OnLobbyCreated(RailID lobbyID)
		{
			WeGameHelper.WriteDebugString("SetLocalPeer: {0}", new object[] { lobbyID.id_ });
			this._reader.SetLocalPeer(lobbyID);
			this._writer.SetLocalPeer(lobbyID);
			this._serverID = lobbyID;
			Action action = delegate
			{
				ReportServerID reportServerID = new ReportServerID
				{
					_serverID = lobbyID.id_.ToString()
				};
				IPCMessage ipcmessage = new IPCMessage();
				ipcmessage.Build<ReportServerID>(IPCMessageType.IPCMessageTypeReportServerID, reportServerID);
				WeGameHelper.WriteDebugString("Send serverID to game client - " + this._client.SendMessage(ipcmessage).ToString(), new object[0]);
			};
			if (this._serverConnected)
			{
				action();
				return;
			}
			this._ipcConnetedAction = (Action)Delegate.Combine(this._ipcConnetedAction, action);
			WeGameHelper.WriteDebugString("report server id fail, no connection", new object[0]);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x004FD3C8 File Offset: 0x004FB5C8
		private void RegisterRailEvent()
		{
			foreach (RAILEventID raileventID in new RAILEventID[] { 16001, 16002, 3006, 3005 })
			{
				this._callbackHelper.RegisterCallback(raileventID, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x004FD428 File Offset: 0x004FB628
		public override void Initialize()
		{
			base.Initialize();
			this._mode |= ServerMode.Lobby;
			this.RegisterRailEvent();
			this._reader.SetReadEvent(new WeGameP2PReader.OnReadEvent(this.OnPacketRead));
			if (Program.LaunchParameters.ContainsKey("-lobby"))
			{
				this._mode |= ServerMode.Lobby;
				string text = Program.LaunchParameters["-lobby"];
				if (!(text == "private"))
				{
					if (!(text == "friends"))
					{
						Console.WriteLine(Language.GetTextValue("Error.InvalidLobbyFlag", "private", "friends"));
					}
					else
					{
						this._mode |= ServerMode.FriendsCanJoin;
						this._lobby.Create(false);
					}
				}
				else
				{
					this._lobby.Create(true);
				}
			}
			if (Program.LaunchParameters.ContainsKey("-friendsoffriends"))
			{
				this._mode |= ServerMode.FriendsOfFriends;
			}
			this._client.Init("WeGame.Terraria.Message.Client", "WeGame.Terraria.Message.Server");
			this._client.OnConnected += this.OnConnected;
			this._client.OnMessage += this.OnWegameMessage;
			CoreSocialModule.OnTick += this._client.Tick;
			this._client.Start();
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x004FD577 File Offset: 0x004FB777
		public override ulong GetLobbyId()
		{
			return this._serverID.id_;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00009E46 File Offset: 0x00008046
		public override void CancelJoin()
		{
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00009E46 File Offset: 0x00008046
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x004FD584 File Offset: 0x004FB784
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return false;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x004FD595 File Offset: 0x004FB795
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x004FD5A0 File Offset: 0x004FB7A0
		public override void Close(RemoteAddress address)
		{
			RailID railID = base.RemoteAddressToRailId(address);
			this.CloseAndUpdateUserState(railID);
		}

		// Token: 0x040015A3 RID: 5539
		private SocketConnectionAccepted _connectionAcceptedCallback;

		// Token: 0x040015A4 RID: 5540
		private bool _acceptingClients;

		// Token: 0x040015A5 RID: 5541
		private ServerMode _mode;

		// Token: 0x040015A6 RID: 5542
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x040015A7 RID: 5543
		private MessageDispatcherClient _client = new MessageDispatcherClient();

		// Token: 0x040015A8 RID: 5544
		private bool _serverConnected;

		// Token: 0x040015A9 RID: 5545
		private RailID _serverID = new RailID();

		// Token: 0x040015AA RID: 5546
		private Action _ipcConnetedAction;

		// Token: 0x040015AB RID: 5547
		private List<RailFriendInfo> _wegameFriendList;

		// Token: 0x02000732 RID: 1842
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x060040A0 RID: 16544 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x060040A1 RID: 16545 RVA: 0x0069EB28 File Offset: 0x0069CD28
			internal void <OnLobbyCreated>b__0()
			{
				ReportServerID reportServerID = new ReportServerID
				{
					_serverID = this.lobbyID.id_.ToString()
				};
				IPCMessage ipcmessage = new IPCMessage();
				ipcmessage.Build<ReportServerID>(IPCMessageType.IPCMessageTypeReportServerID, reportServerID);
				WeGameHelper.WriteDebugString("Send serverID to game client - " + this.<>4__this._client.SendMessage(ipcmessage).ToString(), new object[0]);
			}

			// Token: 0x040069A1 RID: 27041
			public RailID lobbyID;

			// Token: 0x040069A2 RID: 27042
			public NetServerSocialModule <>4__this;
		}
	}
}
