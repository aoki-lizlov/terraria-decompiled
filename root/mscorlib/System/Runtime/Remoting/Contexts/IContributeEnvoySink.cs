using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200056A RID: 1386
	[ComVisible(true)]
	public interface IContributeEnvoySink
	{
		// Token: 0x06003773 RID: 14195
		IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
