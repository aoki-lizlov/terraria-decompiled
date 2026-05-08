using System;
using System.Collections.Concurrent;
using rail;
using Terraria.Net;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200012C RID: 300
	public abstract class NetSocialModule : NetSocialModule
	{
		// Token: 0x06001C0F RID: 7183 RVA: 0x004FD5BC File Offset: 0x004FB7BC
		protected NetSocialModule()
		{
			this._reader = new WeGameP2PReader();
			this._writer = new WeGameP2PWriter();
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x004FD5F0 File Offset: 0x004FB7F0
		public override void Initialize()
		{
			CoreSocialModule.OnTick += this._reader.ReadTick;
			CoreSocialModule.OnTick += this._writer.SendAll;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x004FD61E File Offset: 0x004FB81E
		public override void Shutdown()
		{
			this._lobby.Leave();
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x004FD62C File Offset: 0x004FB82C
		public override bool IsConnected(RemoteAddress address)
		{
			if (address == null)
			{
				return false;
			}
			RailID railID = this.RemoteAddressToRailId(address);
			return this._connectionStateMap.ContainsKey(railID) && this._connectionStateMap[railID] == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x004FD666 File Offset: 0x004FB866
		protected RailID GetLocalPeer()
		{
			return rail_api.RailFactory().RailPlayer().GetRailID();
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x004FD678 File Offset: 0x004FB878
		protected bool GetSessionState(RailID userId, RailNetworkSessionState state)
		{
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork.GetSessionState(userId, state) != null)
			{
				WeGameHelper.WriteDebugString("GetSessionState Failed user:{0}", new object[] { userId.id_ });
				return false;
			}
			return true;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x004FD6BB File Offset: 0x004FB8BB
		protected RailID RemoteAddressToRailId(RemoteAddress address)
		{
			return ((WeGameAddress)address).rail_id;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x004FD6C8 File Offset: 0x004FB8C8
		public override bool Send(RemoteAddress address, byte[] data, int length)
		{
			RailID railID = this.RemoteAddressToRailId(address);
			this._writer.QueueSend(railID, data, length);
			return true;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x004FD6EC File Offset: 0x004FB8EC
		public override int Receive(RemoteAddress address, byte[] data, int offset, int length)
		{
			if (address == null)
			{
				return 0;
			}
			RailID railID = this.RemoteAddressToRailId(address);
			return this._reader.Receive(railID, data, offset, length);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x004FD718 File Offset: 0x004FB918
		public override bool IsDataAvailable(RemoteAddress address)
		{
			RailID railID = this.RemoteAddressToRailId(address);
			return this._reader.IsDataAvailable(railID);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x004FD739 File Offset: 0x004FB939
		// Note: this type is marked as 'beforefieldinit'.
		static NetSocialModule()
		{
		}

		// Token: 0x040015AC RID: 5548
		protected const int LobbyMessageJoin = 1;

		// Token: 0x040015AD RID: 5549
		protected Lobby _lobby = new Lobby();

		// Token: 0x040015AE RID: 5550
		protected WeGameP2PReader _reader;

		// Token: 0x040015AF RID: 5551
		protected WeGameP2PWriter _writer;

		// Token: 0x040015B0 RID: 5552
		protected ConcurrentDictionary<RailID, NetSocialModule.ConnectionState> _connectionStateMap = new ConcurrentDictionary<RailID, NetSocialModule.ConnectionState>();

		// Token: 0x040015B1 RID: 5553
		protected static readonly byte[] _handshake = new byte[] { 10, 0, 93, 114, 101, 108, 111, 103, 105, 99 };

		// Token: 0x02000733 RID: 1843
		public enum ConnectionState
		{
			// Token: 0x040069A4 RID: 27044
			Inactive,
			// Token: 0x040069A5 RID: 27045
			Authenticating,
			// Token: 0x040069A6 RID: 27046
			Connected
		}
	}
}
