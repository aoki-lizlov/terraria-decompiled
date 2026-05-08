using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Steamworks;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social.WeGame;

namespace Terraria.Social.Steam
{
	// Token: 0x0200014A RID: 330
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x004FFFCD File Offset: 0x004FE1CD
		public NetClientSocialModule()
			: base(2, 1)
		{
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x004FFFF4 File Offset: 0x004FE1F4
		public override void Initialize()
		{
			base.Initialize();
			this._gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnLobbyJoinRequest));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			this._p2pSessionConnectfail = Callback<P2PSessionConnectFail_t>.Create(new Callback<P2PSessionConnectFail_t>.DispatchDelegate(this.OnSessionConnectFail));
			Main.OnEngineLoad += this.CheckParameters;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00500060 File Offset: 0x004FE260
		private void CheckParameters()
		{
			ulong num;
			if (Program.LaunchParameters.ContainsKey("+connect_lobby") && ulong.TryParse(Program.LaunchParameters["+connect_lobby"], out num))
			{
				this.ConnectToLobby(num);
			}
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x005000A0 File Offset: 0x004FE2A0
		public void ConnectToLobby(ulong lobbyId)
		{
			CSteamID lobbySteamId = new CSteamID(lobbyId);
			if (lobbySteamId.IsValid())
			{
				Main.OpenPlayerSelectFromNet(delegate
				{
					Main.menuMode = 882;
					Main.statusText = Language.GetTextValue("Social.Joining");
					this._lobby.Join(lobbySteamId, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
				});
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x005000E4 File Offset: 0x004FE2E4
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			WeGameHelper.WriteDebugString("LaunchLocalServer", new object[0]);
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -steam -localsteamid " + SteamUser.GetSteamID().m_SteamID;
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
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
			Netplay.OnDisconnect += this.OnDisconnect;
			process.Start();
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x004E0C87 File Offset: 0x004DEE87
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x00009E46 File Offset: 0x00008046
		public override void StopListening()
		{
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x005001DC File Offset: 0x004FE3DC
		public override void Close(RemoteAddress address)
		{
			SteamFriends.ClearRichPresence();
			CSteamID csteamID = base.RemoteAddressToSteamId(address);
			this.Close(csteamID);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x005001FD File Offset: 0x004FE3FD
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00500226 File Offset: 0x004FE426
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00500234 File Offset: 0x004FE434
		private void Close(CSteamID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return;
			}
			SteamNetworking.CloseP2PSessionWithUser(user);
			this.ClearAuthTicket();
			this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
			this._lobby.Leave();
			this._reader.ClearUser(user);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00500281 File Offset: 0x004FE481
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0050029C File Offset: 0x004FE49C
		private void OnLobbyJoinRequest(GameLobbyJoinRequested_t result)
		{
			WeGameHelper.WriteDebugString(" OnLobbyJoinRequest", new object[0]);
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			string friendName = SteamFriends.GetFriendPersonaName(result.m_steamIDFriend);
			Main.OpenPlayerSelectFromNet(delegate
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.JoiningFriend", friendName);
				this._lobby.Join(result.m_steamIDLobby, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
			});
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0050030C File Offset: 0x004FE50C
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			WeGameHelper.WriteDebugString(" OnLobbyEntered", new object[0]);
			SteamNetworking.AllowP2PPacketRelay(true);
			this.SendAuthTicket(this._lobby.Owner);
			int num = 0;
			P2PSessionState_t p2PSessionState_t;
			while (SteamNetworking.GetP2PSessionState(this._lobby.Owner, ref p2PSessionState_t) && p2PSessionState_t.m_bConnectionActive != 1)
			{
				switch (p2PSessionState_t.m_eP2PSessionError)
				{
				case 1:
					this.ClearAuthTicket();
					return;
				case 2:
					this.ClearAuthTicket();
					return;
				case 3:
					this.ClearAuthTicket();
					return;
				case 4:
					if (++num > 5)
					{
						this.ClearAuthTicket();
						return;
					}
					SteamNetworking.CloseP2PSessionWithUser(this._lobby.Owner);
					this.SendAuthTicket(this._lobby.Owner);
					break;
				case 5:
					this.ClearAuthTicket();
					return;
				}
			}
			this._connectionStateMap[this._lobby.Owner] = NetSocialModule.ConnectionState.Connected;
			SteamFriends.SetPlayedWith(this._lobby.Owner);
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
			Main.clrInput();
			Netplay.ServerPassword = "";
			Main.GetInputText("", false);
			Main.autoPass = false;
			Main.netMode = 1;
			Netplay.OnConnectedToSocialServer(new SocialSocket(new SteamAddress(this._lobby.Owner)));
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00500454 File Offset: 0x004FE654
		private void SendAuthTicket(CSteamID address)
		{
			WeGameHelper.WriteDebugString(" SendAuthTicket", new object[0]);
			if (this._authTicket == HAuthTicket.Invalid)
			{
				SteamNetworkingIdentity steamNetworkingIdentity = default(SteamNetworkingIdentity);
				steamNetworkingIdentity.SetSteamID(address);
				this._authTicket = SteamUser.GetAuthSessionTicket(this._authData, this._authData.Length, ref this._authDataLength, ref steamNetworkingIdentity);
			}
			int num = (int)(this._authDataLength + 3U);
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)((num >> 8) & 255);
			array[2] = 93;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this._authDataLength))
			{
				array[num2 + 3] = this._authData[num2];
				num2++;
			}
			SteamNetworking.SendP2PPacket(address, array, (uint)num, 2, 1);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0050050C File Offset: 0x004FE70C
		private void ClearAuthTicket()
		{
			if (this._authTicket != HAuthTicket.Invalid)
			{
				SteamUser.CancelAuthTicket(this._authTicket);
			}
			this._authTicket = HAuthTicket.Invalid;
			for (int i = 0; i < this._authData.Length; i++)
			{
				this._authData[i] = 0;
			}
			this._authDataLength = 0U;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00500564 File Offset: 0x004FE764
		private void OnDisconnect()
		{
			SteamFriends.ClearRichPresence();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00500583 File Offset: 0x004FE783
		private void OnSessionConnectFail(P2PSessionConnectFail_t result)
		{
			WeGameHelper.WriteDebugString(" OnSessionConnectFail", new object[0]);
			this.Close(result.m_steamIDRemote);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x005005A4 File Offset: 0x004FE7A4
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			WeGameHelper.WriteDebugString(" OnP2PSessionRequest", new object[0]);
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
			}
		}

		// Token: 0x04001607 RID: 5639
		private Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequested;

		// Token: 0x04001608 RID: 5640
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x04001609 RID: 5641
		private Callback<P2PSessionConnectFail_t> _p2pSessionConnectfail;

		// Token: 0x0400160A RID: 5642
		private HAuthTicket _authTicket = HAuthTicket.Invalid;

		// Token: 0x0400160B RID: 5643
		private byte[] _authData = new byte[1021];

		// Token: 0x0400160C RID: 5644
		private uint _authDataLength;

		// Token: 0x0400160D RID: 5645
		private bool _hasLocalHost;

		// Token: 0x0200073B RID: 1851
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x060040B1 RID: 16561 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x060040B2 RID: 16562 RVA: 0x0069EC44 File Offset: 0x0069CE44
			internal void <ConnectToLobby>b__0()
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.Joining");
				this.<>4__this._lobby.Join(this.lobbySteamId, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.<>4__this.OnLobbyEntered));
			}

			// Token: 0x040069B3 RID: 27059
			public NetClientSocialModule <>4__this;

			// Token: 0x040069B4 RID: 27060
			public CSteamID lobbySteamId;
		}

		// Token: 0x0200073C RID: 1852
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x060040B3 RID: 16563 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x060040B4 RID: 16564 RVA: 0x0069EC94 File Offset: 0x0069CE94
			internal void <OnLobbyJoinRequest>b__0()
			{
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.JoiningFriend", this.friendName);
				this.<>4__this._lobby.Join(this.result.m_steamIDLobby, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.<>4__this.OnLobbyEntered));
			}

			// Token: 0x040069B5 RID: 27061
			public string friendName;

			// Token: 0x040069B6 RID: 27062
			public NetClientSocialModule <>4__this;

			// Token: 0x040069B7 RID: 27063
			public GameLobbyJoinRequested_t result;
		}
	}
}
