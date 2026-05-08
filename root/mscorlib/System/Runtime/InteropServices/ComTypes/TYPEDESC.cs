using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200078C RID: 1932
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002C32 RID: 11314
		public IntPtr lpValue;

		// Token: 0x04002C33 RID: 11315
		public short vt;
	}
}
