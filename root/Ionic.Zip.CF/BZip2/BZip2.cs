using System;

namespace Ionic.BZip2
{
	// Token: 0x02000036 RID: 54
	internal static class BZip2
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00011470 File Offset: 0x0000F670
		internal static T[][] InitRectangularArray<T>(int d1, int d2)
		{
			T[][] array = new T[d1][];
			for (int i = 0; i < d1; i++)
			{
				array[i] = new T[d2];
			}
			return array;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001149C File Offset: 0x0000F69C
		// Note: this type is marked as 'beforefieldinit'.
		static BZip2()
		{
		}

		// Token: 0x040001AA RID: 426
		public static readonly int BlockSizeMultiple = 100000;

		// Token: 0x040001AB RID: 427
		public static readonly int MinBlockSize = 1;

		// Token: 0x040001AC RID: 428
		public static readonly int MaxBlockSize = 9;

		// Token: 0x040001AD RID: 429
		public static readonly int MaxAlphaSize = 258;

		// Token: 0x040001AE RID: 430
		public static readonly int MaxCodeLength = 23;

		// Token: 0x040001AF RID: 431
		public static readonly char RUNA = '\0';

		// Token: 0x040001B0 RID: 432
		public static readonly char RUNB = '\u0001';

		// Token: 0x040001B1 RID: 433
		public static readonly int NGroups = 6;

		// Token: 0x040001B2 RID: 434
		public static readonly int G_SIZE = 50;

		// Token: 0x040001B3 RID: 435
		public static readonly int N_ITERS = 4;

		// Token: 0x040001B4 RID: 436
		public static readonly int MaxSelectors = 2 + 900000 / BZip2.G_SIZE;

		// Token: 0x040001B5 RID: 437
		public static readonly int NUM_OVERSHOOT_BYTES = 20;

		// Token: 0x040001B6 RID: 438
		internal static readonly int QSORT_STACK_SIZE = 1000;
	}
}
