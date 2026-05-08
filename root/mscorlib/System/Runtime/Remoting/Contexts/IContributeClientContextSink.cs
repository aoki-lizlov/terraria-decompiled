using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000568 RID: 1384
	[ComVisible(true)]
	public interface IContributeClientContextSink
	{
		// Token: 0x06003771 RID: 14193
		IMessageSink GetClientContextSink(IMessageSink nextSink);
	}
}
