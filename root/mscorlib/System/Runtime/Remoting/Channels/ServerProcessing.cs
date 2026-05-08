using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000599 RID: 1433
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		// Token: 0x0400256F RID: 9583
		Complete,
		// Token: 0x04002570 RID: 9584
		OneWay,
		// Token: 0x04002571 RID: 9585
		Async
	}
}
