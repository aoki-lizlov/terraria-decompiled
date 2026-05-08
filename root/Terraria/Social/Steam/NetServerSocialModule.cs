using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Steam
{
	// Token: 0x0200014B RID: 331
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x005005EB File Offset: 0x004FE7EB
		public NetServerSocialModule()
			: base(1, 2)
		{
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x005005F8 File Offset: 0x004FE7F8
		private void BroadcastConnectedUsers()
		{
			List<ulong> list = new List<ulong>();
			foreach (KeyValuePair<CSteamID, NetSocialModule.ConnectionState> keyValuePair in this._connectionStateMap)
			{
				if (keyValuePair.Value == NetSocialModule.ConnectionState.Connected)
				{
					list.Add(keyValuePair.Key.m_SteamID);
				}
			}
			byte[] array = new byte[list.Count * 8 + 1];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(1);
					foreach (ulong num in list)
					{
						binaryWriter.Write(num);
					}
				}
			}
			this._lobby.SendMessage(array);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00500708 File Offset: 0x004FE908
		public override void Initialize()
		{
			base.Initialize();
			this._reader.SetReadEvent(new SteamP2PReader.OnReadEvent(this.OnPacketRead));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
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
						this._lobby.Create(false, new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
					}
				}
				else
				{
					this._lobby.Create(true, new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
				}
			}
			if (Program.LaunchParameters.ContainsKey("-friendsoffriends"))
			{
				this._mode |= ServerMode.FriendsOfFriends;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00500811 File Offset: 0x004FEA11
		public override ulong GetLobbyId()
		{
			return this._lobby.Id.m_SteamID;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00009E46 File Offset: 0x00008046
		public override void CancelJoin()
		{
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00009E46 File Offset: 0x00008046
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00500823 File Offset: 0x004FEA23
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return true;
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00500834 File Offset: 0x004FEA34
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00500840 File Offset: 0x004FEA40
		public override void Close(RemoteAddress address)
		{
			CSteamID csteamID = base.RemoteAddressToSteamId(address);
			this.Close(csteamID);
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0050085C File Offset: 0x004FEA5C
		private void Close(CSteamID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return;
			}
			Task.Factory.StartNew(delegate
			{
				Thread.Sleep(2000);
				SteamUser.EndAuthSession(user);
				SteamNetworking.CloseP2PSessionWithUser(user);
			});
			this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
			this._reader.ClearUser(user);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x005008C4 File Offset: 0x004FEAC4
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (failure)
			{
				return;
			}
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x005008E0 File Offset: 0x004FEAE0
		private bool OnPacketRead(byte[] data, int length, CSteamID userId)
		{
			if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
			{
				P2PSessionRequest_t p2PSessionRequest_t;
				p2PSessionRequest_t.m_steamIDRemote = userId;
				this.OnP2PSessionRequest(p2PSessionRequest_t);
				if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
				{
					return false;
				}
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[userId];
			if (connectionState != NetSocialModule.ConnectionState.Authenticating)
			{
				return connectionState == NetSocialModule.ConnectionState.Connected;
			}
			if (length < 3)
			{
				return false;
			}
			if ((((int)data[1] << 8) | (int)data[0]) != length)
			{
				return false;
			}
			if (data[2] != 93)
			{
				return false;
			}
			byte[] array = new byte[data.Length - 3];
			Array.Copy(data, 3, array, 0, array.Length);
			switch (SteamUser.BeginAuthSession(array, array.Length, userId))
			{
			case 0:
				this._connectionStateMap[userId] = NetSocialModule.ConnectionState.Connected;
				this.BroadcastConnectedUsers();
				break;
			case 1:
				this.Close(userId);
				break;
			case 2:
				this.Close(userId);
				break;
			case 3:
				this.Close(userId);
				break;
			case 4:
				this.Close(userId);
				break;
			case 5:
				this.Close(userId);
				break;
			}
			return false;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x005009F0 File Offset: 0x004FEBF0
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
				return;
			}
			if (!this._acceptingClients)
			{
				return;
			}
			if ((this._mode & ServerMode.FriendsOfFriends) == ServerMode.None && SteamFriends.GetFriendRelationship(steamIDRemote) != 3 && steamIDRemote != SteamUser.GetSteamID())
			{
				return;
			}
			SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
			P2PSessionState_t p2PSessionState_t;
			while (SteamNetworking.GetP2PSessionState(steamIDRemote, ref p2PSessionState_t) && p2PSessionState_t.m_bConnecting == 1)
			{
			}
			if (p2PSessionState_t.m_bConnectionActive == 0)
			{
				this.Close(steamIDRemote);
			}
			this._connectionStateMap[steamIDRemote] = NetSocialModule.ConnectionState.Authenticating;
			this._connectionAcceptedCallback(new SocialSocket(new SteamAddress(steamIDRemote)));
		}

		// Token: 0x0400160E RID: 5646
		private ServerMode _mode;

		// Token: 0x0400160F RID: 5647
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x04001610 RID: 5648
		private bool _acceptingClients;

		// Token: 0x04001611 RID: 5649
		private SocketConnectionAccepted _connectionAcceptedCallback;

		// Token: 0x0200073D RID: 1853
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x060040B5 RID: 16565 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x060040B6 RID: 16566 RVA: 0x0069ECEC File Offset: 0x0069CEEC
			internal void <Close>b__0()
			{
				Thread.Sleep(2000);
				SteamUser.EndAuthSession(this.user);
				SteamNetworking.CloseP2PSessionWithUser(this.user);
			}

			// Token: 0x040069B8 RID: 27064
			public CSteamID user;
		}
	}
}
