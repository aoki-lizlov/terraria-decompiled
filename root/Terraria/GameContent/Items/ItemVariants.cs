using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000477 RID: 1143
	public static class ItemVariants
	{
		// Token: 0x0600331C RID: 13084 RVA: 0x005F3668 File Offset: 0x005F1868
		public static IEnumerable<ItemVariants.VariantEntry> GetVariants(int itemId)
		{
			if (!ItemVariants._variants.IndexInRange(itemId))
			{
				return Enumerable.Empty<ItemVariants.VariantEntry>();
			}
			IEnumerable<ItemVariants.VariantEntry> enumerable = ItemVariants._variants[itemId];
			return enumerable ?? Enumerable.Empty<ItemVariants.VariantEntry>();
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x005F369C File Offset: 0x005F189C
		private static ItemVariants.VariantEntry GetEntry(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetVariants(itemId).SingleOrDefault((ItemVariants.VariantEntry v) => v.Variant == variant);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x005F36D0 File Offset: 0x005F18D0
		public static void AddVariant(int itemId, ItemVariant variant, params ItemVariantCondition[] conditions)
		{
			ItemVariants.VariantEntry variantEntry = ItemVariants.GetEntry(itemId, variant);
			if (variantEntry == null)
			{
				List<ItemVariants.VariantEntry> list = ItemVariants._variants[itemId];
				if (list == null)
				{
					list = (ItemVariants._variants[itemId] = new List<ItemVariants.VariantEntry>());
				}
				list.Add(variantEntry = new ItemVariants.VariantEntry(variant));
			}
			variantEntry.AddConditions(conditions);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x005F3716 File Offset: 0x005F1916
		public static bool HasVariant(int itemId, ItemVariant variant)
		{
			return ItemVariants.GetEntry(itemId, variant) != null;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x005F3724 File Offset: 0x005F1924
		public static ItemVariant SelectVariant(int itemId)
		{
			if (!ItemVariants._variants.IndexInRange(itemId))
			{
				return null;
			}
			List<ItemVariants.VariantEntry> list = ItemVariants._variants[itemId];
			if (list == null)
			{
				return null;
			}
			foreach (ItemVariants.VariantEntry variantEntry in list)
			{
				if (variantEntry.AnyConditionMet())
				{
					return variantEntry.Variant;
				}
			}
			return null;
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x005F379C File Offset: 0x005F199C
		static ItemVariants()
		{
			ItemVariants.AddVariant(112, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(157, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(1319, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(1325, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(3069, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5147, ItemVariants.StrongerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(517, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(683, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(725, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(1314, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(2623, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5279, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5280, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5281, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5282, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5283, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(5284, ItemVariants.WeakerVariant, new ItemVariantCondition[] { ItemVariants.RemixWorld });
			ItemVariants.AddVariant(197, ItemVariants.RebalancedVariant, new ItemVariantCondition[] { ItemVariants.GetGoodWorld });
			ItemVariants.AddVariant(4060, ItemVariants.RebalancedVariant, new ItemVariantCondition[] { ItemVariants.GetGoodWorld });
			ItemVariants.AddVariant(556, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[] { ItemVariants.MechdusaWorld });
			ItemVariants.AddVariant(557, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[] { ItemVariants.MechdusaWorld });
			ItemVariants.AddVariant(544, ItemVariants.DisabledBossSummonVariant, new ItemVariantCondition[] { ItemVariants.MechdusaWorld });
			ItemVariants.AddVariant(5334, ItemVariants.EnabledVariant, new ItemVariantCondition[] { ItemVariants.MechdusaWorld });
		}

		// Token: 0x04005887 RID: 22663
		private static List<ItemVariants.VariantEntry>[] _variants = new List<ItemVariants.VariantEntry>[(int)ItemID.Count];

		// Token: 0x04005888 RID: 22664
		public static ItemVariant StrongerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Stronger", new object[0]));

		// Token: 0x04005889 RID: 22665
		public static ItemVariant WeakerVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Weaker", new object[0]));

		// Token: 0x0400588A RID: 22666
		public static ItemVariant RebalancedVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Rebalanced", new object[0]));

		// Token: 0x0400588B RID: 22667
		public static ItemVariant EnabledVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.Enabled", new object[0]));

		// Token: 0x0400588C RID: 22668
		public static ItemVariant DisabledBossSummonVariant = new ItemVariant(NetworkText.FromKey("ItemVariant.DisabledBossSummon", new object[0]));

		// Token: 0x0400588D RID: 22669
		public static ItemVariantCondition RemixWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.RemixWorld", new object[0]), () => Main.remixWorld);

		// Token: 0x0400588E RID: 22670
		public static ItemVariantCondition GetGoodWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.GetGoodWorld", new object[0]), () => Main.getGoodWorld);

		// Token: 0x0400588F RID: 22671
		public static ItemVariantCondition MechdusaWorld = new ItemVariantCondition(NetworkText.FromKey("ItemVariantCondition.MechdusaWorld", new object[0]), () => SpecialSeedFeatures.Mechdusa);

		// Token: 0x02000971 RID: 2417
		public class VariantEntry
		{
			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06004900 RID: 18688 RVA: 0x006D08BF File Offset: 0x006CEABF
			public IEnumerable<ItemVariantCondition> Conditions
			{
				get
				{
					return this._conditions;
				}
			}

			// Token: 0x06004901 RID: 18689 RVA: 0x006D08C7 File Offset: 0x006CEAC7
			public VariantEntry(ItemVariant variant)
			{
				this.Variant = variant;
			}

			// Token: 0x06004902 RID: 18690 RVA: 0x006D08E1 File Offset: 0x006CEAE1
			internal void AddConditions(IEnumerable<ItemVariantCondition> conditions)
			{
				this._conditions.AddRange(conditions);
			}

			// Token: 0x06004903 RID: 18691 RVA: 0x006D08EF File Offset: 0x006CEAEF
			public bool AnyConditionMet()
			{
				return this.Conditions.Any((ItemVariantCondition c) => c.IsMet());
			}

			// Token: 0x040075E4 RID: 30180
			public readonly ItemVariant Variant;

			// Token: 0x040075E5 RID: 30181
			private readonly List<ItemVariantCondition> _conditions = new List<ItemVariantCondition>();

			// Token: 0x02000AE9 RID: 2793
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004D00 RID: 19712 RVA: 0x006DB7DF File Offset: 0x006D99DF
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004D01 RID: 19713 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004D02 RID: 19714 RVA: 0x006DB7EB File Offset: 0x006D99EB
				internal bool <AnyConditionMet>b__6_0(ItemVariantCondition c)
				{
					return c.IsMet();
				}

				// Token: 0x040078B8 RID: 30904
				public static readonly ItemVariants.VariantEntry.<>c <>9 = new ItemVariants.VariantEntry.<>c();

				// Token: 0x040078B9 RID: 30905
				public static Func<ItemVariantCondition, bool> <>9__6_0;
			}
		}

		// Token: 0x02000972 RID: 2418
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06004904 RID: 18692 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06004905 RID: 18693 RVA: 0x006D091B File Offset: 0x006CEB1B
			internal bool <GetEntry>b__0(ItemVariants.VariantEntry v)
			{
				return v.Variant == this.variant;
			}

			// Token: 0x040075E6 RID: 30182
			public ItemVariant variant;
		}

		// Token: 0x02000973 RID: 2419
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004906 RID: 18694 RVA: 0x006D092B File Offset: 0x006CEB2B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004907 RID: 18695 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004908 RID: 18696 RVA: 0x006C1B23 File Offset: 0x006BFD23
			internal bool <.cctor>b__15_0()
			{
				return Main.remixWorld;
			}

			// Token: 0x06004909 RID: 18697 RVA: 0x006D0937 File Offset: 0x006CEB37
			internal bool <.cctor>b__15_1()
			{
				return Main.getGoodWorld;
			}

			// Token: 0x0600490A RID: 18698 RVA: 0x006C1229 File Offset: 0x006BF429
			internal bool <.cctor>b__15_2()
			{
				return SpecialSeedFeatures.Mechdusa;
			}

			// Token: 0x040075E7 RID: 30183
			public static readonly ItemVariants.<>c <>9 = new ItemVariants.<>c();
		}
	}
}
