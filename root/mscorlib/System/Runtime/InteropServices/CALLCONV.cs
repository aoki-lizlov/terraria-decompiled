using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000703 RID: 1795
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		// Token: 0x04002B05 RID: 11013
		CC_CDECL = 1,
		// Token: 0x04002B06 RID: 11014
		CC_MSCPASCAL,
		// Token: 0x04002B07 RID: 11015
		CC_PASCAL = 2,
		// Token: 0x04002B08 RID: 11016
		CC_MACPASCAL,
		// Token: 0x04002B09 RID: 11017
		CC_STDCALL,
		// Token: 0x04002B0A RID: 11018
		CC_RESERVED,
		// Token: 0x04002B0B RID: 11019
		CC_SYSCALL,
		// Token: 0x04002B0C RID: 11020
		CC_MPWCDECL,
		// Token: 0x04002B0D RID: 11021
		CC_MPWPASCAL,
		// Token: 0x04002B0E RID: 11022
		CC_MAX
	}
}
