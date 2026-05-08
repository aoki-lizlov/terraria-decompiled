using System;

namespace System.Reflection
{
	// Token: 0x020008B2 RID: 2226
	[Flags]
	[Serializable]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x04002F37 RID: 12087
		CallConvMask = 15,
		// Token: 0x04002F38 RID: 12088
		Default = 0,
		// Token: 0x04002F39 RID: 12089
		C = 1,
		// Token: 0x04002F3A RID: 12090
		StdCall = 2,
		// Token: 0x04002F3B RID: 12091
		ThisCall = 3,
		// Token: 0x04002F3C RID: 12092
		FastCall = 4,
		// Token: 0x04002F3D RID: 12093
		Vararg = 5,
		// Token: 0x04002F3E RID: 12094
		Field = 6,
		// Token: 0x04002F3F RID: 12095
		LocalSig = 7,
		// Token: 0x04002F40 RID: 12096
		Property = 8,
		// Token: 0x04002F41 RID: 12097
		Unmgd = 9,
		// Token: 0x04002F42 RID: 12098
		GenericInst = 10,
		// Token: 0x04002F43 RID: 12099
		Generic = 16,
		// Token: 0x04002F44 RID: 12100
		HasThis = 32,
		// Token: 0x04002F45 RID: 12101
		ExplicitThis = 64
	}
}
