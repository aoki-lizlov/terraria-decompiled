using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005EC RID: 1516
	[ComVisible(true)]
	public interface IMessage
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06003A90 RID: 14992
		IDictionary Properties { get; }
	}
}
