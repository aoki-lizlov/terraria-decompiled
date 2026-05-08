using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006E4 RID: 1764
	[ComVisible(true)]
	[Serializable]
	public enum CallingConvention
	{
		// Token: 0x04002A75 RID: 10869
		Winapi = 1,
		// Token: 0x04002A76 RID: 10870
		Cdecl,
		// Token: 0x04002A77 RID: 10871
		StdCall,
		// Token: 0x04002A78 RID: 10872
		ThisCall,
		// Token: 0x04002A79 RID: 10873
		FastCall
	}
}
