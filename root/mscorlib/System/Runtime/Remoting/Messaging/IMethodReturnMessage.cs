using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003AA4 RID: 15012
		Exception Exception { get; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003AA5 RID: 15013
		int OutArgCount { get; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003AA6 RID: 15014
		object[] OutArgs { get; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003AA7 RID: 15015
		object ReturnValue { get; }

		// Token: 0x06003AA8 RID: 15016
		object GetOutArg(int argNum);

		// Token: 0x06003AA9 RID: 15017
		string GetOutArgName(int index);
	}
}
