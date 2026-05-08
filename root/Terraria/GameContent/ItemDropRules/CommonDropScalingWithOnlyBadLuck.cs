using System;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002FF RID: 767
	public class CommonDropScalingWithOnlyBadLuck : CommonDrop
	{
		// Token: 0x060026A7 RID: 9895 RVA: 0x0055FA4C File Offset: 0x0055DC4C
		public CommonDropScalingWithOnlyBadLuck(int itemId, int chanceDenominator, int amountDroppedMinimum = 1, int amountDroppedMaximum = 1, int chanceNumerator = 1)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, chanceNumerator)
		{
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0055F9C8 File Offset: 0x0055DBC8
		public CommonDropScalingWithOnlyBadLuck(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0055FA5C File Offset: 0x0055DC5C
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollOnlyBadLuck(this.chanceDenominator) < this.chanceNumerator)
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
