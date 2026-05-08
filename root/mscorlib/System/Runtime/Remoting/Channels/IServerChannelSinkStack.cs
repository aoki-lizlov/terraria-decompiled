using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000592 RID: 1426
	[ComVisible(true)]
	public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
	{
		// Token: 0x0600382E RID: 14382
		object Pop(IServerChannelSink sink);

		// Token: 0x0600382F RID: 14383
		void Push(IServerChannelSink sink, object state);

		// Token: 0x06003830 RID: 14384
		void ServerCallback(IAsyncResult ar);

		// Token: 0x06003831 RID: 14385
		void Store(IServerChannelSink sink, object state);

		// Token: 0x06003832 RID: 14386
		void StoreAndDispatch(IServerChannelSink sink, object state);
	}
}
