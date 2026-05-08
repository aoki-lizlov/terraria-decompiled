using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002FE RID: 766
	public class CommonDropNotScalingWithLuck : CommonDrop
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x0055F9C8 File Offset: 0x0055DBC8
		public CommonDropNotScalingWithLuck(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0055F9D8 File Offset: 0x0055DBD8
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
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
	}
}
