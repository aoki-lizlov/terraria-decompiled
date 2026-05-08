using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000700 RID: 1792
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002AF1 RID: 10993
		public short wCode;

		// Token: 0x04002AF2 RID: 10994
		public short wReserved;

		// Token: 0x04002AF3 RID: 10995
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002AF4 RID: 10996
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002AF5 RID: 10997
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002AF6 RID: 10998
		public int dwHelpContext;

		// Token: 0x04002AF7 RID: 10999
		public IntPtr pvReserved;

		// Token: 0x04002AF8 RID: 11000
		public IntPtr pfnDeferredFillIn;
	}
}
