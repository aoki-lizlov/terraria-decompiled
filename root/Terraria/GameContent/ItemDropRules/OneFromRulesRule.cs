using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000311 RID: 785
	public class OneFromRulesRule : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x00560D82 File Offset: 0x0055EF82
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x00560D8A File Offset: 0x0055EF8A
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

		// Token: 0x0600270B RID: 9995 RVA: 0x00560D93 File Offset: 0x0055EF93
		public OneFromRulesRule(int chanceDenominator, params IItemDropRule[] options)
		{
			this.chanceDenominator = chanceDenominator;
			this.options = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x00560DB4 File Offset: 0x0055EFB4
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x00560DD4 File Offset: 0x0055EFD4
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.rng.Next(this.chanceDenominator) == 0)
			{
				int num = info.rng.Next(this.options.Length);
				resolveAction(this.options[num], info);
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

		// Token: 0x0600270F RID: 9999 RVA: 0x00560E40 File Offset: 0x0055F040
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float num = 1f / (float)this.chanceDenominator;
			float num2 = 1f / (float)this.options.Length * num;
			for (int i = 0; i < this.options.Length; i++)
			{
				this.options[i].ReportDroprates(drops, ratesInfo.With(num2));
			}
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x040050CB RID: 20683
		public IItemDropRule[] options;

		// Token: 0x040050CC RID: 20684
		public int chanceDenominator;

		// Token: 0x040050CD RID: 20685
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
