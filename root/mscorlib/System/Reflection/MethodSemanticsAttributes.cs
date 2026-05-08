using System;

namespace System.Reflection
{
	// Token: 0x020008B4 RID: 2228
	[Flags]
	[Serializable]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x04002F5E RID: 12126
		Setter = 1,
		// Token: 0x04002F5F RID: 12127
		Getter = 2,
		// Token: 0x04002F60 RID: 12128
		Other = 4,
		// Token: 0x04002F61 RID: 12129
		AddOn = 8,
		// Token: 0x04002F62 RID: 12130
		RemoveOn = 16,
		// Token: 0x04002F63 RID: 12131
		Fire = 32
	}
}
