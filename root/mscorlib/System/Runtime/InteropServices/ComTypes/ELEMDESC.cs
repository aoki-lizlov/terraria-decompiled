using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200078D RID: 1933
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		// Token: 0x04002C34 RID: 11316
		public TYPEDESC tdesc;

		// Token: 0x04002C35 RID: 11317
		public ELEMDESC.DESCUNION desc;

		// Token: 0x0200078E RID: 1934
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04002C36 RID: 11318
			[FieldOffset(0)]
			public IDLDESC idldesc;

			// Token: 0x04002C37 RID: 11319
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
