using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000716 RID: 1814
	[Obsolete]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002B49 RID: 11081
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002B4A RID: 11082
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002B4B RID: 11083
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002B4C RID: 11084
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
