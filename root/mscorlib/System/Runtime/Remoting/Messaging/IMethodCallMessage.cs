using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005EF RID: 1519
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06003A95 RID: 14997
		int InArgCount { get; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06003A96 RID: 14998
		object[] InArgs { get; }

		// Token: 0x06003A97 RID: 14999
		object GetInArg(int argNum);

		// Token: 0x06003A98 RID: 15000
		string GetInArgName(int index);
	}
}
