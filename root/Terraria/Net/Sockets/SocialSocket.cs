using System;
using System.Threading;
using Terraria.Social;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000176 RID: 374
	public class SocialSocket : ISocket
	{
		// Token: 0x06001E11 RID: 7697 RVA: 0x0000357B File Offset: 0x0000177B
		public SocialSocket()
		{
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00503064 File Offset: 0x00501264
		public SocialSocket(RemoteAddress remoteAddress)
		{
			this._remoteAddress = remoteAddress;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00503073 File Offset: 0x00501273
		void ISocket.Close()
		{
			if (this._remoteAddress == null)
			{
				return;
			}
			SocialAPI.Network.Close(this._remoteAddress);
			this._remoteAddress = null;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00503095 File Offset: 0x00501295
		bool ISocket.IsConnected()
		{
			return SocialAPI.Network.IsConnected(this._remoteAddress);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x005030A7 File Offset: 0x005012A7
		void ISocket.Connect(RemoteAddress address)
		{
			this._remoteAddress = address;
			SocialAPI.Network.Connect(address);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x005030BB File Offset: 0x005012BB
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
			SocialAPI.Network.Send(this._remoteAddress, data, size);
			callback.BeginInvoke(state, null, null);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x005030DC File Offset: 0x005012DC
		private void ReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			int num;
			while ((num = SocialAPI.Network.Receive(this._remoteAddress, data, offset, size)) == 0)
			{
				Thread.Sleep(1);
			}
			callback(state, num);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00503112 File Offset: 0x00501312
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			new SocialSocket.InternalReadCallback(this.ReadCallback).BeginInvoke(data, offset, size, callback, state, null, null);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0050312F File Offset: 0x0050132F
		bool ISocket.IsDataAvailable()
		{
			return SocialAPI.Network.IsDataAvailable(this._remoteAddress);
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00503141 File Offset: 0x00501341
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00503149 File Offset: 0x00501349
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			return SocialAPI.Network.StartListening(callback);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00503156 File Offset: 0x00501356
		void ISocket.StopListening()
		{
			SocialAPI.Network.StopListening();
		}

		// Token: 0x0400168D RID: 5773
		private RemoteAddress _remoteAddress;

		// Token: 0x0200074F RID: 1871
		// (Invoke) Token: 0x060040E3 RID: 16611
		private delegate void InternalReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state);
	}
}
