using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A2 RID: 1442
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06003873 RID: 14451
		Type ActivationType { get; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06003874 RID: 14452
		string ActivationTypeName { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06003875 RID: 14453
		// (set) Token: 0x06003876 RID: 14454
		IActivator Activator { get; set; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003877 RID: 14455
		object[] CallSiteActivationAttributes { get; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003878 RID: 14456
		IList ContextProperties { get; }
	}
}
