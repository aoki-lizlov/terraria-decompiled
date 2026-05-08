using System;
using System.Collections.Generic;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x02000245 RID: 581
	public class FlexibleTileWand
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x0053A721 File Offset: 0x00538921
		public static FlexibleTileWand ExposedGem(int itemId, int style)
		{
			return FlexibleTileWand.CreateSingleTileWand(itemId, 178, new int[]
			{
				style,
				style + 7,
				style + 14
			}).WithoutAmmoIcon().WithoutAmmoConsumption();
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0053A74F File Offset: 0x0053894F
		public FlexibleTileWand WithoutAmmoIcon()
		{
			this.ShowsHoverAmmoIcon = false;
			return this;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0053A759 File Offset: 0x00538959
		public FlexibleTileWand WithoutAmmoConsumption()
		{
			this.ConsumesAmmoItem = false;
			return this;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0053A763 File Offset: 0x00538963
		public FlexibleTileWand WithConsumingFavorites()
		{
			this.CanConsumeFavorites = true;
			return this;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0053A770 File Offset: 0x00538970
		public void AddVariation(int itemType, int tileIdToPlace, int tileStyleToPlace)
		{
			FlexibleTileWand.OptionBucket optionBucket;
			if (!this._options.TryGetValue(itemType, out optionBucket))
			{
				optionBucket = (this._options[itemType] = new FlexibleTileWand.OptionBucket(itemType));
			}
			optionBucket.Options.Add(new FlexibleTileWand.PlacementOption
			{
				TileIdToPlace = tileIdToPlace,
				TileStyleToPlace = tileStyleToPlace
			});
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0053A7C4 File Offset: 0x005389C4
		public void AddVariations(int itemType, int tileIdToPlace, params int[] stylesToPlace)
		{
			foreach (int num in stylesToPlace)
			{
				this.AddVariation(itemType, tileIdToPlace, num);
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0053A7EC File Offset: 0x005389EC
		public void AddVariationsWithOffset(int itemType, int tileIdToPlace, int offset, params int[] stylesToPlace)
		{
			for (int i = 0; i < stylesToPlace.Length; i++)
			{
				int num = offset + stylesToPlace[i];
				this.AddVariation(itemType, tileIdToPlace, num);
			}
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0053A818 File Offset: 0x00538A18
		public void AddVariations_ByRow(int itemType, int tileIdToPlace, int variationsPerRow, params int[] rows)
		{
			for (int i = 0; i < rows.Length; i++)
			{
				for (int j = 0; j < variationsPerRow; j++)
				{
					int num = rows[i] * variationsPerRow + j;
					this.AddVariation(itemType, tileIdToPlace, num);
				}
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0053A854 File Offset: 0x00538A54
		public bool TryGetPlacementOption(Player player, int randomSeed, int selectCycleOffset, out FlexibleTileWand.PlacementOption option, out Item itemToConsume)
		{
			option = null;
			itemToConsume = null;
			Item[] inventory = player.inventory;
			int num = 1;
			for (int i = 0; i < 58 + num; i++)
			{
				if (i < 50 || i >= 54)
				{
					Item item = inventory[i];
					FlexibleTileWand.OptionBucket optionBucket;
					if (!item.IsAir && (this.CanConsumeFavorites || !item.favorited) && this._options.TryGetValue(item.type, out optionBucket))
					{
						this._random.SetSeed(randomSeed);
						option = optionBucket.GetOptionWithCycling(selectCycleOffset);
						itemToConsume = item;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0053A8DC File Offset: 0x00538ADC
		public static FlexibleTileWand CreateRubblePlacerLarge()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			int num = 647;
			flexibleTileWand.AddVariations(154, num, new int[] { 0, 1, 2, 3, 4, 5, 6 });
			flexibleTileWand.AddVariations(3, num, new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15 });
			flexibleTileWand.AddVariations(71, num, new int[] { 16, 17 });
			flexibleTileWand.AddVariations(72, num, new int[] { 18, 19 });
			flexibleTileWand.AddVariations(73, num, new int[] { 20, 21 });
			flexibleTileWand.AddVariations(9, num, new int[] { 22, 23, 24, 25 });
			flexibleTileWand.AddVariations(593, num, new int[] { 26, 27, 28, 29, 30, 31 });
			flexibleTileWand.AddVariations(183, num, new int[] { 32, 33, 34 });
			num = 648;
			flexibleTileWand.AddVariations(195, num, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(195, num, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariations(174, num, new int[] { 6, 7, 8 });
			flexibleTileWand.AddVariation(4144, 706, 0);
			flexibleTileWand.AddVariations(150, num, new int[] { 9, 10, 11, 12, 13 });
			flexibleTileWand.AddVariations(3, num, new int[] { 14, 15, 16 });
			flexibleTileWand.AddVariations(989, num, new int[] { 17 });
			flexibleTileWand.AddVariations(1101, num, new int[] { 18, 19, 20 });
			flexibleTileWand.AddVariations(9, num, new int[] { 21, 22 });
			flexibleTileWand.AddVariations(9, num, new int[] { 23, 24, 25, 26, 27, 28 });
			flexibleTileWand.AddVariations(3271, num, new int[] { 29, 30, 31, 32, 33, 34 });
			flexibleTileWand.AddVariations(3086, num, new int[] { 35, 36, 37, 38, 39, 40 });
			flexibleTileWand.AddVariations(3081, num, new int[] { 41, 42, 43, 44, 45, 46 });
			flexibleTileWand.AddVariations(62, num, new int[] { 47, 48, 49 });
			flexibleTileWand.AddVariations(62, num, new int[] { 50, 51 });
			flexibleTileWand.AddVariations(154, num, new int[] { 52, 53, 54 });
			num = 651;
			flexibleTileWand.AddVariations(195, num, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(62, num, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariations(331, num, new int[] { 6, 7, 8 });
			flexibleTileWand.AddVariation(501, 704, 0);
			num = 705;
			flexibleTileWand.AddVariations(276, num, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
			flexibleTileWand.AddVariations(369, num, new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 });
			flexibleTileWand.AddVariations(2171, num, new int[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
			flexibleTileWand.AddVariations(59, num, new int[] { 27, 28, 29, 30, 31, 32, 33, 34, 35 });
			return flexibleTileWand;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0053AC60 File Offset: 0x00538E60
		public static FlexibleTileWand CreateRubblePlacerMedium()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			ushort num = 652;
			flexibleTileWand.AddVariations(195, (int)num, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(62, (int)num, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariations(331, (int)num, new int[] { 6, 7, 8, 9, 10, 11 });
			num = 649;
			flexibleTileWand.AddVariations(3, (int)num, new int[] { 0, 1, 2, 3, 4, 5 });
			flexibleTileWand.AddVariations(154, (int)num, new int[] { 6, 7, 8, 9, 10 });
			flexibleTileWand.AddVariations(154, (int)num, new int[] { 11, 12, 13, 14, 15 });
			flexibleTileWand.AddVariations(71, (int)num, new int[] { 16 });
			flexibleTileWand.AddVariations(72, (int)num, new int[] { 17 });
			flexibleTileWand.AddVariations(73, (int)num, new int[] { 18 });
			flexibleTileWand.AddVariations(181, (int)num, new int[] { 19 });
			flexibleTileWand.AddVariations(180, (int)num, new int[] { 20 });
			flexibleTileWand.AddVariations(177, (int)num, new int[] { 21 });
			flexibleTileWand.AddVariations(179, (int)num, new int[] { 22 });
			flexibleTileWand.AddVariations(178, (int)num, new int[] { 23 });
			flexibleTileWand.AddVariations(182, (int)num, new int[] { 24 });
			flexibleTileWand.AddVariations(593, (int)num, new int[] { 25, 26, 27, 28, 29, 30 });
			flexibleTileWand.AddVariations(9, (int)num, new int[] { 31, 32, 33 });
			flexibleTileWand.AddVariations(150, (int)num, new int[] { 34, 35, 36, 37 });
			flexibleTileWand.AddVariations(3, (int)num, new int[] { 38, 39, 40 });
			flexibleTileWand.AddVariations(3271, (int)num, new int[] { 41, 42, 43, 44, 45, 46 });
			flexibleTileWand.AddVariations(3086, (int)num, new int[] { 47, 48, 49, 50, 51, 52 });
			flexibleTileWand.AddVariations(3081, (int)num, new int[] { 53, 54, 55, 56, 57, 58 });
			flexibleTileWand.AddVariations(62, (int)num, new int[] { 59, 60, 61 });
			flexibleTileWand.AddVariations(169, (int)num, new int[] { 62, 63, 65, 66, 67 });
			flexibleTileWand.AddVariations(276, (int)num, new int[] { 64 });
			flexibleTileWand.AddVariations(1291, 702, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(5667, 751, new int[1]);
			return flexibleTileWand;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0053AF30 File Offset: 0x00539130
		public static FlexibleTileWand CreateRubblePlacerSmall()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			ushort num = 650;
			flexibleTileWand.AddVariations(3, (int)num, new int[] { 0, 1, 2, 3, 4, 5 });
			flexibleTileWand.AddVariations(2, (int)num, new int[] { 6, 7, 8, 9, 10, 11 });
			flexibleTileWand.AddVariations(154, (int)num, new int[] { 12, 13, 14, 15, 16, 17, 18, 19 });
			flexibleTileWand.AddVariations(154, (int)num, new int[] { 20, 21, 22, 23, 24, 25, 26, 27 });
			flexibleTileWand.AddVariations(9, (int)num, new int[] { 28, 29, 30, 31, 32 });
			flexibleTileWand.AddVariations(9, (int)num, new int[] { 33, 34, 35 });
			flexibleTileWand.AddVariations(593, (int)num, new int[] { 36, 37, 38, 39, 40, 41 });
			flexibleTileWand.AddVariations(664, (int)num, new int[] { 42, 43, 44, 45, 46, 47 });
			flexibleTileWand.AddVariations(150, (int)num, new int[] { 48, 49, 50, 51, 52, 53 });
			flexibleTileWand.AddVariations(3271, (int)num, new int[] { 54, 55, 56, 57, 58, 59 });
			flexibleTileWand.AddVariations(3086, (int)num, new int[] { 60, 61, 62, 63, 64, 65 });
			flexibleTileWand.AddVariations(3081, (int)num, new int[] { 66, 67, 68, 69, 70, 71 });
			flexibleTileWand.AddVariations(62, (int)num, new int[] { 72 });
			flexibleTileWand.AddVariations(169, (int)num, new int[] { 73, 74, 76, 78, 79, 80, 81 });
			flexibleTileWand.AddVariations(276, (int)num, new int[] { 75, 77 });
			flexibleTileWand.AddVariation(5114, 700, 0);
			flexibleTileWand.AddVariation(5333, 701, 0);
			flexibleTileWand.AddVariations(208, 703, new int[] { 6, 7 });
			flexibleTileWand.AddVariations(331, 703, new int[] { 8 });
			flexibleTileWand.AddVariations(223, 703, new int[] { 9 });
			flexibleTileWand.AddVariation(165, 707, 5);
			return flexibleTileWand;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0053B16B File Offset: 0x0053936B
		public static FlexibleTileWand CreateSingleTileWand(int itemIdToConsume, int TileTypeToplace, params int[] stylesToPlace)
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			flexibleTileWand.AddVariations(itemIdToConsume, TileTypeToplace, stylesToPlace);
			return flexibleTileWand;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0053B17C File Offset: 0x0053937C
		public static FlexibleTileWand CreateMiteyTitey()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			ushort num = 693;
			flexibleTileWand.AddVariations(664, (int)num, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(3, (int)num, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariations(1124, (int)num, new int[] { 9, 10, 11 });
			flexibleTileWand.AddVariations(409, (int)num, new int[] { 12, 13, 14 });
			flexibleTileWand.AddVariations(61, (int)num, new int[] { 15, 16, 17 });
			flexibleTileWand.AddVariations(836, (int)num, new int[] { 18, 19, 20 });
			flexibleTileWand.AddVariations(3271, (int)num, new int[] { 21, 22, 23 });
			flexibleTileWand.AddVariations(3086, (int)num, new int[] { 24, 25, 26 });
			flexibleTileWand.AddVariations(3081, (int)num, new int[] { 27, 28, 29 });
			flexibleTileWand.AddVariations(834, (int)num, new int[] { 30, 31, 32 });
			flexibleTileWand.AddVariations(833, (int)num, new int[] { 33, 34, 35 });
			flexibleTileWand.AddVariations(835, (int)num, new int[] { 36, 37, 38 });
			int num2 = 39;
			flexibleTileWand.AddVariationsWithOffset(3, (int)num, num2, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariationsWithOffset(1124, (int)num, num2, new int[] { 9, 10, 11 });
			flexibleTileWand.AddVariationsWithOffset(409, (int)num, num2, new int[] { 12, 13, 14 });
			flexibleTileWand.AddVariationsWithOffset(61, (int)num, num2, new int[] { 15, 16, 17 });
			flexibleTileWand.AddVariationsWithOffset(836, (int)num, num2, new int[] { 18, 19, 20 });
			flexibleTileWand.AddVariationsWithOffset(3271, (int)num, num2, new int[] { 21, 22, 23 });
			flexibleTileWand.AddVariationsWithOffset(3086, (int)num, num2, new int[] { 24, 25, 26 });
			flexibleTileWand.AddVariationsWithOffset(3081, (int)num, num2, new int[] { 27, 28, 29 });
			num = 694;
			flexibleTileWand.AddVariations(664, (int)num, new int[] { 0, 1, 2 });
			flexibleTileWand.AddVariations(3, (int)num, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariations(150, (int)num, new int[] { 6, 7, 8 });
			flexibleTileWand.AddVariations(409, (int)num, new int[] { 12, 13, 14 });
			flexibleTileWand.AddVariations(61, (int)num, new int[] { 15, 16, 17 });
			flexibleTileWand.AddVariations(836, (int)num, new int[] { 18, 19, 20 });
			flexibleTileWand.AddVariations(3271, (int)num, new int[] { 21, 22, 23 });
			flexibleTileWand.AddVariations(3086, (int)num, new int[] { 24, 25, 26 });
			flexibleTileWand.AddVariations(3081, (int)num, new int[] { 27, 28, 29 });
			flexibleTileWand.AddVariations(834, (int)num, new int[] { 30, 31, 32 });
			flexibleTileWand.AddVariations(833, (int)num, new int[] { 33, 34, 35 });
			flexibleTileWand.AddVariations(835, (int)num, new int[] { 36, 37, 38 });
			flexibleTileWand.AddVariationsWithOffset(3, (int)num, num2, new int[] { 3, 4, 5 });
			flexibleTileWand.AddVariationsWithOffset(409, (int)num, num2, new int[] { 12, 13, 14 });
			flexibleTileWand.AddVariationsWithOffset(61, (int)num, num2, new int[] { 15, 16, 17 });
			flexibleTileWand.AddVariationsWithOffset(836, (int)num, num2, new int[] { 18, 19, 20 });
			flexibleTileWand.AddVariationsWithOffset(3271, (int)num, num2, new int[] { 21, 22, 23 });
			flexibleTileWand.AddVariationsWithOffset(3086, (int)num, num2, new int[] { 24, 25, 26 });
			flexibleTileWand.AddVariationsWithOffset(3081, (int)num, num2, new int[] { 27, 28, 29 });
			return flexibleTileWand;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x0053B5F8 File Offset: 0x005397F8
		public static FlexibleTileWand CreatePortableKiln()
		{
			FlexibleTileWand flexibleTileWand = new FlexibleTileWand();
			int num = 3;
			int num2 = 653;
			flexibleTileWand.AddVariations_ByRow(133, num2, num, new int[] { 0, 1, 2, 3 });
			flexibleTileWand.AddVariations_ByRow(664, num2, num, new int[] { 4, 5, 6 });
			flexibleTileWand.AddVariations_ByRow(4564, num2, num, new int[] { 7, 8, 9 });
			flexibleTileWand.AddVariations_ByRow(154, num2, num, new int[] { 10, 11, 12 });
			flexibleTileWand.AddVariations_ByRow(173, num2, num, new int[] { 13, 14, 15 });
			flexibleTileWand.AddVariations_ByRow(61, num2, num, new int[] { 16, 17, 18 });
			flexibleTileWand.AddVariations_ByRow(150, num2, num, new int[] { 19, 20, 21 });
			flexibleTileWand.AddVariations_ByRow(836, num2, num, new int[] { 22, 23, 24 });
			flexibleTileWand.AddVariations_ByRow(3272, num2, num, new int[] { 25, 26, 27 });
			flexibleTileWand.AddVariations_ByRow(1101, num2, num, new int[] { 28, 29, 30 });
			flexibleTileWand.AddVariations_ByRow(3081, num2, num, new int[] { 31, 32, 33 });
			flexibleTileWand.AddVariations_ByRow(3271, num2, num, new int[] { 34, 35, 36 });
			return flexibleTileWand;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0053B779 File Offset: 0x00539979
		public FlexibleTileWand()
		{
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0053B7AC File Offset: 0x005399AC
		// Note: this type is marked as 'beforefieldinit'.
		static FlexibleTileWand()
		{
			int num = 5472;
			int num2 = 698;
			int[] array = new int[3];
			array[0] = 1;
			array[1] = 2;
			FlexibleTileWand.DeadCellsDisplayJar = FlexibleTileWand.CreateSingleTileWand(num, num2, array).WithoutAmmoIcon().WithoutAmmoConsumption();
			FlexibleTileWand.Amethyst = FlexibleTileWand.ExposedGem(181, 0);
			FlexibleTileWand.Topaz = FlexibleTileWand.ExposedGem(180, 1);
			FlexibleTileWand.Sapphire = FlexibleTileWand.ExposedGem(177, 2);
			FlexibleTileWand.Emerald = FlexibleTileWand.ExposedGem(179, 3);
			FlexibleTileWand.Ruby = FlexibleTileWand.ExposedGem(178, 4);
			FlexibleTileWand.Diamond = FlexibleTileWand.ExposedGem(182, 5);
			FlexibleTileWand.Amber = FlexibleTileWand.ExposedGem(999, 6);
			FlexibleTileWand.Explosives = FlexibleTileWand.CreateSingleTileWand(580, 141, new int[] { 0, 1 }).WithoutAmmoIcon().WithoutAmmoConsumption();
			FlexibleTileWand.CrystalShard = FlexibleTileWand.CreateSingleTileWand(502, 129, new int[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17
			}).WithoutAmmoIcon().WithoutAmmoConsumption();
		}

		// Token: 0x04004D1C RID: 19740
		public static FlexibleTileWand RubblePlacementSmall = FlexibleTileWand.CreateRubblePlacerSmall();

		// Token: 0x04004D1D RID: 19741
		public static FlexibleTileWand RubblePlacementMedium = FlexibleTileWand.CreateRubblePlacerMedium();

		// Token: 0x04004D1E RID: 19742
		public static FlexibleTileWand RubblePlacementLarge = FlexibleTileWand.CreateRubblePlacerLarge();

		// Token: 0x04004D1F RID: 19743
		public static FlexibleTileWand MiteyTitey = FlexibleTileWand.CreateMiteyTitey();

		// Token: 0x04004D20 RID: 19744
		public static FlexibleTileWand SandCastleBucket = FlexibleTileWand.CreateSingleTileWand(169, 552, new int[] { 0, 1, 2, 3 }).WithoutAmmoIcon();

		// Token: 0x04004D21 RID: 19745
		public static FlexibleTileWand GardenGnome = FlexibleTileWand.CreateSingleTileWand(4609, 567, new int[] { 0, 1, 2, 3, 4 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D22 RID: 19746
		public static FlexibleTileWand Coral = FlexibleTileWand.CreateSingleTileWand(275, 81, new int[] { 0, 1, 2, 3, 4, 5 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D23 RID: 19747
		public static FlexibleTileWand Seashell = FlexibleTileWand.CreateSingleTileWand(2625, 324, new int[] { 0, 1, 2 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D24 RID: 19748
		public static FlexibleTileWand Starfish = FlexibleTileWand.CreateSingleTileWand(2626, 324, new int[] { 3, 4, 5 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D25 RID: 19749
		public static FlexibleTileWand LightningWhelkShell = FlexibleTileWand.CreateSingleTileWand(4072, 324, new int[] { 6, 7, 8 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D26 RID: 19750
		public static FlexibleTileWand TulipShell = FlexibleTileWand.CreateSingleTileWand(4073, 324, new int[] { 9, 10, 11 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D27 RID: 19751
		public static FlexibleTileWand JunoniaShell = FlexibleTileWand.CreateSingleTileWand(4071, 324, new int[] { 12, 13, 14 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D28 RID: 19752
		public static FlexibleTileWand JackoLantern = FlexibleTileWand.CreateSingleTileWand(1813, 35, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D29 RID: 19753
		public static FlexibleTileWand Catacomb = FlexibleTileWand.CreateSingleTileWand(1417, 241, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D2A RID: 19754
		public static FlexibleTileWand Present = FlexibleTileWand.CreateSingleTileWand(1869, 36, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D2B RID: 19755
		public static FlexibleTileWand PartyPresent = FlexibleTileWand.CreateSingleTileWand(3749, 457, new int[] { 0, 1, 2, 3, 4 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D2C RID: 19756
		public static FlexibleTileWand Book = FlexibleTileWand.CreateSingleTileWand(149, 50, new int[] { 0, 1, 2, 3, 4 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D2D RID: 19757
		public static FlexibleTileWand LawnFlamingo = FlexibleTileWand.CreateSingleTileWand(4420, 545, new int[] { 0, 1 }).WithoutAmmoIcon().WithoutAmmoConsumption();

		// Token: 0x04004D2E RID: 19758
		public static FlexibleTileWand PortableKiln = FlexibleTileWand.CreatePortableKiln();

		// Token: 0x04004D2F RID: 19759
		public static FlexibleTileWand DeadCellsDisplayJar;

		// Token: 0x04004D30 RID: 19760
		public static FlexibleTileWand Amethyst;

		// Token: 0x04004D31 RID: 19761
		public static FlexibleTileWand Topaz;

		// Token: 0x04004D32 RID: 19762
		public static FlexibleTileWand Sapphire;

		// Token: 0x04004D33 RID: 19763
		public static FlexibleTileWand Emerald;

		// Token: 0x04004D34 RID: 19764
		public static FlexibleTileWand Ruby;

		// Token: 0x04004D35 RID: 19765
		public static FlexibleTileWand Diamond;

		// Token: 0x04004D36 RID: 19766
		public static FlexibleTileWand Amber;

		// Token: 0x04004D37 RID: 19767
		public static FlexibleTileWand Explosives;

		// Token: 0x04004D38 RID: 19768
		public static FlexibleTileWand CrystalShard;

		// Token: 0x04004D39 RID: 19769
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04004D3A RID: 19770
		private Dictionary<int, FlexibleTileWand.OptionBucket> _options = new Dictionary<int, FlexibleTileWand.OptionBucket>();

		// Token: 0x04004D3B RID: 19771
		public bool ConsumesAmmoItem = true;

		// Token: 0x04004D3C RID: 19772
		public bool ShowsHoverAmmoIcon = true;

		// Token: 0x04004D3D RID: 19773
		public bool CanConsumeFavorites = true;

		// Token: 0x020007CF RID: 1999
		private class OptionBucket
		{
			// Token: 0x06004232 RID: 16946 RVA: 0x006BE0D3 File Offset: 0x006BC2D3
			public OptionBucket(int itemTypeToConsume)
			{
				this.ItemTypeToConsume = itemTypeToConsume;
				this.Options = new List<FlexibleTileWand.PlacementOption>();
			}

			// Token: 0x06004233 RID: 16947 RVA: 0x006BE0ED File Offset: 0x006BC2ED
			public FlexibleTileWand.PlacementOption GetRandomOption(UnifiedRandom random)
			{
				return this.Options[random.Next(this.Options.Count)];
			}

			// Token: 0x06004234 RID: 16948 RVA: 0x006BE10C File Offset: 0x006BC30C
			public FlexibleTileWand.PlacementOption GetOptionWithCycling(int cycleOffset)
			{
				int count = this.Options.Count;
				int num = (cycleOffset % count + count) % count;
				return this.Options[num];
			}

			// Token: 0x04007105 RID: 28933
			public int ItemTypeToConsume;

			// Token: 0x04007106 RID: 28934
			public List<FlexibleTileWand.PlacementOption> Options;
		}

		// Token: 0x020007D0 RID: 2000
		public class PlacementOption
		{
			// Token: 0x06004235 RID: 16949 RVA: 0x0000357B File Offset: 0x0000177B
			public PlacementOption()
			{
			}

			// Token: 0x04007107 RID: 28935
			public int TileIdToPlace;

			// Token: 0x04007108 RID: 28936
			public int TileStyleToPlace;
		}
	}
}
