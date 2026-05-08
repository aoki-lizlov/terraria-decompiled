using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000785 RID: 1925
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		// Token: 0x04002BFC RID: 11260
		IMPLTYPEFLAG_FDEFAULT = 1,
		// Token: 0x04002BFD RID: 11261
		IMPLTYPEFLAG_FSOURCE = 2,
		// Token: 0x04002BFE RID: 11262
		IMPLTYPEFLAG_FRESTRICTED = 4,
		// Token: 0x04002BFF RID: 11263
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
