using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000567 RID: 1383
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		// Token: 0x0600376C RID: 14188
		void CollectFromClientContext(IConstructionCallMessage msg);

		// Token: 0x0600376D RID: 14189
		void CollectFromServerContext(IConstructionReturnMessage msg);

		// Token: 0x0600376E RID: 14190
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		// Token: 0x0600376F RID: 14191
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);

		// Token: 0x06003770 RID: 14192
		bool IsOKToActivate(IConstructionCallMessage msg);
	}
}
