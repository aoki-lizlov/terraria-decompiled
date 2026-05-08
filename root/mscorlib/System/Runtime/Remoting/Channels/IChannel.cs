using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000583 RID: 1411
	[ComVisible(true)]
	public interface IChannel
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003807 RID: 14343
		string ChannelName { get; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003808 RID: 14344
		int ChannelPriority { get; }

		// Token: 0x06003809 RID: 14345
		string Parse(string url, out string objectURI);
	}
}
