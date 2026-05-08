using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005EE RID: 1518
	[ComVisible(true)]
	public interface IMessageSink
	{
		// Token: 0x06003A92 RID: 14994
		IMessage SyncProcessMessage(IMessage msg);

		// Token: 0x06003A93 RID: 14995
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003A94 RID: 14996
		IMessageSink NextSink { get; }
	}
}
