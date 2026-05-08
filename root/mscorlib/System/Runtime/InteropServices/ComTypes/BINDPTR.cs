using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000781 RID: 1921
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002BDE RID: 11230
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002BDF RID: 11231
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04002BE0 RID: 11232
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
