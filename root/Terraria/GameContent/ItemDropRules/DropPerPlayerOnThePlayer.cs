using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000301 RID: 769
	public class DropPerPlayerOnThePlayer : CommonDrop
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x0055FB74 File Offset: 0x0055DD74
		public DropPerPlayerOnThePlayer(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, IItemDropRuleCondition optionalCondition)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.condition = optionalCondition;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0055FB8A File Offset: 0x0055DD8A
		public override bool CanDrop(DropAttemptInfo info)
		{
			return this.condition == null || this.condition.CanDrop(info);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0055FBA4 File Offset: 0x0055DDA4
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			CommonCode.DropItemForEachInteractingPlayerOnThePlayer(info.npc, this.itemId, info.rng, this.chanceNumerator, this.chanceDenominator, info.rng.Next(this.amountDroppedMinimum, this.amountDroppedMaximum + 1), true);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x040050A5 RID: 20645
		public IItemDropRuleCondition condition;
	}
}
