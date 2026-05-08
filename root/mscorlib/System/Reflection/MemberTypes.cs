using System;

namespace System.Reflection
{
	// Token: 0x0200087B RID: 2171
	[Flags]
	public enum MemberTypes
	{
		// Token: 0x04002E3B RID: 11835
		Constructor = 1,
		// Token: 0x04002E3C RID: 11836
		Event = 2,
		// Token: 0x04002E3D RID: 11837
		Field = 4,
		// Token: 0x04002E3E RID: 11838
		Method = 8,
		// Token: 0x04002E3F RID: 11839
		Property = 16,
		// Token: 0x04002E40 RID: 11840
		TypeInfo = 32,
		// Token: 0x04002E41 RID: 11841
		Custom = 64,
		// Token: 0x04002E42 RID: 11842
		NestedType = 128,
		// Token: 0x04002E43 RID: 11843
		All = 191
	}
}
