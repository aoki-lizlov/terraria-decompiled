using System;

namespace System.Reflection
{
	// Token: 0x0200088B RID: 2187
	[Flags]
	public enum PropertyAttributes
	{
		// Token: 0x04002E9D RID: 11933
		None = 0,
		// Token: 0x04002E9E RID: 11934
		SpecialName = 512,
		// Token: 0x04002E9F RID: 11935
		RTSpecialName = 1024,
		// Token: 0x04002EA0 RID: 11936
		HasDefault = 4096,
		// Token: 0x04002EA1 RID: 11937
		Reserved2 = 8192,
		// Token: 0x04002EA2 RID: 11938
		Reserved3 = 16384,
		// Token: 0x04002EA3 RID: 11939
		Reserved4 = 32768,
		// Token: 0x04002EA4 RID: 11940
		ReservedMask = 62464
	}
}
