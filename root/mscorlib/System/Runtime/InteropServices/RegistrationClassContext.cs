using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200071D RID: 1821
	[Flags]
	public enum RegistrationClassContext
	{
		// Token: 0x04002B61 RID: 11105
		DisableActivateAsActivator = 32768,
		// Token: 0x04002B62 RID: 11106
		EnableActivateAsActivator = 65536,
		// Token: 0x04002B63 RID: 11107
		EnableCodeDownload = 8192,
		// Token: 0x04002B64 RID: 11108
		FromDefaultContext = 131072,
		// Token: 0x04002B65 RID: 11109
		InProcessHandler = 2,
		// Token: 0x04002B66 RID: 11110
		InProcessHandler16 = 32,
		// Token: 0x04002B67 RID: 11111
		InProcessServer = 1,
		// Token: 0x04002B68 RID: 11112
		InProcessServer16 = 8,
		// Token: 0x04002B69 RID: 11113
		LocalServer = 4,
		// Token: 0x04002B6A RID: 11114
		NoCodeDownload = 1024,
		// Token: 0x04002B6B RID: 11115
		NoCustomMarshal = 4096,
		// Token: 0x04002B6C RID: 11116
		NoFailureLog = 16384,
		// Token: 0x04002B6D RID: 11117
		RemoteServer = 16,
		// Token: 0x04002B6E RID: 11118
		Reserved1 = 64,
		// Token: 0x04002B6F RID: 11119
		Reserved2 = 128,
		// Token: 0x04002B70 RID: 11120
		Reserved3 = 256,
		// Token: 0x04002B71 RID: 11121
		Reserved4 = 512,
		// Token: 0x04002B72 RID: 11122
		Reserved5 = 2048
	}
}
