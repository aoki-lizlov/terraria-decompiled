using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000790 RID: 1936
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04002C3D RID: 11325
		public int memid;

		// Token: 0x04002C3E RID: 11326
		public string lpstrSchema;

		// Token: 0x04002C3F RID: 11327
		public VARDESC.DESCUNION desc;

		// Token: 0x04002C40 RID: 11328
		public ELEMDESC elemdescVar;

		// Token: 0x04002C41 RID: 11329
		public short wVarFlags;

		// Token: 0x04002C42 RID: 11330
		public VARKIND varkind;

		// Token: 0x02000791 RID: 1937
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04002C43 RID: 11331
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x04002C44 RID: 11332
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
