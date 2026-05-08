using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F4 RID: 1780
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002AAE RID: 10926
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002AAF RID: 10927
		public Guid guid;

		// Token: 0x04002AB0 RID: 10928
		public int lcid;

		// Token: 0x04002AB1 RID: 10929
		public int dwReserved;

		// Token: 0x04002AB2 RID: 10930
		public int memidConstructor;

		// Token: 0x04002AB3 RID: 10931
		public int memidDestructor;

		// Token: 0x04002AB4 RID: 10932
		public IntPtr lpstrSchema;

		// Token: 0x04002AB5 RID: 10933
		public int cbSizeInstance;

		// Token: 0x04002AB6 RID: 10934
		public TYPEKIND typekind;

		// Token: 0x04002AB7 RID: 10935
		public short cFuncs;

		// Token: 0x04002AB8 RID: 10936
		public short cVars;

		// Token: 0x04002AB9 RID: 10937
		public short cImplTypes;

		// Token: 0x04002ABA RID: 10938
		public short cbSizeVft;

		// Token: 0x04002ABB RID: 10939
		public short cbAlignment;

		// Token: 0x04002ABC RID: 10940
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002ABD RID: 10941
		public short wMajorVerNum;

		// Token: 0x04002ABE RID: 10942
		public short wMinorVerNum;

		// Token: 0x04002ABF RID: 10943
		public TYPEDESC tdescAlias;

		// Token: 0x04002AC0 RID: 10944
		public IDLDESC idldescType;
	}
}
