using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000920 RID: 2336
	internal static class HashHelpers
	{
		// Token: 0x06005367 RID: 21351 RVA: 0x001186AF File Offset: 0x001168AF
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x001186BD File Offset: 0x001168BD
		// Note: this type is marked as 'beforefieldinit'.
		static HashHelpers()
		{
		}

		// Token: 0x04003310 RID: 13072
		public static readonly int RandomSeed = new Random().Next(int.MinValue, int.MaxValue);
	}
}
