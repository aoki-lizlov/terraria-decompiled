using System;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000175 RID: 373
	public interface ISocket
	{
		// Token: 0x06001E08 RID: 7688
		void Close();

		// Token: 0x06001E09 RID: 7689
		bool IsConnected();

		// Token: 0x06001E0A RID: 7690
		void Connect(RemoteAddress address);

		// Token: 0x06001E0B RID: 7691
		void AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state = null);

		// Token: 0x06001E0C RID: 7692
		void AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state = null);

		// Token: 0x06001E0D RID: 7693
		bool IsDataAvailable();

		// Token: 0x06001E0E RID: 7694
		bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x06001E0F RID: 7695
		void StopListening();

		// Token: 0x06001E10 RID: 7696
		RemoteAddress GetRemoteAddress();
	}
}
