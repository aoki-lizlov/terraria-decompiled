using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F3 RID: 1779
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		// Token: 0x04002AAA RID: 10922
		IMPLTYPEFLAG_FDEFAULT = 1,
		// Token: 0x04002AAB RID: 10923
		IMPLTYPEFLAG_FSOURCE = 2,
		// Token: 0x04002AAC RID: 10924
		IMPLTYPEFLAG_FRESTRICTED = 4,
		// Token: 0x04002AAD RID: 10925
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
