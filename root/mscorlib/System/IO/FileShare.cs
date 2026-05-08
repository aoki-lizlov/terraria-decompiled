using System;

namespace System.IO
{
	// Token: 0x02000929 RID: 2345
	[Flags]
	public enum FileShare
	{
		// Token: 0x04003329 RID: 13097
		None = 0,
		// Token: 0x0400332A RID: 13098
		Read = 1,
		// Token: 0x0400332B RID: 13099
		Write = 2,
		// Token: 0x0400332C RID: 13100
		ReadWrite = 3,
		// Token: 0x0400332D RID: 13101
		Delete = 4,
		// Token: 0x0400332E RID: 13102
		Inheritable = 16
	}
}
