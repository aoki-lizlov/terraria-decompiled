using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200056B RID: 1387
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		// Token: 0x06003774 RID: 14196
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
