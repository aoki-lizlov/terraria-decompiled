using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000332 RID: 818
	public static class SortingSteps
	{
		// Token: 0x020008B5 RID: 2229
		public class ByNetId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060045F9 RID: 17913 RVA: 0x006C61B8 File Offset: 0x006C43B8
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				return npcnetIdBestiaryInfoElement.NetId.CompareTo(npcnetIdBestiaryInfoElement2.NetId);
			}

			// Token: 0x060045FA RID: 17914 RVA: 0x006C6251 File Offset: 0x006C4451
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_ID";
			}

			// Token: 0x060045FB RID: 17915 RVA: 0x0000357B File Offset: 0x0000177B
			public ByNetId()
			{
			}

			// Token: 0x02000ADE RID: 2782
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CDF RID: 19679 RVA: 0x006DB279 File Offset: 0x006D9479
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CE0 RID: 19680 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CE1 RID: 19681 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_0(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x06004CE2 RID: 19682 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_1(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x04007890 RID: 30864
				public static readonly SortingSteps.ByNetId.<>c <>9 = new SortingSteps.ByNetId.<>c();

				// Token: 0x04007891 RID: 30865
				public static Func<IBestiaryInfoElement, bool> <>9__2_0;

				// Token: 0x04007892 RID: 30866
				public static Func<IBestiaryInfoElement, bool> <>9__2_1;
			}
		}

		// Token: 0x020008B6 RID: 2230
		public class ByUnlockState : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060045FC RID: 17916 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool HiddenFromSortOptions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060045FD RID: 17917 RVA: 0x006C6258 File Offset: 0x006C4458
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = x.UIInfoProvider.GetEntryUICollectionInfo();
				BestiaryUICollectionInfo entryUICollectionInfo2 = y.UIInfoProvider.GetEntryUICollectionInfo();
				return y.Icon.GetUnlockState(entryUICollectionInfo2).CompareTo(x.Icon.GetUnlockState(entryUICollectionInfo));
			}

			// Token: 0x060045FE RID: 17918 RVA: 0x006C629D File Offset: 0x006C449D
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Unlocks";
			}

			// Token: 0x060045FF RID: 17919 RVA: 0x0000357B File Offset: 0x0000177B
			public ByUnlockState()
			{
			}
		}

		// Token: 0x020008B7 RID: 2231
		public class ByBestiarySortingId : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06004600 RID: 17920 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004601 RID: 17921 RVA: 0x006C62A4 File Offset: 0x006C44A4
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int num = ContentSamples.NpcBestiarySortingId[npcnetIdBestiaryInfoElement.NetId];
				int num2 = ContentSamples.NpcBestiarySortingId[npcnetIdBestiaryInfoElement2.NetId];
				return num.CompareTo(num2);
			}

			// Token: 0x06004602 RID: 17922 RVA: 0x006C6353 File Offset: 0x006C4553
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_BestiaryID";
			}

			// Token: 0x06004603 RID: 17923 RVA: 0x0000357B File Offset: 0x0000177B
			public ByBestiarySortingId()
			{
			}

			// Token: 0x02000ADF RID: 2783
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CE3 RID: 19683 RVA: 0x006DB290 File Offset: 0x006D9490
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CE4 RID: 19684 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CE5 RID: 19685 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_0(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x06004CE6 RID: 19686 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_1(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x04007893 RID: 30867
				public static readonly SortingSteps.ByBestiarySortingId.<>c <>9 = new SortingSteps.ByBestiarySortingId.<>c();

				// Token: 0x04007894 RID: 30868
				public static Func<IBestiaryInfoElement, bool> <>9__2_0;

				// Token: 0x04007895 RID: 30869
				public static Func<IBestiaryInfoElement, bool> <>9__2_1;
			}
		}

		// Token: 0x020008B8 RID: 2232
		public class ByBestiaryRarity : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x06004604 RID: 17924 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004605 RID: 17925 RVA: 0x006C635C File Offset: 0x006C455C
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				int num = ContentSamples.NpcBestiaryRarityStars[npcnetIdBestiaryInfoElement.NetId];
				return ContentSamples.NpcBestiaryRarityStars[npcnetIdBestiaryInfoElement2.NetId].CompareTo(num);
			}

			// Token: 0x06004606 RID: 17926 RVA: 0x006C640B File Offset: 0x006C460B
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Rarity";
			}

			// Token: 0x06004607 RID: 17927 RVA: 0x0000357B File Offset: 0x0000177B
			public ByBestiaryRarity()
			{
			}

			// Token: 0x02000AE0 RID: 2784
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CE7 RID: 19687 RVA: 0x006DB29C File Offset: 0x006D949C
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CE8 RID: 19688 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CE9 RID: 19689 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_0(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x06004CEA RID: 19690 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_1(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x04007896 RID: 30870
				public static readonly SortingSteps.ByBestiaryRarity.<>c <>9 = new SortingSteps.ByBestiaryRarity.<>c();

				// Token: 0x04007897 RID: 30871
				public static Func<IBestiaryInfoElement, bool> <>9__2_0;

				// Token: 0x04007898 RID: 30872
				public static Func<IBestiaryInfoElement, bool> <>9__2_1;
			}
		}

		// Token: 0x020008B9 RID: 2233
		public class Alphabetical : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x06004608 RID: 17928 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004609 RID: 17929 RVA: 0x006C6414 File Offset: 0x006C4614
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				NPCNetIdBestiaryInfoElement npcnetIdBestiaryInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => element is NPCNetIdBestiaryInfoElement) as NPCNetIdBestiaryInfoElement;
				if (npcnetIdBestiaryInfoElement == null && npcnetIdBestiaryInfoElement2 != null)
				{
					return 1;
				}
				if (npcnetIdBestiaryInfoElement2 == null && npcnetIdBestiaryInfoElement != null)
				{
					return -1;
				}
				if (npcnetIdBestiaryInfoElement == null || npcnetIdBestiaryInfoElement2 == null)
				{
					return 0;
				}
				string textValue = Language.GetTextValue(ContentSamples.NpcsByNetId[npcnetIdBestiaryInfoElement.NetId].TypeName);
				string textValue2 = Language.GetTextValue(ContentSamples.NpcsByNetId[npcnetIdBestiaryInfoElement2.NetId].TypeName);
				return textValue.CompareTo(textValue2);
			}

			// Token: 0x0600460A RID: 17930 RVA: 0x006C64D4 File Offset: 0x006C46D4
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Alphabetical";
			}

			// Token: 0x0600460B RID: 17931 RVA: 0x0000357B File Offset: 0x0000177B
			public Alphabetical()
			{
			}

			// Token: 0x02000AE1 RID: 2785
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CEB RID: 19691 RVA: 0x006DB2A8 File Offset: 0x006D94A8
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CEC RID: 19692 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CED RID: 19693 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_0(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x06004CEE RID: 19694 RVA: 0x006DB285 File Offset: 0x006D9485
				internal bool <Compare>b__2_1(IBestiaryInfoElement element)
				{
					return element is NPCNetIdBestiaryInfoElement;
				}

				// Token: 0x04007899 RID: 30873
				public static readonly SortingSteps.Alphabetical.<>c <>9 = new SortingSteps.Alphabetical.<>c();

				// Token: 0x0400789A RID: 30874
				public static Func<IBestiaryInfoElement, bool> <>9__2_0;

				// Token: 0x0400789B RID: 30875
				public static Func<IBestiaryInfoElement, bool> <>9__2_1;
			}
		}

		// Token: 0x020008BA RID: 2234
		public abstract class ByStat : IBestiarySortStep, IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
		{
			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x0600460C RID: 17932 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			public bool HiddenFromSortOptions
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600460D RID: 17933 RVA: 0x006C64DC File Offset: 0x006C46DC
			public int Compare(BestiaryEntry x, BestiaryEntry y)
			{
				NPCStatsReportInfoElement npcstatsReportInfoElement = x.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				NPCStatsReportInfoElement npcstatsReportInfoElement2 = y.Info.FirstOrDefault((IBestiaryInfoElement element) => this.IsAStatsCardINeed(element)) as NPCStatsReportInfoElement;
				if (npcstatsReportInfoElement == null && npcstatsReportInfoElement2 != null)
				{
					return 1;
				}
				if (npcstatsReportInfoElement2 == null && npcstatsReportInfoElement != null)
				{
					return -1;
				}
				if (npcstatsReportInfoElement == null || npcstatsReportInfoElement2 == null)
				{
					return 0;
				}
				return this.Compare(npcstatsReportInfoElement, npcstatsReportInfoElement2);
			}

			// Token: 0x0600460E RID: 17934
			public abstract int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY);

			// Token: 0x0600460F RID: 17935
			public abstract string GetDisplayNameKey();

			// Token: 0x06004610 RID: 17936 RVA: 0x006C6543 File Offset: 0x006C4743
			private bool IsAStatsCardINeed(IBestiaryInfoElement element)
			{
				return element is NPCStatsReportInfoElement;
			}

			// Token: 0x06004611 RID: 17937 RVA: 0x0000357B File Offset: 0x0000177B
			protected ByStat()
			{
			}

			// Token: 0x06004612 RID: 17938 RVA: 0x006C6550 File Offset: 0x006C4750
			[CompilerGenerated]
			private bool <Compare>b__2_0(IBestiaryInfoElement element)
			{
				return this.IsAStatsCardINeed(element);
			}

			// Token: 0x06004613 RID: 17939 RVA: 0x006C6550 File Offset: 0x006C4750
			[CompilerGenerated]
			private bool <Compare>b__2_1(IBestiaryInfoElement element)
			{
				return this.IsAStatsCardINeed(element);
			}
		}

		// Token: 0x020008BB RID: 2235
		public class ByAttack : SortingSteps.ByStat
		{
			// Token: 0x06004614 RID: 17940 RVA: 0x006C6559 File Offset: 0x006C4759
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Damage.CompareTo(cardX.Damage);
			}

			// Token: 0x06004615 RID: 17941 RVA: 0x006C656C File Offset: 0x006C476C
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Attack";
			}

			// Token: 0x06004616 RID: 17942 RVA: 0x006C6573 File Offset: 0x006C4773
			public ByAttack()
			{
			}
		}

		// Token: 0x020008BC RID: 2236
		public class ByDefense : SortingSteps.ByStat
		{
			// Token: 0x06004617 RID: 17943 RVA: 0x006C657B File Offset: 0x006C477B
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.Defense.CompareTo(cardX.Defense);
			}

			// Token: 0x06004618 RID: 17944 RVA: 0x006C658E File Offset: 0x006C478E
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Defense";
			}

			// Token: 0x06004619 RID: 17945 RVA: 0x006C6573 File Offset: 0x006C4773
			public ByDefense()
			{
			}
		}

		// Token: 0x020008BD RID: 2237
		public class ByCoins : SortingSteps.ByStat
		{
			// Token: 0x0600461A RID: 17946 RVA: 0x006C6595 File Offset: 0x006C4795
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.MonetaryValue.CompareTo(cardX.MonetaryValue);
			}

			// Token: 0x0600461B RID: 17947 RVA: 0x006C65A8 File Offset: 0x006C47A8
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_Coins";
			}

			// Token: 0x0600461C RID: 17948 RVA: 0x006C6573 File Offset: 0x006C4773
			public ByCoins()
			{
			}
		}

		// Token: 0x020008BE RID: 2238
		public class ByHP : SortingSteps.ByStat
		{
			// Token: 0x0600461D RID: 17949 RVA: 0x006C65AF File Offset: 0x006C47AF
			public override int Compare(NPCStatsReportInfoElement cardX, NPCStatsReportInfoElement cardY)
			{
				return cardY.LifeMax.CompareTo(cardX.LifeMax);
			}

			// Token: 0x0600461E RID: 17950 RVA: 0x006C65C2 File Offset: 0x006C47C2
			public override string GetDisplayNameKey()
			{
				return "BestiaryInfo.Sort_HitPoints";
			}

			// Token: 0x0600461F RID: 17951 RVA: 0x006C6573 File Offset: 0x006C4773
			public ByHP()
			{
			}
		}
	}
}
