using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000323 RID: 803
	public static class SortingSteps
	{
		// Token: 0x0200088F RID: 2191
		public abstract class ACreativeItemSortStep : ICreativeItemSortStep, IEntrySortStep<Item>, IComparer<Item>
		{
			// Token: 0x060044B6 RID: 17590
			public abstract string GetDisplayNameKey();

			// Token: 0x060044B7 RID: 17591
			public abstract int Compare(Item x, Item y);

			// Token: 0x060044B8 RID: 17592 RVA: 0x0000357B File Offset: 0x0000177B
			protected ACreativeItemSortStep()
			{
			}
		}

		// Token: 0x02000890 RID: 2192
		public abstract class AStepByFittingFilter : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060044B9 RID: 17593 RVA: 0x006C34E4 File Offset: 0x006C16E4
			public override int Compare(Item x, Item y)
			{
				int num = this.FitsFilter(x).CompareTo(this.FitsFilter(y));
				if (num == 0)
				{
					num = 1;
				}
				return num;
			}

			// Token: 0x060044BA RID: 17594
			public abstract bool FitsFilter(Item item);

			// Token: 0x060044BB RID: 17595 RVA: 0x006C350E File Offset: 0x006C170E
			public virtual int CompareWhenBothFit(Item x, Item y)
			{
				return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060044BC RID: 17596 RVA: 0x006C3522 File Offset: 0x006C1722
			protected AStepByFittingFilter()
			{
			}
		}

		// Token: 0x02000891 RID: 2193
		public class Blocks : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060044BD RID: 17597 RVA: 0x006C352A File Offset: 0x006C172A
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Blocks";
			}

			// Token: 0x060044BE RID: 17598 RVA: 0x006C3531 File Offset: 0x006C1731
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && !Main.tileFrameImportant[item.createTile];
			}

			// Token: 0x060044BF RID: 17599 RVA: 0x006C354D File Offset: 0x006C174D
			public Blocks()
			{
			}
		}

		// Token: 0x02000892 RID: 2194
		public class Walls : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060044C0 RID: 17600 RVA: 0x006C3555 File Offset: 0x006C1755
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Walls";
			}

			// Token: 0x060044C1 RID: 17601 RVA: 0x006C355C File Offset: 0x006C175C
			public override bool FitsFilter(Item item)
			{
				return item.createWall >= 0;
			}

			// Token: 0x060044C2 RID: 17602 RVA: 0x006C354D File Offset: 0x006C174D
			public Walls()
			{
			}
		}

		// Token: 0x02000893 RID: 2195
		public class PlaceableObjects : SortingSteps.AStepByFittingFilter
		{
			// Token: 0x060044C3 RID: 17603 RVA: 0x006C356A File Offset: 0x006C176A
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_PlaceableObjects";
			}

			// Token: 0x060044C4 RID: 17604 RVA: 0x006C3571 File Offset: 0x006C1771
			public override bool FitsFilter(Item item)
			{
				return item.createTile >= 0 && Main.tileFrameImportant[item.createTile];
			}

			// Token: 0x060044C5 RID: 17605 RVA: 0x006C354D File Offset: 0x006C174D
			public PlaceableObjects()
			{
			}
		}

		// Token: 0x02000894 RID: 2196
		public class ByUnlockStatus : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060044C6 RID: 17606 RVA: 0x006C358A File Offset: 0x006C178A
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_UnlockedFirst";
			}

			// Token: 0x060044C7 RID: 17607 RVA: 0x006C3594 File Offset: 0x006C1794
			public override int Compare(Item x, Item y)
			{
				ItemsSacrificedUnlocksTracker itemSacrifices = Main.LocalPlayerCreativeTracker.ItemSacrifices;
				bool flag = itemSacrifices.IsNewlyResearched(x.type);
				bool flag2 = itemSacrifices.IsNewlyResearched(y.type);
				if (flag != flag2)
				{
					if (!flag)
					{
						return 1;
					}
					return -1;
				}
				else
				{
					bool flag3 = itemSacrifices.IsFullyResearched(x.type);
					bool flag4 = itemSacrifices.IsFullyResearched(y.type);
					if (flag3 == flag4)
					{
						return 0;
					}
					if (!flag3)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x060044C8 RID: 17608 RVA: 0x006C3522 File Offset: 0x006C1722
			public ByUnlockStatus()
			{
			}
		}

		// Token: 0x02000895 RID: 2197
		public class ByCreativeSortingId : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060044C9 RID: 17609 RVA: 0x006C35F9 File Offset: 0x006C17F9
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_SortingID";
			}

			// Token: 0x060044CA RID: 17610 RVA: 0x006C3600 File Offset: 0x006C1800
			public override int Compare(Item x, Item y)
			{
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup = ContentSamples.ItemCreativeSortingId[x.type];
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup2 = ContentSamples.ItemCreativeSortingId[y.type];
				int num = itemGroupAndOrderInGroup.Group.CompareTo(itemGroupAndOrderInGroup2.Group);
				if (num == 0)
				{
					num = itemGroupAndOrderInGroup.OrderInGroup.CompareTo(itemGroupAndOrderInGroup2.OrderInGroup);
				}
				return num;
			}

			// Token: 0x060044CB RID: 17611 RVA: 0x006C3522 File Offset: 0x006C1722
			public ByCreativeSortingId()
			{
			}
		}

		// Token: 0x02000896 RID: 2198
		public class Alphabetical : SortingSteps.ACreativeItemSortStep
		{
			// Token: 0x060044CC RID: 17612 RVA: 0x006C3664 File Offset: 0x006C1864
			public override string GetDisplayNameKey()
			{
				return "CreativePowers.Sort_Alphabetical";
			}

			// Token: 0x060044CD RID: 17613 RVA: 0x006C366C File Offset: 0x006C186C
			public override int Compare(Item x, Item y)
			{
				string name = x.Name;
				string name2 = y.Name;
				return name.CompareTo(name2);
			}

			// Token: 0x060044CE RID: 17614 RVA: 0x006C3522 File Offset: 0x006C1722
			public Alphabetical()
			{
			}
		}
	}
}
