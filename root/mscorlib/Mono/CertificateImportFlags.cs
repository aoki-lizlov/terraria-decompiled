using System;

namespace Mono
{
	// Token: 0x0200001F RID: 31
	[Flags]
	internal enum CertificateImportFlags
	{
		// Token: 0x04000CC0 RID: 3264
		None = 0,
		// Token: 0x04000CC1 RID: 3265
		DisableNativeBackend = 1,
		// Token: 0x04000CC2 RID: 3266
		DisableAutomaticFallback = 2
	}
}
