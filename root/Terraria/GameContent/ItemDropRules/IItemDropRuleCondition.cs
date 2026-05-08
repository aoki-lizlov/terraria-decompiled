using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002FA RID: 762
	public interface IItemDropRuleCondition : IProvideItemConditionDescription
	{
		// Token: 0x06002698 RID: 9880
		bool CanDrop(DropAttemptInfo info);

		// Token: 0x06002699 RID: 9881
		bool CanShowItemDropInUI();
	}
}
