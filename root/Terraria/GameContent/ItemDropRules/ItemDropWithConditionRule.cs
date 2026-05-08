using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030B RID: 779
	public class ItemDropWithConditionRule : CommonDrop
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x0056071B File Offset: 0x0055E91B
		public ItemDropWithConditionRule(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition condition, int chanceNumerator = 1)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
			this.condition = condition;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00560732 File Offset: 0x0055E932
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x00560740 File Offset: 0x0055E940
		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(this.condition);
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = num * dropRateInfoChainFeed.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, num2, dropRateInfoChainFeed.conditions));
			Chains.ReportDroprates(base.ChainedRules, num, drops, dropRateInfoChainFeed);
		}

		// Token: 0x040050B9 RID: 20665
		public IItemDropRuleCondition condition;
	}
}
