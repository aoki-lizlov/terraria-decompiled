using System;

namespace System.Reflection
{
	// Token: 0x0200085C RID: 2140
	[Flags]
	public enum AssemblyNameFlags
	{
		// Token: 0x04002DDC RID: 11740
		None = 0,
		// Token: 0x04002DDD RID: 11741
		PublicKey = 1,
		// Token: 0x04002DDE RID: 11742
		EnableJITcompileOptimizer = 16384,
		// Token: 0x04002DDF RID: 11743
		EnableJITcompileTracking = 32768,
		// Token: 0x04002DE0 RID: 11744
		Retargetable = 256
	}
}
