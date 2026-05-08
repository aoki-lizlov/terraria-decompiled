using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000721 RID: 1825
	[Obsolete]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002B83 RID: 11139
		public Guid guid;

		// Token: 0x04002B84 RID: 11140
		public int lcid;

		// Token: 0x04002B85 RID: 11141
		public SYSKIND syskind;

		// Token: 0x04002B86 RID: 11142
		public short wMajorVerNum;

		// Token: 0x04002B87 RID: 11143
		public short wMinorVerNum;

		// Token: 0x04002B88 RID: 11144
		public LIBFLAGS wLibFlags;
	}
}
