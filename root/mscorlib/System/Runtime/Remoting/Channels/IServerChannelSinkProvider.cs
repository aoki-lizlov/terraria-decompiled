using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000591 RID: 1425
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600382A RID: 14378
		// (set) Token: 0x0600382B RID: 14379
		IServerChannelSinkProvider Next { get; set; }

		// Token: 0x0600382C RID: 14380
		IServerChannelSink CreateSink(IChannelReceiver channel);

		// Token: 0x0600382D RID: 14381
		void GetChannelData(IChannelDataStore channelData);
	}
}
