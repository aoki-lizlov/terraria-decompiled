using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000519 RID: 1305
	public enum ResourceType
	{
		// Token: 0x04002472 RID: 9330
		Unknown,
		// Token: 0x04002473 RID: 9331
		FileObject,
		// Token: 0x04002474 RID: 9332
		Service,
		// Token: 0x04002475 RID: 9333
		Printer,
		// Token: 0x04002476 RID: 9334
		RegistryKey,
		// Token: 0x04002477 RID: 9335
		LMShare,
		// Token: 0x04002478 RID: 9336
		KernelObject,
		// Token: 0x04002479 RID: 9337
		WindowObject,
		// Token: 0x0400247A RID: 9338
		DSObject,
		// Token: 0x0400247B RID: 9339
		DSObjectAll,
		// Token: 0x0400247C RID: 9340
		ProviderDefined,
		// Token: 0x0400247D RID: 9341
		WmiGuidObject,
		// Token: 0x0400247E RID: 9342
		RegistryWow6432Key
	}
}
