using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000789 RID: 1929
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002C25 RID: 11301
		public IntPtr dwReserved;

		// Token: 0x04002C26 RID: 11302
		public IDLFLAG wIDLFlags;
	}
}
