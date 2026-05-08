using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200058A RID: 1418
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600381C RID: 14364
		// (set) Token: 0x0600381D RID: 14365
		IClientChannelSinkProvider Next { get; set; }

		// Token: 0x0600381E RID: 14366
		IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);
	}
}
