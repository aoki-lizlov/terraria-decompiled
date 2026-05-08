using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000788 RID: 1928
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002C20 RID: 11296
		IDLFLAG_NONE = 0,
		// Token: 0x04002C21 RID: 11297
		IDLFLAG_FIN = 1,
		// Token: 0x04002C22 RID: 11298
		IDLFLAG_FOUT = 2,
		// Token: 0x04002C23 RID: 11299
		IDLFLAG_FLCID = 4,
		// Token: 0x04002C24 RID: 11300
		IDLFLAG_FRETVAL = 8
	}
}
