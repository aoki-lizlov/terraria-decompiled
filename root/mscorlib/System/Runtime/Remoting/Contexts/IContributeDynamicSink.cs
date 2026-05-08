using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000569 RID: 1385
	[ComVisible(true)]
	public interface IContributeDynamicSink
	{
		// Token: 0x06003772 RID: 14194
		IDynamicMessageSink GetDynamicSink();
	}
}
