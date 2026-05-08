using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000597 RID: 1431
	internal class ServerDispatchSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x06003840 RID: 14400 RVA: 0x000025BE File Offset: 0x000007BE
		public ServerDispatchSink()
		{
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x00047E00 File Offset: 0x00046000
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000CA262 File Offset: 0x000C8462
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}
	}
}
