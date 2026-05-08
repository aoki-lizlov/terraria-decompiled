using System;
using ReLogic.Reflection;

namespace Terraria.ID
{
	// Token: 0x020001A1 RID: 417
	public class SurfaceBackgroundID
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x0000357B File Offset: 0x0000177B
		public SurfaceBackgroundID()
		{
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00513B8A File Offset: 0x00511D8A
		// Note: this type is marked as 'beforefieldinit'.
		static SurfaceBackgroundID()
		{
		}

		// Token: 0x0400194A RID: 6474
		public const int Forest1 = 0;

		// Token: 0x0400194B RID: 6475
		public const int Corruption = 1;

		// Token: 0x0400194C RID: 6476
		public const int Desert = 2;

		// Token: 0x0400194D RID: 6477
		public const int Jungle = 3;

		// Token: 0x0400194E RID: 6478
		public const int Ocean = 4;

		// Token: 0x0400194F RID: 6479
		public const int CorruptDesert = 5;

		// Token: 0x04001950 RID: 6480
		public const int Hallow = 6;

		// Token: 0x04001951 RID: 6481
		public const int Snow = 7;

		// Token: 0x04001952 RID: 6482
		public const int Crimson = 8;

		// Token: 0x04001953 RID: 6483
		public const int Mushroom = 9;

		// Token: 0x04001954 RID: 6484
		public const int Forest2 = 10;

		// Token: 0x04001955 RID: 6485
		public const int Forest3 = 11;

		// Token: 0x04001956 RID: 6486
		public const int Forest4 = 12;

		// Token: 0x04001957 RID: 6487
		public const int HallowDesert = 13;

		// Token: 0x04001958 RID: 6488
		public const int CrimsonDesert = 14;

		// Token: 0x04001959 RID: 6489
		public const int Empty = 15;

		// Token: 0x0400195A RID: 6490
		public const int Count = 16;

		// Token: 0x0400195B RID: 6491
		public static readonly IdDictionary Search = IdDictionary.Create<SurfaceBackgroundID, int>();

		// Token: 0x02000763 RID: 1891
		public static class Sets
		{
			// Token: 0x06004117 RID: 16663 RVA: 0x006A0DA0 File Offset: 0x0069EFA0
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006A12 RID: 27154
			public static SetFactory Factory = new SetFactory(16);

			// Token: 0x04006A13 RID: 27155
			public static bool[] IsDesertVariant = SurfaceBackgroundID.Sets.Factory.CreateBoolSet(false, new int[] { 2, 5, 13, 14 });

			// Token: 0x04006A14 RID: 27156
			public static bool[] IsForest = SurfaceBackgroundID.Sets.Factory.CreateBoolSet(false, new int[] { 0, 10, 11, 12 });
		}
	}
}
