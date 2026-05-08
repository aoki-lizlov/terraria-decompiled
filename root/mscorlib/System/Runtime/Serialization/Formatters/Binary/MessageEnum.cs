using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000692 RID: 1682
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x0400295D RID: 10589
		NoArgs = 1,
		// Token: 0x0400295E RID: 10590
		ArgsInline = 2,
		// Token: 0x0400295F RID: 10591
		ArgsIsArray = 4,
		// Token: 0x04002960 RID: 10592
		ArgsInArray = 8,
		// Token: 0x04002961 RID: 10593
		NoContext = 16,
		// Token: 0x04002962 RID: 10594
		ContextInline = 32,
		// Token: 0x04002963 RID: 10595
		ContextInArray = 64,
		// Token: 0x04002964 RID: 10596
		MethodSignatureInArray = 128,
		// Token: 0x04002965 RID: 10597
		PropertyInArray = 256,
		// Token: 0x04002966 RID: 10598
		NoReturnValue = 512,
		// Token: 0x04002967 RID: 10599
		ReturnValueVoid = 1024,
		// Token: 0x04002968 RID: 10600
		ReturnValueInline = 2048,
		// Token: 0x04002969 RID: 10601
		ReturnValueInArray = 4096,
		// Token: 0x0400296A RID: 10602
		ExceptionInArray = 8192,
		// Token: 0x0400296B RID: 10603
		GenericMethod = 32768
	}
}
