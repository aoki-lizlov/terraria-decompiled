using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200071F RID: 1823
	[Obsolete]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002B74 RID: 11124
		public string pwcsName;

		// Token: 0x04002B75 RID: 11125
		public int type;

		// Token: 0x04002B76 RID: 11126
		public long cbSize;

		// Token: 0x04002B77 RID: 11127
		public FILETIME mtime;

		// Token: 0x04002B78 RID: 11128
		public FILETIME ctime;

		// Token: 0x04002B79 RID: 11129
		public FILETIME atime;

		// Token: 0x04002B7A RID: 11130
		public int grfMode;

		// Token: 0x04002B7B RID: 11131
		public int grfLocksSupported;

		// Token: 0x04002B7C RID: 11132
		public Guid clsid;

		// Token: 0x04002B7D RID: 11133
		public int grfStateBits;

		// Token: 0x04002B7E RID: 11134
		public int reserved;
	}
}
