using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006FD RID: 1789
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04002AE6 RID: 10982
		public int memid;

		// Token: 0x04002AE7 RID: 10983
		public string lpstrSchema;

		// Token: 0x04002AE8 RID: 10984
		public ELEMDESC elemdescVar;

		// Token: 0x04002AE9 RID: 10985
		public short wVarFlags;

		// Token: 0x04002AEA RID: 10986
		public VarEnum varkind;

		// Token: 0x020006FE RID: 1790
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04002AEB RID: 10987
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x04002AEC RID: 10988
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
