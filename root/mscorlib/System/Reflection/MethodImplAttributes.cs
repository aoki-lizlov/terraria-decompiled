using System;

namespace System.Reflection
{
	// Token: 0x0200087E RID: 2174
	public enum MethodImplAttributes
	{
		// Token: 0x04002E5E RID: 11870
		CodeTypeMask = 3,
		// Token: 0x04002E5F RID: 11871
		IL = 0,
		// Token: 0x04002E60 RID: 11872
		Native,
		// Token: 0x04002E61 RID: 11873
		OPTIL,
		// Token: 0x04002E62 RID: 11874
		Runtime,
		// Token: 0x04002E63 RID: 11875
		ManagedMask,
		// Token: 0x04002E64 RID: 11876
		Unmanaged = 4,
		// Token: 0x04002E65 RID: 11877
		Managed = 0,
		// Token: 0x04002E66 RID: 11878
		ForwardRef = 16,
		// Token: 0x04002E67 RID: 11879
		PreserveSig = 128,
		// Token: 0x04002E68 RID: 11880
		InternalCall = 4096,
		// Token: 0x04002E69 RID: 11881
		Synchronized = 32,
		// Token: 0x04002E6A RID: 11882
		NoInlining = 8,
		// Token: 0x04002E6B RID: 11883
		AggressiveInlining = 256,
		// Token: 0x04002E6C RID: 11884
		NoOptimization = 64,
		// Token: 0x04002E6D RID: 11885
		MaxMethodImplVal = 65535
	}
}
