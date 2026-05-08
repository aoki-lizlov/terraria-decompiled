using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002FD RID: 765
	public class CommonDrop : IItemDropRule
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x0055F8B0 File Offset: 0x0055DAB0
		// (set) Token: 0x060026A0 RID: 9888 RVA: 0x0055F8B8 File Offset: 0x0055DAB8
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

		// Token: 0x060026A1 RID: 9889 RVA: 0x0055F8C1 File Offset: 0x0055DAC1
		public CommonDrop(int itemId, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1)
		{
			this.itemId = itemId;
			this.chanceDenominator = chanceDenominator;
			this.amountDroppedMinimum = amountDroppedMinimum;
			this.amountDroppedMaximum = amountDroppedMaximum;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000379E9 File Offset: 0x00035BE9
		public virtual bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0055F8FC File Offset: 0x0055DAFC
		public virtual ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItemFromNPC(info.npc, this.itemId, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), false);
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

		// Token: 0x060026A4 RID: 9892 RVA: 0x0055F970 File Offset: 0x0055DB70
		public virtual void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = num * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, num2, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x0400509E RID: 20638
		public int itemId;

		// Token: 0x0400509F RID: 20639
		public int chanceDenominator;

		// Token: 0x040050A0 RID: 20640
		public int amountDroppedMinimum;

		// Token: 0x040050A1 RID: 20641
		public int amountDroppedMaximum;

		// Token: 0x040050A2 RID: 20642
		public int chanceNumerator;

		// Token: 0x040050A3 RID: 20643
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
