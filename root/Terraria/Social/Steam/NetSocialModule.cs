using System;
using System.Collections.Concurrent;
using System.IO;
using Steamworks;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x0200014C RID: 332
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x06001D0B RID: 7435 RVA: 0x00500AA0 File Offset: 0x004FECA0
		protected NetSocialModule(int readChannel, int writeChannel)
		{
			this._reader = new SteamP2PReader(readChannel);
			this._writer = new SteamP2PWriter(writeChannel);
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00500AEC File Offset: 0x004FECEC
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
			this._lobbyChatMessage = Callback<LobbyChatMsg_t>.Create(new Callback<LobbyChatMsg_t>.DispatchDelegate(this.OnLobbyChatMessage));
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00500B3D File Offset: 0x004FED3D
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00500B4C File Offset: 0x004FED4C
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			CSteamID csteamID = this.RemoteAddressToSteamId(address);
			if (!this._connectionStateMap.ContainsKey(csteamID) || this._connectionStateMap[csteamID] != NetSocialModule.ConnectionState.Connected)
			{
				return false;
			}
			if (this.GetSessionState(csteamID).m_bConnectionActive != 1)
			{
				this.Close(address);
				return false;
			}
			return true;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00500BA0 File Offset: 0x004FEDA0
		protected virtual void OnLobbyChatMessage(LobbyChatMsg_t result)
		{
			if (result.m_ulSteamIDLobby != this._lobby.Id.m_SteamID)
			{
				return;
			}
			if (result.m_eChatEntryType != 1)
			{
				return;
			}
			if (result.m_ulSteamIDUser != this._lobby.Owner.m_SteamID)
			{
				return;
			}
			byte[] message = this._lobby.GetMessage((int)result.m_iChatID);
			if (message.Length == 0)
			{
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(message))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					byte b = binaryReader.ReadByte();
					if (b == 1)
					{
						while ((long)message.Length - memoryStream.Position >= 8L)
						{
							CSteamID csteamID;
							csteamID..ctor(binaryReader.ReadUInt64());
							if (csteamID != SteamUser.GetSteamID())
							{
								this._lobby.SetPlayedWith(csteamID);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00500C84 File Offset: 0x004FEE84
		protected P2PSessionState_t GetSessionState(CSteamID userId)
		{
			P2PSessionState_t p2PSessionState_t;
			SteamNetworking.GetP2PSessionState(userId, ref p2PSessionState_t);
			return p2PSessionState_t;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00500C9B File Offset: 0x004FEE9B
		protected CSteamID RemoteAddressToSteamId(RemoteAddress address)
		{
			return ((SteamAddress)address).SteamId;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00500CA8 File Offset: 0x004FEEA8
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			CSteamID csteamID = this.RemoteAddressToSteamId(address);
			this._writer.QueueSend(csteamID, data, length);
			return true;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00500CCC File Offset: 0x004FEECC
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			CSteamID csteamID = this.RemoteAddressToSteamId(address);
			return this._reader.Receive(csteamID, data, offset, length);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00500CF8 File Offset: 0x004FEEF8
		public override bool IsDataAvailable(RemoteAddress address)
		{
			CSteamID csteamID = this.RemoteAddressToSteamId(address);
			return this._reader.IsDataAvailable(csteamID);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00500D19 File Offset: 0x004FEF19
		// Note: this type is marked as 'beforefieldinit'.
		static NetSocialModule()
		{
		}

		// Token: 0x04001612 RID: 5650
		protected const int ServerReadChannel = 1;

		// Token: 0x04001613 RID: 5651
		protected const int ClientReadChannel = 2;

		// Token: 0x04001614 RID: 5652
		protected const int LobbyMessageJoin = 1;

		// Token: 0x04001615 RID: 5653
		protected const ushort GamePort = 27005;

		// Token: 0x04001616 RID: 5654
		protected const ushort SteamPort = 27006;

		// Token: 0x04001617 RID: 5655
		protected const ushort QueryPort = 27007;

		// Token: 0x04001618 RID: 5656
		protected static readonly byte[] _handshake = new byte[] { 10, 0, 93, 114, 101, 108, 111, 103, 105, 99 };

		// Token: 0x04001619 RID: 5657
		protected SteamP2PReader _reader;

		// Token: 0x0400161A RID: 5658
		protected SteamP2PWriter _writer;

		// Token: 0x0400161B RID: 5659
		protected Lobby _lobby = new Lobby();

		// Token: 0x0400161C RID: 5660
		protected ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<CSteamID, NetSocialModule.ConnectionState>();

		// Token: 0x0400161D RID: 5661
		protected object _steamLock = new object();

		// Token: 0x0400161E RID: 5662
		private Callback<LobbyChatMsg_t> _lobbyChatMessage;

		// Token: 0x0200073E RID: 1854
		public enum ConnectionState
		{
			// Token: 0x040069BA RID: 27066
			Inactive,
			// Token: 0x040069BB RID: 27067
			Authenticating,
			// Token: 0x040069BC RID: 27068
			Connected
		}

		// Token: 0x0200073F RID: 1855
		// (Invoke) Token: 0x060040B8 RID: 16568
		protected delegate void AsyncHandshake(CSteamID client);
	}
}
