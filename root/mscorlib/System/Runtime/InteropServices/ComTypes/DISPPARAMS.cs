using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000792 RID: 1938
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002C45 RID: 11333
		public IntPtr rgvarg;

		// Token: 0x04002C46 RID: 11334
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002C47 RID: 11335
		public int cArgs;

		// Token: 0x04002C48 RID: 11336
		public int cNamedArgs;
	}
}
