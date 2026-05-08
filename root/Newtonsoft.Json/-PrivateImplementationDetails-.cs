using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x02000102 RID: 258
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x06000C44 RID: 3140 RVA: 0x00030814 File Offset: 0x0002EA14
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261U;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s.get_Chars(i) ^ num) * 16777619U;
			}
		}
		return num;
	}

	// Token: 0x04000409 RID: 1033 RVA: 0x00080B50 File Offset: 0x0007ED50
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=6 3DE43C11C7130AF9014115BCDC2584DFE6B50579;

	// Token: 0x0400040A RID: 1034 RVA: 0x00080B58 File Offset: 0x0007ED58
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=28 9E31F24F64765FCAA589F589324D17C9FCF6A06D;

	// Token: 0x0400040B RID: 1035 RVA: 0x00080B78 File Offset: 0x0007ED78
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 ADFD2E1C801C825415DD53F4F2F72A13B389313C;

	// Token: 0x0400040C RID: 1036 RVA: 0x00080B88 File Offset: 0x0007ED88
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 D40004AB0E92BF6C8DFE481B56BE3D04ABDA76EB;

	// Token: 0x0400040D RID: 1037 RVA: 0x00080B98 File Offset: 0x0007ED98
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 DD3AEFEADB1CD615F3017763F1568179FEE640B0;

	// Token: 0x0400040E RID: 1038 RVA: 0x00080BD0 File Offset: 0x0007EDD0
	// Note: this field is marked with 'hasfieldrva'.
	internal static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 E92B39D8233061927D9ACDE54665E68E7535635A;

	// Token: 0x02000174 RID: 372
	[StructLayout(2, Pack = 1, Size = 6)]
	private struct __StaticArrayInitTypeSize=6
	{
	}

	// Token: 0x02000175 RID: 373
	[StructLayout(2, Pack = 1, Size = 10)]
	private struct __StaticArrayInitTypeSize=10
	{
	}

	// Token: 0x02000176 RID: 374
	[StructLayout(2, Pack = 1, Size = 12)]
	private struct __StaticArrayInitTypeSize=12
	{
	}

	// Token: 0x02000177 RID: 375
	[StructLayout(2, Pack = 1, Size = 28)]
	private struct __StaticArrayInitTypeSize=28
	{
	}

	// Token: 0x02000178 RID: 376
	[StructLayout(2, Pack = 1, Size = 52)]
	private struct __StaticArrayInitTypeSize=52
	{
	}
}
