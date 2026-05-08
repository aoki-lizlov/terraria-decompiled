using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005EB RID: 1515
	internal interface IInternalMessage
	{
		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003A8B RID: 14987
		// (set) Token: 0x06003A8C RID: 14988
		Identity TargetIdentity { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003A8D RID: 14989
		// (set) Token: 0x06003A8E RID: 14990
		string Uri { get; set; }

		// Token: 0x06003A8F RID: 14991
		bool HasProperties();
	}
}
