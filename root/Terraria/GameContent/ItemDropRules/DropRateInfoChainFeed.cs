using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F4 RID: 756
	public struct DropRateInfoChainFeed
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x0055F455 File Offset: 0x0055D655
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0055F476 File Offset: 0x0055D676
		public DropRateInfoChainFeed(float droprate)
		{
			this.parentDroprateChance = droprate;
			this.conditions = null;
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0055F488 File Offset: 0x0055D688
		public DropRateInfoChainFeed With(float multiplier)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = new DropRateInfoChainFeed(this.parentDroprateChance * multiplier);
			if (this.conditions != null)
			{
				dropRateInfoChainFeed.conditions = new List<IItemDropRuleCondition>(this.conditions);
			}
			return dropRateInfoChainFeed;
		}

		// Token: 0x04005096 RID: 20630
		public float parentDroprateChance;

		// Token: 0x04005097 RID: 20631
		public List<IItemDropRuleCondition> conditions;
	}
}
