using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000565 RID: 1381
	[ComVisible(true)]
	public interface IContextAttribute
	{
		// Token: 0x06003767 RID: 14183
		void GetPropertiesForNewContext(IConstructionCallMessage msg);

		// Token: 0x06003768 RID: 14184
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);
	}
}
