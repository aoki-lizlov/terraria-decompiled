using System;

namespace System.Reflection
{
	// Token: 0x020008CE RID: 2254
	[Flags]
	internal enum PInfo
	{
		// Token: 0x04002FF2 RID: 12274
		Attributes = 1,
		// Token: 0x04002FF3 RID: 12275
		GetMethod = 2,
		// Token: 0x04002FF4 RID: 12276
		SetMethod = 4,
		// Token: 0x04002FF5 RID: 12277
		ReflectedType = 8,
		// Token: 0x04002FF6 RID: 12278
		DeclaringType = 16,
		// Token: 0x04002FF7 RID: 12279
		Name = 32
	}
}
