using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006FF RID: 1791
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002AED RID: 10989
		public IntPtr rgvarg;

		// Token: 0x04002AEE RID: 10990
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002AEF RID: 10991
		public int cArgs;

		// Token: 0x04002AF0 RID: 10992
		public int cNamedArgs;
	}
}
