using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000585 RID: 1413
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600380D RID: 14349
		object ChannelData { get; }

		// Token: 0x0600380E RID: 14350
		string[] GetUrlsForUri(string objectURI);

		// Token: 0x0600380F RID: 14351
		void StartListening(object data);

		// Token: 0x06003810 RID: 14352
		void StopListening(object data);
	}
}
