using System;

namespace System.Reflection
{
	// Token: 0x02000889 RID: 2185
	[Flags]
	public enum PortableExecutableKinds
	{
		// Token: 0x04002E8F RID: 11919
		NotAPortableExecutableImage = 0,
		// Token: 0x04002E90 RID: 11920
		ILOnly = 1,
		// Token: 0x04002E91 RID: 11921
		Required32Bit = 2,
		// Token: 0x04002E92 RID: 11922
		PE32Plus = 4,
		// Token: 0x04002E93 RID: 11923
		Unmanaged32Bit = 8,
		// Token: 0x04002E94 RID: 11924
		Preferred32Bit = 16
	}
}
