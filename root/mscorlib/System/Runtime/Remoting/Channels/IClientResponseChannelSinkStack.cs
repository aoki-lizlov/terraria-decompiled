using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200058E RID: 1422
	[ComVisible(true)]
	public interface IClientResponseChannelSinkStack
	{
		// Token: 0x06003821 RID: 14369
		void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

		// Token: 0x06003822 RID: 14370
		void DispatchException(Exception e);

		// Token: 0x06003823 RID: 14371
		void DispatchReplyMessage(IMessage msg);
	}
}
