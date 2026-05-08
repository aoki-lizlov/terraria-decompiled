using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F6 RID: 758
	public interface INestedItemDropRule
	{
		// Token: 0x06002694 RID: 9876
		ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction);
	}
}
