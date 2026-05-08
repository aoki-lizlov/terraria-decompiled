using System;

namespace NVorbis
{
	// Token: 0x02000018 RID: 24
	internal static class Utils
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005ED8 File Offset: 0x000040D8
		internal static int ilog(int x)
		{
			int num = 0;
			while (x > 0)
			{
				num++;
				x >>= 1;
			}
			return num;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005EF7 File Offset: 0x000040F7
		internal static uint BitReverse(uint n)
		{
			return Utils.BitReverse(n, 32);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005F04 File Offset: 0x00004104
		internal static uint BitReverse(uint n, int bits)
		{
			n = ((n & 2863311530U) >> 1) | ((n & 1431655765U) << 1);
			n = ((n & 3435973836U) >> 2) | ((n & 858993459U) << 2);
			n = ((n & 4042322160U) >> 4) | ((n & 252645135U) << 4);
			n = ((n & 4278255360U) >> 8) | ((n & 16711935U) << 8);
			return ((n >> 16) | (n << 16)) >> 32 - bits;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005F76 File Offset: 0x00004176
		internal static float ClipValue(float value, ref bool clipped)
		{
			if (value > 0.99999994f)
			{
				clipped = true;
				return 0.99999994f;
			}
			if (value < -0.99999994f)
			{
				clipped = true;
				return -0.99999994f;
			}
			return value;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005F9C File Offset: 0x0000419C
		internal static float ConvertFromVorbisFloat32(uint bits)
		{
			int num = (int)bits >> 31;
			double num2 = (double)(((bits & 2145386496U) >> 21) - 788U);
			return (float)(((ulong)(bits & 2097151U) ^ (ulong)((long)num)) + (ulong)((long)(num & 1))) * (float)Math.Pow(2.0, num2);
		}
	}
}
