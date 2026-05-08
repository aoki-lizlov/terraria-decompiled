using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000787 RID: 1927
	public struct FUNCDESC
	{
		// Token: 0x04002C13 RID: 11283
		public int memid;

		// Token: 0x04002C14 RID: 11284
		public IntPtr lprgscode;

		// Token: 0x04002C15 RID: 11285
		public IntPtr lprgelemdescParam;

		// Token: 0x04002C16 RID: 11286
		public FUNCKIND funckind;

		// Token: 0x04002C17 RID: 11287
		public INVOKEKIND invkind;

		// Token: 0x04002C18 RID: 11288
		public CALLCONV callconv;

		// Token: 0x04002C19 RID: 11289
		public short cParams;

		// Token: 0x04002C1A RID: 11290
		public short cParamsOpt;

		// Token: 0x04002C1B RID: 11291
		public short oVft;

		// Token: 0x04002C1C RID: 11292
		public short cScodes;

		// Token: 0x04002C1D RID: 11293
		public ELEMDESC elemdescFunc;

		// Token: 0x04002C1E RID: 11294
		public short wFuncFlags;
	}
}
