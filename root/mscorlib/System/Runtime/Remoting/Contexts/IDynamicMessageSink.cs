using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200056D RID: 1389
	[ComVisible(true)]
	public interface IDynamicMessageSink
	{
		// Token: 0x06003776 RID: 14198
		void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);

		// Token: 0x06003777 RID: 14199
		void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);
	}
}
