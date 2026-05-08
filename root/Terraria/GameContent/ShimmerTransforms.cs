using System;
using System.Runtime.CompilerServices;
using Terraria.Enums;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000249 RID: 585
	public static class ShimmerTransforms
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x0053C114 File Offset: 0x0053A314
		public static int GetDecraftingRecipeIndex(int type)
		{
			int num = ItemID.Sets.IsCrafted[type];
			if (num < 0)
			{
				return -1;
			}
			if (WorldGen.crimson && ItemID.Sets.IsCraftedCrimson[type] >= 0)
			{
				return ItemID.Sets.IsCraftedCrimson[type];
			}
			if (!WorldGen.crimson && ItemID.Sets.IsCraftedCorruption[type] >= 0)
			{
				return ItemID.Sets.IsCraftedCorruption[type];
			}
			return num;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0053C162 File Offset: 0x0053A362
		public static bool IsItemTransformLocked(int type)
		{
			return !NPC.downedMoonlord && ItemID.Sets.ShimmerPostMoonlord[type];
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0053C177 File Offset: 0x0053A377
		public static bool IsItemDecraftLocked(int type)
		{
			return ShimmerTransforms.IsRecipeIndexDecraftLocked(ShimmerTransforms.GetDecraftingRecipeIndex(type));
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0053C184 File Offset: 0x0053A384
		public static bool IsRecipeIndexDecraftLocked(int recipeIndex)
		{
			return recipeIndex >= 0 && ((!NPC.downedBoss3 && ShimmerTransforms.RecipeSets.PostSkeletron[recipeIndex]) || (!NPC.downedGolemBoss && ShimmerTransforms.RecipeSets.PostGolem[recipeIndex]));
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0053C1B4 File Offset: 0x0053A3B4
		public static bool IsItemDecraftableAndIsDecraftUnlocked(Item item)
		{
			if (item == null)
			{
				return false;
			}
			int decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(item.GetShimmerEquivalentType(true));
			return !ShimmerTransforms.IsRecipeIndexDecraftLocked(decraftingRecipeIndex) && decraftingRecipeIndex >= 0 && item.stack / Main.recipe[decraftingRecipeIndex].createItem.stack > 0;
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0053C200 File Offset: 0x0053A400
		public static void UpdateRecipeSets()
		{
			ShimmerTransforms.RecipeSets.PostSkeletron = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(154));
			ShimmerTransforms.RecipeSets.PostGolem = Utils.MapArray<Recipe, bool>(Main.recipe, (Recipe r) => r.ContainsIngredient(1101));
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0053C26C File Offset: 0x0053A46C
		public static int GetTransformToItem(int type)
		{
			int num = ItemID.Sets.ShimmerTransformToItem[type];
			if (num > 0)
			{
				return num;
			}
			if (ContentSamples.ItemsByType[type].createTile == 139)
			{
				int placeStyle = ContentSamples.ItemsByType[type].placeStyle;
				if (placeStyle == 90)
				{
					return 5538;
				}
				if (placeStyle == 89)
				{
					return 5579;
				}
				if (placeStyle == 97)
				{
					return 5638;
				}
				if (placeStyle == 96)
				{
					return 5639;
				}
				return 576;
			}
			else
			{
				if (type == 3461)
				{
					return ShimmerTransforms.GetLunarBrickTransformFromMoonPhase(Main.GetMoonPhase());
				}
				return 0;
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0053C2F8 File Offset: 0x0053A4F8
		private static int GetLunarBrickTransformFromMoonPhase(MoonPhase moonPhase)
		{
			switch (moonPhase)
			{
			case MoonPhase.Full:
				return 5408;
			case MoonPhase.ThreeQuartersAtLeft:
				return 5401;
			case MoonPhase.HalfAtLeft:
				return 5403;
			case MoonPhase.QuarterAtLeft:
				return 5402;
			default:
				return 5406;
			case MoonPhase.QuarterAtRight:
				return 5407;
			case MoonPhase.HalfAtRight:
				return 5405;
			case MoonPhase.ThreeQuartersAtRight:
				return 5404;
			}
		}

		// Token: 0x020007D2 RID: 2002
		public static class RecipeSets
		{
			// Token: 0x0400710D RID: 28941
			public static bool[] PostSkeletron;

			// Token: 0x0400710E RID: 28942
			public static bool[] PostGolem;
		}

		// Token: 0x020007D3 RID: 2003
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004238 RID: 16952 RVA: 0x006BE150 File Offset: 0x006BC350
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004239 RID: 16953 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600423A RID: 16954 RVA: 0x006BE15C File Offset: 0x006BC35C
			internal bool <UpdateRecipeSets>b__6_0(Recipe r)
			{
				return r.ContainsIngredient(154);
			}

			// Token: 0x0600423B RID: 16955 RVA: 0x006BE169 File Offset: 0x006BC369
			internal bool <UpdateRecipeSets>b__6_1(Recipe r)
			{
				return r.ContainsIngredient(1101);
			}

			// Token: 0x0400710F RID: 28943
			public static readonly ShimmerTransforms.<>c <>9 = new ShimmerTransforms.<>c();

			// Token: 0x04007110 RID: 28944
			public static Func<Recipe, bool> <>9__6_0;

			// Token: 0x04007111 RID: 28945
			public static Func<Recipe, bool> <>9__6_1;
		}
	}
}
