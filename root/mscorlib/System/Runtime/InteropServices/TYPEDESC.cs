using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006FA RID: 1786
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002AE0 RID: 10976
		public IntPtr lpValue;

		// Token: 0x04002AE1 RID: 10977
		public short vt;
	}
}
