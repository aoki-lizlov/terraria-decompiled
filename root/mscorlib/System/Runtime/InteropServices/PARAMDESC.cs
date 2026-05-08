using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F9 RID: 1785
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002ADE RID: 10974
		public IntPtr lpVarValue;

		// Token: 0x04002ADF RID: 10975
		public PARAMFLAG wParamFlags;
	}
}
