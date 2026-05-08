using System;

namespace System.Threading
{
	// Token: 0x0200025A RID: 602
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06001D40 RID: 7488
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x06001D41 RID: 7489
		IAsyncLocalValueMap Set(IAsyncLocal key, object value);
	}
}
