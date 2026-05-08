using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000584 RID: 1412
	[ComVisible(true)]
	public interface IChannelDataStore
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600380A RID: 14346
		string[] ChannelUris { get; }

		// Token: 0x170007F6 RID: 2038
		object this[object key] { get; set; }
	}
}
