using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030E RID: 782
	public class OneFromOptionsNotScaledWithLuckDropRule : IItemDropRule
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x005609D2 File Offset: 0x0055EBD2
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x005609DA File Offset: 0x0055EBDA
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

		// Token: 0x060026F9 RID: 9977 RVA: 0x005609E3 File Offset: 0x0055EBE3
		public OneFromOptionsNotScaledWithLuckDropRule(int chanceDenominator, int chanceNumerator, params int[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.dropIds = options;
			this.chanceNumerator = chanceNumerator;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00560A0C File Offset: 0x0055EC0C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (info.rng.Next(this.chanceDenominator) < this.chanceNumerator)
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

		// Token: 0x060026FC RID: 9980 RVA: 0x00560A7C File Offset: 0x0055EC7C
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

		// Token: 0x040050BF RID: 20671
		public int[] dropIds;

		// Token: 0x040050C0 RID: 20672
		public int chanceDenominator;

		// Token: 0x040050C1 RID: 20673
		public int chanceNumerator;

		// Token: 0x040050C2 RID: 20674
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
