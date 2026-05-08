using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000589 RID: 1417
	[ComVisible(true)]
	public interface IClientChannelSink : IChannelSinkBase
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003817 RID: 14359
		IClientChannelSink NextChannelSink { get; }

		// Token: 0x06003818 RID: 14360
		void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06003819 RID: 14361
		void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

		// Token: 0x0600381A RID: 14362
		Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

		// Token: 0x0600381B RID: 14363
		void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);
	}
}
