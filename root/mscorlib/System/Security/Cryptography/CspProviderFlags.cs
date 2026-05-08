using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000455 RID: 1109
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CspProviderFlags
	{
		// Token: 0x04002011 RID: 8209
		NoFlags = 0,
		// Token: 0x04002012 RID: 8210
		UseMachineKeyStore = 1,
		// Token: 0x04002013 RID: 8211
		UseDefaultKeyContainer = 2,
		// Token: 0x04002014 RID: 8212
		UseNonExportableKey = 4,
		// Token: 0x04002015 RID: 8213
		UseExistingKey = 8,
		// Token: 0x04002016 RID: 8214
		UseArchivableKey = 16,
		// Token: 0x04002017 RID: 8215
		UseUserProtectedKey = 32,
		// Token: 0x04002018 RID: 8216
		NoPrompt = 64,
		// Token: 0x04002019 RID: 8217
		CreateEphemeralKey = 128
	}
}
