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
	// Token: 0x0200012A RID: 298
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x06001BC1 RID: 7105 RVA: 0x004FBD68 File Offset: 0x004F9F68
		public NetClientSocialModule()
		{
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x004FBD9C File Offset: 0x004F9F9C
		private void OnIPCClientAccess()
		{
			WeGameHelper.WriteDebugString("IPC client access", new object[0]);
			this.SendFriendListToLocalServer();
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x004FBDB8 File Offset: 0x004F9FB8
		private void LazyCreateWeGameMsgServer()
		{
			if (this._msgServer == null)
			{
				this._msgServer = new MessageDispatcherServer();
				this._msgServer.Init("WeGame.Terraria.Message.Server");
				this._msgServer.OnMessage += this.OnWegameMessage;
				this._msgServer.OnIPCClientAccess += this.OnIPCClientAccess;
				CoreSocialModule.OnTick += this._msgServer.Tick;
				this._msgServer.Start();
			}
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x004FBE38 File Offset: 0x004FA038
		private void OnWegameMessage(IPCMessage message)
		{
			if (message.GetCmd() == IPCMessageType.IPCMessageTypeReportServerID)
			{
				ReportServerID reportServerID;
				message.Parse<ReportServerID>(out reportServerID);
				this.OnReportServerID(reportServerID);
			}
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x004FBE5E File Offset: 0x004FA05E
		private void OnReportServerID(ReportServerID reportServerID)
		{
			WeGameHelper.WriteDebugString("OnReportServerID - " + reportServerID._serverID, new object[0]);
			this.AsyncSetMyMetaData(this._serverIDMedataKey, reportServerID._serverID);
			this.AsyncSetInviteCommandLine(reportServerID._serverID);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x004FBE9C File Offset: 0x004FA09C
		public override void Initialize()
		{
			base.Initialize();
			this.RegisterRailEvent();
			this.AsyncGetFriendsInfo();
			this._reader.SetReadEvent(new WeGameP2PReader.OnReadEvent(this.OnPacketRead));
			this._reader.SetLocalPeer(base.GetLocalPeer());
			this._writer.SetLocalPeer(base.GetLocalPeer());
			Main.OnEngineLoad += this.CheckParameters;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x004FBF05 File Offset: 0x004FA105
		private void AsyncSetInviteCommandLine(string cmdline)
		{
			rail_api.RailFactory().RailFriends().AsyncSetInviteCommandLine(cmdline, "");
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x004FBF20 File Offset: 0x004FA120
		private void AsyncSetMyMetaData(string key, string value)
		{
			List<RailKeyValue> list = new List<RailKeyValue>();
			list.Add(new RailKeyValue
			{
				key = key,
				value = value
			});
			rail_api.RailFactory().RailFriends().AsyncSetMyMetadata(list, "");
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x004FBF64 File Offset: 0x004FA164
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

		// Token: 0x06001BCA RID: 7114 RVA: 0x004FC034 File Offset: 0x004FA234
		private bool OnPacketRead(byte[] data, int size, RailID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return false;
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[user];
			if (connectionState == NetSocialModule.ConnectionState.Authenticating)
			{
				if (!this.TryAuthUserByRecvData(user, data, size))
				{
					WeGameHelper.WriteDebugString(" Auth Server Ticket Failed", new object[0]);
					this.Close(user);
				}
				else
				{
					WeGameHelper.WriteDebugString("OnRailAuthSessionTicket Auth Success..", new object[0]);
					this.OnAuthSuccess(user);
				}
				return false;
			}
			return connectionState == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x004FC0A4 File Offset: 0x004FA2A4
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Connected;
			this.AsyncSetPlayWith(this._inviter_id);
			this.AsyncSetMyMetaData("status", Language.GetTextValue("Social.StatusInGame"));
			this.AsyncSetMyMetaData(this._serverIDMedataKey, remote_peer.id_.ToString());
			Main.clrInput();
			Netplay.ServerPassword = "";
			Main.GetInputText("", false);
			Main.autoPass = false;
			Main.netMode = 1;
			Netplay.OnConnectedToSocialServer(new SocialSocket(new WeGameAddress(remote_peer, this.GetFriendNickname(this._inviter_id))));
			WeGameHelper.WriteDebugString("OnConnectToSocialServer server:" + remote_peer.id_.ToString(), new object[0]);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x004FC168 File Offset: 0x004FA368
		private bool GetRailConnectIDFromCmdLine(RailID server_id)
		{
			foreach (string text in Environment.GetCommandLineArgs())
			{
				string text2 = "--rail_connect_cmd=";
				int num = text.IndexOf(text2);
				if (num != -1)
				{
					ulong num2 = 0UL;
					if (ulong.TryParse(text.Substring(num + text2.Length), out num2))
					{
						server_id.id_ = num2;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x004FC1C8 File Offset: 0x004FA3C8
		private void CheckParameters()
		{
			RailID server_id = new RailID();
			if (this.GetRailConnectIDFromCmdLine(server_id))
			{
				if (server_id.IsValid())
				{
					Main.OpenPlayerSelectFromNet(delegate
					{
						Main.menuMode = 882;
						Main.statusText = Language.GetTextValue("Social.Joining");
						WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", new object[0]);
						this.JoinServer(server_id);
					});
					return;
				}
				WeGameHelper.WriteDebugString("Invalid RailID passed to +connect_lobby", new object[0]);
			}
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x004FC22C File Offset: 0x004FA42C
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			this.LazyCreateWeGameMsgServer();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -wegame -localwegameid " + base.GetLocalPeer().id_;
			if ((mode & ServerMode.Lobby) != ServerMode.None)
			{
				this._hasLocalHost = true;
				if ((mode & ServerMode.FriendsCanJoin) != ServerMode.None)
				{
					ProcessStartInfo startInfo2 = process.StartInfo;
					startInfo2.Arguments += " -lobby friends";
				}
				else
				{
					ProcessStartInfo startInfo3 = process.StartInfo;
					startInfo3.Arguments += " -lobby private";
				}
				if ((mode & ServerMode.FriendsOfFriends) != ServerMode.None)
				{
					ProcessStartInfo startInfo4 = process.StartInfo;
					startInfo4.Arguments += " -friendsoffriends";
				}
			}
			string text;
			rail_api.RailFactory().RailUtils().GetLaunchAppParameters(2, ref text);
			ProcessStartInfo startInfo5 = process.StartInfo;
			startInfo5.Arguments = startInfo5.Arguments + " " + text;
			WeGameHelper.WriteDebugString("LaunchLocalServer,cmd_line:" + process.StartInfo.Arguments, new object[0]);
			this.AsyncSetMyMetaData("status", Language.GetTextValue("Social.StatusInGame"));
			Netplay.OnDisconnect += this.OnDisconnect;
			process.Start();
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x004FC367 File Offset: 0x004FA567
		public override void Shutdown()
		{
			this.AsyncSetInviteCommandLine("");
			this.CleanMyMetaData();
			this.UnRegisterRailEvent();
			base.Shutdown();
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x004E0C87 File Offset: 0x004DEE87
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00009E46 File Offset: 0x00008046
		public override void StopListening()
		{
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x004FC388 File Offset: 0x004FA588
		public override void Close(RemoteAddress address)
		{
			this.CleanMyMetaData();
			RailID railID = base.RemoteAddressToRailId(address);
			this.Close(railID);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x004FC3AA File Offset: 0x004FA5AA
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x004FC3D3 File Offset: 0x004FA5D3
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x004FC3E0 File Offset: 0x004FA5E0
		private void Close(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			WeGameHelper.WriteDebugString("CloseRemotePeer, remote:{0}", new object[] { remote_peer.id_ });
			rail_api.RailFactory().RailNetworkHelper().CloseSession(base.GetLocalPeer(), remote_peer);
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Inactive;
			this._lobby.Leave();
			this._reader.ClearUser(remote_peer);
			this._writer.ClearUser(remote_peer);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x004FC461 File Offset: 0x004FA661
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x004FC47C File Offset: 0x004FA67C
		private void RegisterRailEvent()
		{
			foreach (RAILEventID raileventID in new RAILEventID[] { 16001, 16002, 13503, 13501, 12003, 12002, 12010 })
			{
				this._callbackHelper.RegisterCallback(raileventID, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x004FC4F2 File Offset: 0x004FA6F2
		private void UnRegisterRailEvent()
		{
			this._callbackHelper.UnregisterAllCallback();
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x004FC500 File Offset: 0x004FA700
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
			if (id <= 12010)
			{
				if (id == 12002)
				{
					this.OnRailSetMetaData((RailFriendsSetMetadataResult)data);
					return;
				}
				if (id == 12003)
				{
					this.OnGetFriendMetaData((RailFriendsGetMetadataResult)data);
					return;
				}
				if (id != 12010)
				{
					return;
				}
				this.OnFriendlistChange((RailFriendsListChanged)data);
				return;
			}
			else if (id <= 13503)
			{
				if (id == 13501)
				{
					this.OnRailGetUsersInfo((RailUsersInfoData)data);
					return;
				}
				if (id != 13503)
				{
					return;
				}
				this.OnRailRespondInvation((RailUsersRespondInvitation)data);
				return;
			}
			else
			{
				if (id == 16001)
				{
					this.OnRailCreateSessionRequest((CreateSessionRequest)data);
					return;
				}
				if (id != 16002)
				{
					return;
				}
				this.OnRailCreateSessionFailed((CreateSessionFailed)data);
				return;
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x004FC5EC File Offset: 0x004FA7EC
		private string DumpMataDataString(List<RailKeyValueResult> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailKeyValueResult railKeyValueResult in list)
			{
				stringBuilder.Append("key: " + railKeyValueResult.key + " value: " + railKeyValueResult.value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x004FC664 File Offset: 0x004FA864
		private string GetValueByKey(string key, List<RailKeyValueResult> list)
		{
			string text = null;
			foreach (RailKeyValueResult railKeyValueResult in list)
			{
				if (railKeyValueResult.key == key)
				{
					text = railKeyValueResult.value;
					break;
				}
			}
			return text;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x004FC6C8 File Offset: 0x004FA8C8
		private bool SendFriendListToLocalServer()
		{
			bool flag = false;
			if (this._hasLocalHost)
			{
				List<RailFriendInfo> list = new List<RailFriendInfo>();
				if (this.GetRailFriendList(list))
				{
					WeGameFriendListInfo weGameFriendListInfo = new WeGameFriendListInfo
					{
						_friendList = list
					};
					IPCMessage ipcmessage = new IPCMessage();
					ipcmessage.Build<WeGameFriendListInfo>(IPCMessageType.IPCMessageTypeNotifyFriendList, weGameFriendListInfo);
					flag = this._msgServer.SendMessage(ipcmessage);
					WeGameHelper.WriteDebugString("NotifyFriendListToServer: " + flag.ToString(), new object[0]);
				}
			}
			return flag;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x004FC734 File Offset: 0x004FA934
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

		// Token: 0x06001BE0 RID: 7136 RVA: 0x004FC760 File Offset: 0x004FA960
		private void OnGetFriendMetaData(RailFriendsGetMetadataResult data)
		{
			if (data.result == null && data.friend_kvs.Count > 0)
			{
				WeGameHelper.WriteDebugString("OnGetFriendMetaData - " + this.DumpMataDataString(data.friend_kvs), new object[0]);
				string valueByKey = this.GetValueByKey(this._serverIDMedataKey, data.friend_kvs);
				if (valueByKey != null)
				{
					if (valueByKey.Length > 0)
					{
						RailID railID = new RailID();
						railID.id_ = ulong.Parse(valueByKey);
						if (railID.IsValid())
						{
							this.JoinServer(railID);
							return;
						}
						WeGameHelper.WriteDebugString("JoinServer failed, invalid server id", new object[0]);
						return;
					}
					else
					{
						WeGameHelper.WriteDebugString("can not find server id key", new object[0]);
					}
				}
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x004FC80C File Offset: 0x004FAA0C
		private void JoinServer(RailID server_id)
		{
			WeGameHelper.WriteDebugString("JoinServer:{0}", new object[] { server_id.id_ });
			this._connectionStateMap[server_id] = NetSocialModule.ConnectionState.Authenticating;
			int num = 3;
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)((num >> 8) & 255);
			array[2] = 93;
			rail_api.RailFactory().RailNetworkHelper().SendReliableData(base.GetLocalPeer(), server_id, array, (uint)num);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x004FC884 File Offset: 0x004FAA84
		private string GetFriendNickname(RailID rail_id)
		{
			if (this._player_info_list != null)
			{
				foreach (PlayerPersonalInfo playerPersonalInfo in this._player_info_list)
				{
					if (playerPersonalInfo.rail_id == rail_id)
					{
						return playerPersonalInfo.rail_name;
					}
				}
			}
			return "";
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x004FC8F8 File Offset: 0x004FAAF8
		private void OnRailGetUsersInfo(RailUsersInfoData data)
		{
			this._player_info_list = data.user_info_list;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x004FC906 File Offset: 0x004FAB06
		private void OnFriendlistChange(RailFriendsListChanged data)
		{
			if (this._hasLocalHost)
			{
				this.SendFriendListToLocalServer();
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x004FC918 File Offset: 0x004FAB18
		private void AsyncGetFriendsInfo()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				List<RailFriendInfo> list = new List<RailFriendInfo>();
				railFriends.GetFriendsList(list);
				List<RailID> list2 = new List<RailID>();
				foreach (RailFriendInfo railFriendInfo in list)
				{
					list2.Add(railFriendInfo.friend_rail_id);
				}
				railFriends.AsyncGetPersonalInfo(list2, "");
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x004FC99C File Offset: 0x004FAB9C
		private void AsyncSetPlayWith(RailID rail_id)
		{
			List<RailUserPlayedWith> list = new List<RailUserPlayedWith>();
			list.Add(new RailUserPlayedWith
			{
				rail_id = rail_id
			});
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncReportPlayedWithUserList(list, "");
			}
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x004FC9DE File Offset: 0x004FABDE
		private void OnRailSetMetaData(RailFriendsSetMetadataResult data)
		{
			WeGameHelper.WriteDebugString("OnRailSetMetaData - " + data.result.ToString(), new object[0]);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x004FCA08 File Offset: 0x004FAC08
		private void OnRailRespondInvation(RailUsersRespondInvitation data)
		{
			WeGameHelper.WriteDebugString(" request join game", new object[0]);
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			this._inviter_id = data.inviter_id;
			Main.OpenPlayerSelectFromNet(delegate
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.JoiningFriend", this.GetFriendNickname(data.inviter_id));
				this.AsyncGetServerIDByOwener(data.inviter_id);
				WeGameHelper.WriteDebugString("inviter_id: " + data.inviter_id.id_, new object[0]);
			});
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x004FCA74 File Offset: 0x004FAC74
		private void AsyncGetServerIDByOwener(RailID ownerID)
		{
			List<string> list = new List<string>();
			list.Add(this._serverIDMedataKey);
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncGetFriendMetadata(ownerID, list, "");
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x004FCAB0 File Offset: 0x004FACB0
		private void OnRailCreateSessionRequest(CreateSessionRequest result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionRequest", new object[0]);
			if (this._connectionStateMap.ContainsKey(result.remote_peer) && this._connectionStateMap[result.remote_peer] != NetSocialModule.ConnectionState.Inactive)
			{
				WeGameHelper.WriteDebugString("AcceptSessionRequest, local{0}, remote:{1}", new object[]
				{
					result.local_peer.id_,
					result.remote_peer.id_
				});
				rail_api.RailFactory().RailNetworkHelper().AcceptSessionRequest(result.local_peer, result.remote_peer);
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x004FCB48 File Offset: 0x004FAD48
		private void OnRailCreateSessionFailed(CreateSessionFailed result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionFailed, CloseRemote: local:{0}, remote:{1}", new object[]
			{
				result.local_peer.id_,
				result.remote_peer.id_
			});
			this.Close(result.remote_peer);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x004FCB98 File Offset: 0x004FAD98
		private void CleanMyMetaData()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncClearAllMyMetadata("");
			}
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x004FCBBF File Offset: 0x004FADBF
		private void OnDisconnect()
		{
			this.CleanMyMetaData();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x0400159C RID: 5532
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x0400159D RID: 5533
		private bool _hasLocalHost;

		// Token: 0x0400159E RID: 5534
		private IPCServer server = new IPCServer();

		// Token: 0x0400159F RID: 5535
		private readonly string _serverIDMedataKey = "terraria.serverid";

		// Token: 0x040015A0 RID: 5536
		private RailID _inviter_id = new RailID();

		// Token: 0x040015A1 RID: 5537
		private List<PlayerPersonalInfo> _player_info_list;

		// Token: 0x040015A2 RID: 5538
		private MessageDispatcherServer _msgServer;

		// Token: 0x02000730 RID: 1840
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x0600409C RID: 16540 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x0600409D RID: 16541 RVA: 0x0069EA70 File Offset: 0x0069CC70
			internal void <CheckParameters>b__0()
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.Joining");
				WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", new object[0]);
				this.<>4__this.JoinServer(this.server_id);
			}

			// Token: 0x0400699D RID: 27037
			public NetClientSocialModule <>4__this;

			// Token: 0x0400699E RID: 27038
			public RailID server_id;
		}

		// Token: 0x02000731 RID: 1841
		[CompilerGenerated]
		private sealed class <>c__DisplayClass46_0
		{
			// Token: 0x0600409E RID: 16542 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass46_0()
			{
			}

			// Token: 0x0600409F RID: 16543 RVA: 0x0069EAAC File Offset: 0x0069CCAC
			internal void <OnRailRespondInvation>b__0()
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.JoiningFriend", this.<>4__this.GetFriendNickname(this.data.inviter_id));
				this.<>4__this.AsyncGetServerIDByOwener(this.data.inviter_id);
				WeGameHelper.WriteDebugString("inviter_id: " + this.data.inviter_id.id_, new object[0]);
			}

			// Token: 0x0400699F RID: 27039
			public NetClientSocialModule <>4__this;

			// Token: 0x040069A0 RID: 27040
			public RailUsersRespondInvitation data;
		}
	}
}
