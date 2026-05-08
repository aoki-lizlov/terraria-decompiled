using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F6 RID: 1782
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002ACE RID: 10958
		IDLFLAG_NONE = 0,
		// Token: 0x04002ACF RID: 10959
		IDLFLAG_FIN = 1,
		// Token: 0x04002AD0 RID: 10960
		IDLFLAG_FOUT = 2,
		// Token: 0x04002AD1 RID: 10961
		IDLFLAG_FLCID = 4,
		// Token: 0x04002AD2 RID: 10962
		IDLFLAG_FRETVAL = 8
	}
}
