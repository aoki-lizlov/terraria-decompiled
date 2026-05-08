using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F5 RID: 757
	public interface IItemDropRule
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002690 RID: 9872
		List<IItemDropRuleChainAttempt> ChainedRules { get; }

		// Token: 0x06002691 RID: 9873
		bool CanDrop(DropAttemptInfo info);

		// Token: 0x06002692 RID: 9874
		void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo);

		// Token: 0x06002693 RID: 9875
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info);
	}
}
