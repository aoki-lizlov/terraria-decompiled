using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200079D RID: 1949
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002C8E RID: 11406
		public Guid guid;

		// Token: 0x04002C8F RID: 11407
		public int lcid;

		// Token: 0x04002C90 RID: 11408
		public SYSKIND syskind;

		// Token: 0x04002C91 RID: 11409
		public short wMajorVerNum;

		// Token: 0x04002C92 RID: 11410
		public short wMinorVerNum;

		// Token: 0x04002C93 RID: 11411
		public LIBFLAGS wLibFlags;
	}
}
