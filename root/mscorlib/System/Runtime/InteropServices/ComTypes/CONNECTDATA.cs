using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000771 RID: 1905
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002BC8 RID: 11208
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002BC9 RID: 11209
		public int dwCookie;
	}
}
