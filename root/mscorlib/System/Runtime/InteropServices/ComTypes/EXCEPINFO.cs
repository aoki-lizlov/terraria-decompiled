using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000793 RID: 1939
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002C49 RID: 11337
		public short wCode;

		// Token: 0x04002C4A RID: 11338
		public short wReserved;

		// Token: 0x04002C4B RID: 11339
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002C4C RID: 11340
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002C4D RID: 11341
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002C4E RID: 11342
		public int dwHelpContext;

		// Token: 0x04002C4F RID: 11343
		public IntPtr pvReserved;

		// Token: 0x04002C50 RID: 11344
		public IntPtr pfnDeferredFillIn;

		// Token: 0x04002C51 RID: 11345
		public int scode;
	}
}
