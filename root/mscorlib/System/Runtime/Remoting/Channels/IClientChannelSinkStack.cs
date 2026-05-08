using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200058B RID: 1419
	[ComVisible(true)]
	public interface IClientChannelSinkStack : IClientResponseChannelSinkStack
	{
		// Token: 0x0600381F RID: 14367
		object Pop(IClientChannelSink sink);

		// Token: 0x06003820 RID: 14368
		void Push(IClientChannelSink sink, object state);
	}
}
