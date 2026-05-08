using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020002EA RID: 746
	public class LootSimulationItemCounter
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x0055EC88 File Offset: 0x0055CE88
		public LootSimulationItemCounter()
		{
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0055ECB0 File Offset: 0x0055CEB0
		public void AddItem(int itemId, int amount, bool expert)
		{
			if (expert)
			{
				this._itemCountsObtainedExpert[itemId] += (long)amount;
				return;
			}
			this._itemCountsObtained[itemId] += (long)amount;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0055ECDC File Offset: 0x0055CEDC
		public void Exclude(params int[] itemIds)
		{
			foreach (int num in itemIds)
			{
				this._itemCountsObtained[num] = 0L;
				this._itemCountsObtainedExpert[num] = 0L;
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0055ED11 File Offset: 0x0055CF11
		public void IncreaseTimesAttempted(int amount, bool expert)
		{
			if (expert)
			{
				this._totalTimesAttemptedExpert += (long)amount;
				return;
			}
			this._totalTimesAttempted += (long)amount;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0055ED38 File Offset: 0x0055CF38
		public string PrintCollectedItems(bool expert)
		{
			long[] collectionToUse = this._itemCountsObtained;
			long totalDropsAttempted = this._totalTimesAttempted;
			if (expert)
			{
				collectionToUse = this._itemCountsObtainedExpert;
				this._totalTimesAttempted = this._totalTimesAttemptedExpert;
			}
			IEnumerable<string> enumerable = from entry in collectionToUse.Select((long count, int itemId) => new { itemId, count })
				where entry.count > 0L
				select entry.itemId into itemId
				select string.Format("new ItemDropInfo(ItemID.{0}, {1}, {2})", ItemID.Search.GetName(itemId), collectionToUse[itemId], totalDropsAttempted);
			return string.Join(",\n", enumerable);
		}

		// Token: 0x04005073 RID: 20595
		private long[] _itemCountsObtained = new long[(int)ItemID.Count];

		// Token: 0x04005074 RID: 20596
		private long[] _itemCountsObtainedExpert = new long[(int)ItemID.Count];

		// Token: 0x04005075 RID: 20597
		private long _totalTimesAttempted;

		// Token: 0x04005076 RID: 20598
		private long _totalTimesAttemptedExpert;

		// Token: 0x0200082A RID: 2090
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06004323 RID: 17187 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06004324 RID: 17188 RVA: 0x006C0B95 File Offset: 0x006BED95
			internal string <PrintCollectedItems>b__3(int itemId)
			{
				return string.Format("new ItemDropInfo(ItemID.{0}, {1}, {2})", ItemID.Search.GetName(itemId), this.collectionToUse[itemId], this.totalDropsAttempted);
			}

			// Token: 0x0400726B RID: 29291
			public long[] collectionToUse;

			// Token: 0x0400726C RID: 29292
			public long totalDropsAttempted;
		}

		// Token: 0x0200082B RID: 2091
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004325 RID: 17189 RVA: 0x006C0BC4 File Offset: 0x006BEDC4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004326 RID: 17190 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004327 RID: 17191 RVA: 0x006C0BD0 File Offset: 0x006BEDD0
			internal <>f__AnonymousType3<int, long> <PrintCollectedItems>b__8_0(long count, int itemId)
			{
				return new { itemId, count };
			}

			// Token: 0x06004328 RID: 17192 RVA: 0x006C0BD9 File Offset: 0x006BEDD9
			internal bool <PrintCollectedItems>b__8_1(<>f__AnonymousType3<int, long> entry)
			{
				return entry.count > 0L;
			}

			// Token: 0x06004329 RID: 17193 RVA: 0x006C0BE5 File Offset: 0x006BEDE5
			internal int <PrintCollectedItems>b__8_2(<>f__AnonymousType3<int, long> entry)
			{
				return entry.itemId;
			}

			// Token: 0x0400726D RID: 29293
			public static readonly LootSimulationItemCounter.<>c <>9 = new LootSimulationItemCounter.<>c();

			// Token: 0x0400726E RID: 29294
			public static Func<long, int, <>f__AnonymousType3<int, long>> <>9__8_0;

			// Token: 0x0400726F RID: 29295
			public static Func<<>f__AnonymousType3<int, long>, bool> <>9__8_1;

			// Token: 0x04007270 RID: 29296
			public static Func<<>f__AnonymousType3<int, long>, int> <>9__8_2;
		}
	}
}
