using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200079C RID: 1948
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002C8A RID: 11402
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002C8B RID: 11403
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002C8C RID: 11404
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002C8D RID: 11405
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
