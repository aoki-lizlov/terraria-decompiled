using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ReLogic.OS;
using Terraria.Localization;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000177 RID: 375
	public class TcpSocket : ISocket
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x00503164 File Offset: 0x00501364
		private DebugNetworkStream GetStream()
		{
			if (this._debugStream == null)
			{
				return this._debugStream = new DebugNetworkStream(this._connection.GetStream());
			}
			return this._debugStream;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00503199 File Offset: 0x00501399
		public TcpSocket()
		{
			this._connection = new TcpClient
			{
				NoDelay = true
			};
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x005031B4 File Offset: 0x005013B4
		public TcpSocket(TcpClient tcpClient)
		{
			this._connection = tcpClient;
			this._connection.NoDelay = true;
			IPEndPoint ipendPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
			this._remoteAddress = new TcpAddress(ipendPoint.Address, ipendPoint.Port);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00503202 File Offset: 0x00501402
		void ISocket.Close()
		{
			this._remoteAddress = null;
			this._connection.Close();
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00503216 File Offset: 0x00501416
		bool ISocket.IsConnected()
		{
			return this._connection != null && this._connection.Client != null && this._connection.Connected;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0050323C File Offset: 0x0050143C
		void ISocket.Connect(RemoteAddress address)
		{
			TcpAddress tcpAddress = (TcpAddress)address;
			this._connection.Connect(tcpAddress.Address, tcpAddress.Port);
			this._remoteAddress = address;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00503270 File Offset: 0x00501470
		private void ReadCallback(IAsyncResult result)
		{
			try
			{
				Tuple<SocketReceiveCallback, object> tuple = (Tuple<SocketReceiveCallback, object>)result.AsyncState;
				tuple.Item1(tuple.Item2, this.GetStream().EndRead(result));
			}
			catch (ObjectDisposedException)
			{
				((ISocket)this).Close();
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x005032C4 File Offset: 0x005014C4
		private void SendCallback(IAsyncResult result)
		{
			Tuple<SocketSendCallback, object> tuple;
			if (Platform.IsWindows)
			{
				tuple = (Tuple<SocketSendCallback, object>)result.AsyncState;
			}
			else
			{
				object[] array = (object[])result.AsyncState;
				LegacyNetBufferPool.ReturnBuffer((byte[])array[1]);
				tuple = (Tuple<SocketSendCallback, object>)array[0];
			}
			try
			{
				this.GetStream().EndWrite(result);
				tuple.Item1(tuple.Item2);
			}
			catch (Exception)
			{
				((ISocket)this).Close();
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00503340 File Offset: 0x00501540
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
			if (!Platform.IsWindows)
			{
				byte[] array = LegacyNetBufferPool.RequestBuffer(data, offset, size);
				this.GetStream().BeginWrite(array, 0, size, new AsyncCallback(this.SendCallback), new object[]
				{
					new Tuple<SocketSendCallback, object>(callback, state),
					array
				});
				return;
			}
			this.GetStream().BeginWrite(data, 0, size, new AsyncCallback(this.SendCallback), new Tuple<SocketSendCallback, object>(callback, state));
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x005033B1 File Offset: 0x005015B1
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			this.GetStream().BeginRead(data, offset, size, new AsyncCallback(this.ReadCallback), new Tuple<SocketReceiveCallback, object>(callback, state));
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x005033D6 File Offset: 0x005015D6
		bool ISocket.IsDataAvailable()
		{
			return this._connection.Connected && this.GetStream().DataAvailable;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x005033F2 File Offset: 0x005015F2
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x005033FC File Offset: 0x005015FC
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			IPAddress ipaddress = IPAddress.Any;
			string text;
			if (Program.LaunchParameters.TryGetValue("-ip", out text) && !IPAddress.TryParse(text, out ipaddress))
			{
				ipaddress = IPAddress.Any;
			}
			this._isListening = true;
			this._listenerCallback = callback;
			if (this._listener == null)
			{
				this._listener = new TcpListener(ipaddress, Netplay.ListenPort);
			}
			try
			{
				this._listener.Start();
			}
			catch (Exception)
			{
				return false;
			}
			new Thread(new ThreadStart(this.ListenLoop))
			{
				IsBackground = true,
				Name = "TCP Listen Thread"
			}.Start();
			return true;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x005034A8 File Offset: 0x005016A8
		void ISocket.StopListening()
		{
			this._isListening = false;
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x005034B4 File Offset: 0x005016B4
		private void ListenLoop()
		{
			while (this._isListening && !Netplay.Disconnect)
			{
				try
				{
					ISocket socket = new TcpSocket(this._listener.AcceptTcpClient());
					Console.WriteLine(Language.GetTextValue("Net.ClientConnecting", socket.GetRemoteAddress()));
					this._listenerCallback(socket);
				}
				catch (Exception)
				{
				}
			}
			this._listener.Stop();
		}

		// Token: 0x0400168E RID: 5774
		private TcpClient _connection;

		// Token: 0x0400168F RID: 5775
		private TcpListener _listener;

		// Token: 0x04001690 RID: 5776
		private SocketConnectionAccepted _listenerCallback;

		// Token: 0x04001691 RID: 5777
		private RemoteAddress _remoteAddress;

		// Token: 0x04001692 RID: 5778
		private bool _isListening;

		// Token: 0x04001693 RID: 5779
		private DebugNetworkStream _debugStream;
	}
}
