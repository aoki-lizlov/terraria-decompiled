using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200056C RID: 1388
	[ComVisible(true)]
	public interface IContributeServerContextSink
	{
		// Token: 0x06003775 RID: 14197
		IMessageSink GetServerContextSink(IMessageSink nextSink);
	}
}
