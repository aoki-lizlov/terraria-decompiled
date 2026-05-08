using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000645 RID: 1605
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum StreamingContextStates
	{
		// Token: 0x0400271C RID: 10012
		CrossProcess = 1,
		// Token: 0x0400271D RID: 10013
		CrossMachine = 2,
		// Token: 0x0400271E RID: 10014
		File = 4,
		// Token: 0x0400271F RID: 10015
		Persistence = 8,
		// Token: 0x04002720 RID: 10016
		Remoting = 16,
		// Token: 0x04002721 RID: 10017
		Other = 32,
		// Token: 0x04002722 RID: 10018
		Clone = 64,
		// Token: 0x04002723 RID: 10019
		CrossAppDomain = 128,
		// Token: 0x04002724 RID: 10020
		All = 255
	}
}
