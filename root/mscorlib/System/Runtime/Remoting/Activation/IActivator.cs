using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A1 RID: 1441
	[ComVisible(true)]
	public interface IActivator
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600386F RID: 14447
		ActivatorLevel Level { get; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06003870 RID: 14448
		// (set) Token: 0x06003871 RID: 14449
		IActivator NextActivator { get; set; }

		// Token: 0x06003872 RID: 14450
		IConstructionReturnMessage Activate(IConstructionCallMessage msg);
	}
}
