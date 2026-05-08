using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000587 RID: 1415
	[ComVisible(true)]
	public interface IChannelSender : IChannel
	{
		// Token: 0x06003815 RID: 14357
		IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
	}
}
