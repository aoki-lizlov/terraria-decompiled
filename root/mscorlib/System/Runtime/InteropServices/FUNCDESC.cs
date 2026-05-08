using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F5 RID: 1781
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FUNCDESC
	{
		// Token: 0x04002AC1 RID: 10945
		public int memid;

		// Token: 0x04002AC2 RID: 10946
		public IntPtr lprgscode;

		// Token: 0x04002AC3 RID: 10947
		public IntPtr lprgelemdescParam;

		// Token: 0x04002AC4 RID: 10948
		public FUNCKIND funckind;

		// Token: 0x04002AC5 RID: 10949
		public INVOKEKIND invkind;

		// Token: 0x04002AC6 RID: 10950
		public CALLCONV callconv;

		// Token: 0x04002AC7 RID: 10951
		public short cParams;

		// Token: 0x04002AC8 RID: 10952
		public short cParamsOpt;

		// Token: 0x04002AC9 RID: 10953
		public short oVft;

		// Token: 0x04002ACA RID: 10954
		public short cScodes;

		// Token: 0x04002ACB RID: 10955
		public ELEMDESC elemdescFunc;

		// Token: 0x04002ACC RID: 10956
		public short wFuncFlags;
	}
}
