using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000305 RID: 773
	public class DropBasedOnExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0055FE93 File Offset: 0x0055E093
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x0055FE9B File Offset: 0x0055E09B
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

		// Token: 0x060026C2 RID: 9922 RVA: 0x0055FEA4 File Offset: 0x0055E0A4
		public DropBasedOnExpertMode(IItemDropRule ruleForNormalMode, IItemDropRule ruleForExpertMode)
		{
			this.ruleForNormalMode = ruleForNormalMode;
			this.ruleForExpertMode = ruleForExpertMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0055FEC5 File Offset: 0x0055E0C5
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsExpertMode)
			{
				return this.ruleForExpertMode.CanDrop(info);
			}
			return this.ruleForNormalMode.CanDrop(info);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0055FEE8 File Offset: 0x0055E0E8
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0055FF06 File Offset: 0x0055E106
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsExpertMode)
			{
				return resolveAction(this.ruleForExpertMode, info);
			}
			return resolveAction(this.ruleForNormalMode, info);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0055FF2C File Offset: 0x0055E12C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(new Conditions.IsExpert());
			this.ruleForExpertMode.ReportDroprates(drops, dropRateInfoChainFeed);
			DropRateInfoChainFeed dropRateInfoChainFeed2 = ratesInfo.With(1f);
			dropRateInfoChainFeed2.AddCondition(new Conditions.NotExpert());
			this.ruleForNormalMode.ReportDroprates(drops, dropRateInfoChainFeed2);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050AB RID: 20651
		public IItemDropRule ruleForNormalMode;

		// Token: 0x040050AC RID: 20652
		public IItemDropRule ruleForExpertMode;

		// Token: 0x040050AD RID: 20653
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
