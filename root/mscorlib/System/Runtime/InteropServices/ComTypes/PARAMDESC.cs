using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200078B RID: 1931
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002C30 RID: 11312
		public IntPtr lpVarValue;

		// Token: 0x04002C31 RID: 11313
		public PARAMFLAG wParamFlags;
	}
}
