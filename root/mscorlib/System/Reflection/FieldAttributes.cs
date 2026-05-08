using System;

namespace System.Reflection
{
	// Token: 0x0200086E RID: 2158
	[Flags]
	public enum FieldAttributes
	{
		// Token: 0x04002E12 RID: 11794
		FieldAccessMask = 7,
		// Token: 0x04002E13 RID: 11795
		PrivateScope = 0,
		// Token: 0x04002E14 RID: 11796
		Private = 1,
		// Token: 0x04002E15 RID: 11797
		FamANDAssem = 2,
		// Token: 0x04002E16 RID: 11798
		Assembly = 3,
		// Token: 0x04002E17 RID: 11799
		Family = 4,
		// Token: 0x04002E18 RID: 11800
		FamORAssem = 5,
		// Token: 0x04002E19 RID: 11801
		Public = 6,
		// Token: 0x04002E1A RID: 11802
		Static = 16,
		// Token: 0x04002E1B RID: 11803
		InitOnly = 32,
		// Token: 0x04002E1C RID: 11804
		Literal = 64,
		// Token: 0x04002E1D RID: 11805
		NotSerialized = 128,
		// Token: 0x04002E1E RID: 11806
		SpecialName = 512,
		// Token: 0x04002E1F RID: 11807
		PinvokeImpl = 8192,
		// Token: 0x04002E20 RID: 11808
		RTSpecialName = 1024,
		// Token: 0x04002E21 RID: 11809
		HasFieldMarshal = 4096,
		// Token: 0x04002E22 RID: 11810
		HasDefault = 32768,
		// Token: 0x04002E23 RID: 11811
		HasFieldRVA = 256,
		// Token: 0x04002E24 RID: 11812
		ReservedMask = 38144
	}
}
