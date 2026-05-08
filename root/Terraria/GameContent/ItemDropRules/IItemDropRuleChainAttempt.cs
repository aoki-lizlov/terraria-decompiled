using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F9 RID: 761
	public interface IItemDropRuleChainAttempt
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002695 RID: 9877
		IItemDropRule RuleToChain { get; }

		// Token: 0x06002696 RID: 9878
		bool CanChainIntoRule(ItemDropAttemptResult parentResult);

		// Token: 0x06002697 RID: 9879
		void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);
	}
}
