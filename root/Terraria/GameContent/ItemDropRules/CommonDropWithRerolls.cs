using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000302 RID: 770
	public class CommonDropWithRerolls : CommonDrop
	{
		// Token: 0x060026B0 RID: 9904 RVA: 0x0055FBFF File Offset: 0x0055DDFF
		public CommonDropWithRerolls(int itemId, int chanceDenominator, int amountDroppedMinimum, int amountDroppedMaximum, int rerolls)
			: base(itemId, chanceDenominator, amountDroppedMinimum, amountDroppedMaximum, 1)
		{
			this.timesToRoll = rerolls + 1;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0055FC18 File Offset: 0x0055DE18
		public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			bool flag = false;
			for (int i = 0; i < this.timesToRoll; i++)
			{
				flag = flag || info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator;
			}
			if (flag)
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

		// Token: 0x060026B2 RID: 9906 RVA: 0x0055FCAC File Offset: 0x0055DEAC
		public override void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = 1f - num;
			float num3 = 1f;
			for (int i = 0; i < this.timesToRoll; i++)
			{
				num3 *= num2;
			}
			float num4 = 1f - num3;
			float num5 = num4 * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(this.itemId, this.amountDroppedMinimum, this.amountDroppedMaximum, num5, ratesInfo.conditions));
			Chains.ReportDroprates(base.ChainedRules, num4, drops, ratesInfo);
		}

		// Token: 0x040050A6 RID: 20646
		public int timesToRoll;
	}
}
