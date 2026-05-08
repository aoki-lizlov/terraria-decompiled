using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077E RID: 1918
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002BCC RID: 11212
		public string pwcsName;

		// Token: 0x04002BCD RID: 11213
		public int type;

		// Token: 0x04002BCE RID: 11214
		public long cbSize;

		// Token: 0x04002BCF RID: 11215
		public FILETIME mtime;

		// Token: 0x04002BD0 RID: 11216
		public FILETIME ctime;

		// Token: 0x04002BD1 RID: 11217
		public FILETIME atime;

		// Token: 0x04002BD2 RID: 11218
		public int grfMode;

		// Token: 0x04002BD3 RID: 11219
		public int grfLocksSupported;

		// Token: 0x04002BD4 RID: 11220
		public Guid clsid;

		// Token: 0x04002BD5 RID: 11221
		public int grfStateBits;

		// Token: 0x04002BD6 RID: 11222
		public int reserved;
	}
}
