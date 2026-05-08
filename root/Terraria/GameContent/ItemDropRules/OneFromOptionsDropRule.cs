using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030F RID: 783
	public class OneFromOptionsDropRule : IItemDropRule
	{
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00560AF1 File Offset: 0x0055ECF1
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x00560AF9 File Offset: 0x0055ECF9
		public List<IItemDropRuleChainAttempt> ChainedRules
		{
			[CompilerGenerated]
			get
			{
				return this.<ChainedRules>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ChainedRules>k__BackingField = value;
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x00560B02 File Offset: 0x0055ED02
		public OneFromOptionsDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.chanceNumerator = chanceNumerator;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00560B2C File Offset: 0x0055ED2C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.player.RollLuck(this.chanceDenominator) < this.chanceNumerator)
			{
				CommonCode.DropItemFromNPC(info.npc, this.dropIds[info.rng.Next(this.dropIds.Length)], 1, false);
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

		// Token: 0x06002702 RID: 9986 RVA: 0x00560B9C File Offset: 0x0055ED9C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = (float)this.chanceNumerator / (float)this.chanceDenominator;
			float num2 = num * ratesInfo.parentDroprateChance;
			float num3 = 1f / (float)this.dropIds.Length * num2;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, num3, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x040050C3 RID: 20675
		public int[] dropIds;

		// Token: 0x040050C4 RID: 20676
		public int chanceDenominator;

		// Token: 0x040050C5 RID: 20677
		public int chanceNumerator;

		// Token: 0x040050C6 RID: 20678
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
