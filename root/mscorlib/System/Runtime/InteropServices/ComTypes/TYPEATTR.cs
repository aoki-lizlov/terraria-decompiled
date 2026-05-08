using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000786 RID: 1926
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002C00 RID: 11264
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002C01 RID: 11265
		public Guid guid;

		// Token: 0x04002C02 RID: 11266
		public int lcid;

		// Token: 0x04002C03 RID: 11267
		public int dwReserved;

		// Token: 0x04002C04 RID: 11268
		public int memidConstructor;

		// Token: 0x04002C05 RID: 11269
		public int memidDestructor;

		// Token: 0x04002C06 RID: 11270
		public IntPtr lpstrSchema;

		// Token: 0x04002C07 RID: 11271
		public int cbSizeInstance;

		// Token: 0x04002C08 RID: 11272
		public TYPEKIND typekind;

		// Token: 0x04002C09 RID: 11273
		public short cFuncs;

		// Token: 0x04002C0A RID: 11274
		public short cVars;

		// Token: 0x04002C0B RID: 11275
		public short cImplTypes;

		// Token: 0x04002C0C RID: 11276
		public short cbSizeVft;

		// Token: 0x04002C0D RID: 11277
		public short cbAlignment;

		// Token: 0x04002C0E RID: 11278
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002C0F RID: 11279
		public short wMajorVerNum;

		// Token: 0x04002C10 RID: 11280
		public short wMinorVerNum;

		// Token: 0x04002C11 RID: 11281
		public TYPEDESC tdescAlias;

		// Token: 0x04002C12 RID: 11282
		public IDLDESC idldescType;
	}
}
