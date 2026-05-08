using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000708 RID: 1800
	[Obsolete]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002B2E RID: 11054
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002B2F RID: 11055
		[FieldOffset(0)]
		public IntPtr lptcomp;

		// Token: 0x04002B30 RID: 11056
		[FieldOffset(0)]
		public IntPtr lpvardesc;
	}
}
