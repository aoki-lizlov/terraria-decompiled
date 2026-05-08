using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000586 RID: 1414
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003811 RID: 14353
		string ChannelScheme { get; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003812 RID: 14354
		IServerChannelSink ChannelSinkChain { get; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003813 RID: 14355
		bool WantsToListen { get; }

		// Token: 0x06003814 RID: 14356
		void AddHookChannelUri(string channelUri);
	}
}
