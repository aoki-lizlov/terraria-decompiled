using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F4 RID: 1524
	[ComVisible(true)]
	public class InternalMessageWrapper
	{
		// Token: 0x06003AAD RID: 15021 RVA: 0x000CDD89 File Offset: 0x000CBF89
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		// Token: 0x04002616 RID: 9750
		protected IMessage WrappedMessage;
	}
}
