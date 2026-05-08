using System;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020007A4 RID: 1956
	public enum Consistency
	{
		// Token: 0x04002C9C RID: 11420
		MayCorruptProcess,
		// Token: 0x04002C9D RID: 11421
		MayCorruptAppDomain,
		// Token: 0x04002C9E RID: 11422
		MayCorruptInstance,
		// Token: 0x04002C9F RID: 11423
		WillNotCorruptState
	}
}
