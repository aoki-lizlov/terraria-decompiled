using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006EF RID: 1775
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CONNECTDATA instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002A8D RID: 10893
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002A8E RID: 10894
		public int dwCookie;
	}
}
