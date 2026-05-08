using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D4 RID: 1748
	[Flags]
	public enum DllImportSearchPath
	{
		// Token: 0x04002A4B RID: 10827
		UseDllDirectoryForDependencies = 256,
		// Token: 0x04002A4C RID: 10828
		ApplicationDirectory = 512,
		// Token: 0x04002A4D RID: 10829
		UserDirectories = 1024,
		// Token: 0x04002A4E RID: 10830
		System32 = 2048,
		// Token: 0x04002A4F RID: 10831
		SafeDirectories = 4096,
		// Token: 0x04002A50 RID: 10832
		AssemblyDirectory = 2,
		// Token: 0x04002A51 RID: 10833
		LegacyBehavior = 0
	}
}
