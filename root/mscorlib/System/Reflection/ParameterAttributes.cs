using System;

namespace System.Reflection
{
	// Token: 0x02000885 RID: 2181
	[Flags]
	public enum ParameterAttributes
	{
		// Token: 0x04002E79 RID: 11897
		None = 0,
		// Token: 0x04002E7A RID: 11898
		In = 1,
		// Token: 0x04002E7B RID: 11899
		Out = 2,
		// Token: 0x04002E7C RID: 11900
		Lcid = 4,
		// Token: 0x04002E7D RID: 11901
		Retval = 8,
		// Token: 0x04002E7E RID: 11902
		Optional = 16,
		// Token: 0x04002E7F RID: 11903
		HasDefault = 4096,
		// Token: 0x04002E80 RID: 11904
		HasFieldMarshal = 8192,
		// Token: 0x04002E81 RID: 11905
		Reserved3 = 16384,
		// Token: 0x04002E82 RID: 11906
		Reserved4 = 32768,
		// Token: 0x04002E83 RID: 11907
		ReservedMask = 61440
	}
}
