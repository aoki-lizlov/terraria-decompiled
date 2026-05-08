using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000594 RID: 1428
	[ComVisible(true)]
	public interface IServerResponseChannelSinkStack
	{
		// Token: 0x06003833 RID: 14387
		void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06003834 RID: 14388
		Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
	}
}
