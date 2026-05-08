using System;

namespace Terraria.ID
{
	// Token: 0x020001B6 RID: 438
	public static class PlayerVariantID
	{
		// Token: 0x06001F3B RID: 7995 RVA: 0x00516A26 File Offset: 0x00514C26
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerVariantID()
		{
		}

		// Token: 0x040020DC RID: 8412
		public const int MaleStarter = 0;

		// Token: 0x040020DD RID: 8413
		public const int MaleSticker = 1;

		// Token: 0x040020DE RID: 8414
		public const int MaleGangster = 2;

		// Token: 0x040020DF RID: 8415
		public const int MaleCoat = 3;

		// Token: 0x040020E0 RID: 8416
		public const int FemaleStarter = 4;

		// Token: 0x040020E1 RID: 8417
		public const int FemaleSticker = 5;

		// Token: 0x040020E2 RID: 8418
		public const int FemaleGangster = 6;

		// Token: 0x040020E3 RID: 8419
		public const int FemaleCoat = 7;

		// Token: 0x040020E4 RID: 8420
		public const int MaleDress = 8;

		// Token: 0x040020E5 RID: 8421
		public const int FemaleDress = 9;

		// Token: 0x040020E6 RID: 8422
		public const int MaleDisplayDoll = 10;

		// Token: 0x040020E7 RID: 8423
		public const int FemaleDisplayDoll = 11;

		// Token: 0x040020E8 RID: 8424
		public static readonly int Count = 12;

		// Token: 0x02000783 RID: 1923
		public class Sets
		{
			// Token: 0x0600414D RID: 16717 RVA: 0x0000357B File Offset: 0x0000177B
			public Sets()
			{
			}

			// Token: 0x0600414E RID: 16718 RVA: 0x006A17D4 File Offset: 0x0069F9D4
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006E7E RID: 28286
			public static SetFactory Factory = new SetFactory(PlayerVariantID.Count);

			// Token: 0x04006E7F RID: 28287
			public static bool[] Male = PlayerVariantID.Sets.Factory.CreateBoolSet(new int[] { 0, 1, 2, 3, 8, 10 });

			// Token: 0x04006E80 RID: 28288
			public static int[] AltGenderReference = PlayerVariantID.Sets.Factory.CreateIntSet(0, new int[]
			{
				0, 4, 4, 0, 1, 5, 5, 1, 2, 6,
				6, 2, 3, 7, 7, 3, 8, 9, 9, 8,
				10, 11, 11, 10
			});

			// Token: 0x04006E81 RID: 28289
			public static int[] VariantOrderMale = new int[] { 0, 1, 2, 3, 8, 10 };

			// Token: 0x04006E82 RID: 28290
			public static int[] VariantOrderFemale = new int[] { 4, 5, 6, 7, 9, 11 };
		}
	}
}
