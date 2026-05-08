using System;

namespace System.Reflection
{
	// Token: 0x0200086D RID: 2157
	[Flags]
	public enum ExceptionHandlingClauseOptions
	{
		// Token: 0x04002E0D RID: 11789
		Clause = 0,
		// Token: 0x04002E0E RID: 11790
		Filter = 1,
		// Token: 0x04002E0F RID: 11791
		Finally = 2,
		// Token: 0x04002E10 RID: 11792
		Fault = 4
	}
}
