using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200078A RID: 1930
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002C28 RID: 11304
		PARAMFLAG_NONE = 0,
		// Token: 0x04002C29 RID: 11305
		PARAMFLAG_FIN = 1,
		// Token: 0x04002C2A RID: 11306
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002C2B RID: 11307
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002C2C RID: 11308
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002C2D RID: 11309
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002C2E RID: 11310
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002C2F RID: 11311
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
