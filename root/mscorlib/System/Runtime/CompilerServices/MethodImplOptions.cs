using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000813 RID: 2067
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplOptions
	{
		// Token: 0x04002D00 RID: 11520
		Unmanaged = 4,
		// Token: 0x04002D01 RID: 11521
		ForwardRef = 16,
		// Token: 0x04002D02 RID: 11522
		PreserveSig = 128,
		// Token: 0x04002D03 RID: 11523
		InternalCall = 4096,
		// Token: 0x04002D04 RID: 11524
		Synchronized = 32,
		// Token: 0x04002D05 RID: 11525
		NoInlining = 8,
		// Token: 0x04002D06 RID: 11526
		[ComVisible(false)]
		AggressiveInlining = 256,
		// Token: 0x04002D07 RID: 11527
		NoOptimization = 64
	}
}
