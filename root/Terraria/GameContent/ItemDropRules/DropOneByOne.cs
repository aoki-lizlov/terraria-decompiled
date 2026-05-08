using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030D RID: 781
	public class DropOneByOne : IItemDropRule
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x0056082A File Offset: 0x0055EA2A
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x00560832 File Offset: 0x0055EA32
		public List<IItemDropRuleChainAttempt> ChainedRules
		{
			[CompilerGenerated]
			get
			{
				return this.<ChainedRules>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChainedRules>k__BackingField = value;
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0056083B File Offset: 0x0055EA3B
		public DropOneByOne(int itemId, DropOneByOne.Parameters parameters)
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
			this.parameters = parameters;
			this.itemId = itemId;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0056085C File Offset: 0x0055EA5C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.parameters.ChanceDenominator) < this.parameters.ChanceNumerator)
			{
				int num = info.rng.Next(this.parameters.MinimumItemDropsCount, this.parameters.MaximumItemDropsCount + 1);
				int activePlayersCount = Main.CurrentFrameFlags.ActivePlayersCount;
				int num2 = this.parameters.MinimumStackPerChunkBase + activePlayersCount * this.parameters.BonusMinDropsPerChunkPerPlayer;
				int num3 = this.parameters.MaximumStackPerChunkBase + activePlayersCount * this.parameters.BonusMaxDropsPerChunkPerPlayer;
				for (int i = 0; i < num; i++)
				{
					CommonCode.DropItemFromNPC(info.npc, this.itemId, info.rng.Next(num2, num3 + 1), true);
				}
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.FailedRandomRoll
			};
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00560944 File Offset: 0x0055EB44
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float personalDropRate = this.parameters.GetPersonalDropRate();
			float num = personalDropRate * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.parameters.MinimumItemDropsCount * (this.parameters.MinimumStackPerChunkBase + this.parameters.BonusMinDropsPerChunkPerPlayer), this.parameters.MaximumItemDropsCount * (this.parameters.MaximumStackPerChunkBase + this.parameters.BonusMaxDropsPerChunkPerPlayer), num, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, personalDropRate, drops, ratesInfo);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x040050BC RID: 20668
		public int itemId;

		// Token: 0x040050BD RID: 20669
		public DropOneByOne.Parameters parameters;

		// Token: 0x040050BE RID: 20670
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;

		// Token: 0x0200082D RID: 2093
		public struct Parameters
		{
			// Token: 0x06004340 RID: 17216 RVA: 0x006C0CDF File Offset: 0x006BEEDF
			public float GetPersonalDropRate()
			{
				return (float)this.ChanceNumerator / (float)this.ChanceDenominator;
			}

			// Token: 0x04007272 RID: 29298
			public int ChanceNumerator;

			// Token: 0x04007273 RID: 29299
			public int ChanceDenominator;

			// Token: 0x04007274 RID: 29300
			public int MinimumItemDropsCount;

			// Token: 0x04007275 RID: 29301
			public int MaximumItemDropsCount;

			// Token: 0x04007276 RID: 29302
			public int MinimumStackPerChunkBase;

			// Token: 0x04007277 RID: 29303
			public int MaximumStackPerChunkBase;

			// Token: 0x04007278 RID: 29304
			public int BonusMinDropsPerChunkPerPlayer;

			// Token: 0x04007279 RID: 29305
			public int BonusMaxDropsPerChunkPerPlayer;
		}
	}
}
