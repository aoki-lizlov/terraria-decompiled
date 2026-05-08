using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000796 RID: 1942
	[Serializable]
	public enum CALLCONV
	{
		// Token: 0x04002C5E RID: 11358
		CC_CDECL = 1,
		// Token: 0x04002C5F RID: 11359
		CC_MSCPASCAL,
		// Token: 0x04002C60 RID: 11360
		CC_PASCAL = 2,
		// Token: 0x04002C61 RID: 11361
		CC_MACPASCAL,
		// Token: 0x04002C62 RID: 11362
		CC_STDCALL,
		// Token: 0x04002C63 RID: 11363
		CC_RESERVED,
		// Token: 0x04002C64 RID: 11364
		CC_SYSCALL,
		// Token: 0x04002C65 RID: 11365
		CC_MPWCDECL,
		// Token: 0x04002C66 RID: 11366
		CC_MPWPASCAL,
		// Token: 0x04002C67 RID: 11367
		CC_MAX
	}
}
