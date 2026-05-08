using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000582 RID: 1410
	internal class AsyncRequest
	{
		// Token: 0x06003806 RID: 14342 RVA: 0x000CA15F File Offset: 0x000C835F
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x0400256B RID: 9579
		internal IMessageSink ReplySink;

		// Token: 0x0400256C RID: 9580
		internal IMessage MsgRequest;
	}
}
