using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000304 RID: 772
	public class DropBasedOnExtraGel : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x0055FD8E File Offset: 0x0055DF8E
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x0055FD96 File Offset: 0x0055DF96
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

		// Token: 0x060026BB RID: 9915 RVA: 0x0055FD9F File Offset: 0x0055DF9F
		public DropBasedOnExtraGel(IItemDropRule ruleForNormal, IItemDropRule ruleForExtraGel)
		{
			this.ruleForNormal = ruleForNormal;
			this.ruleForExtraGel = ruleForExtraGel;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x0055FDC0 File Offset: 0x0055DFC0
		public bool CanDrop(DropAttemptInfo info)
		{
			if (SpecialSeedFeatures.ShouldDropExtraGel)
			{
				return this.ruleForExtraGel.CanDrop(info);
			}
			return this.ruleForNormal.CanDrop(info);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0055FDE4 File Offset: 0x0055DFE4
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x0055FE02 File Offset: 0x0055E002
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (SpecialSeedFeatures.ShouldDropExtraGel)
			{
				return resolveAction(this.ruleForExtraGel, info);
			}
			return resolveAction(this.ruleForNormal, info);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x0055FE28 File Offset: 0x0055E028
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(new Conditions.DropExtraGel());
			this.ruleForExtraGel.ReportDroprates(drops, dropRateInfoChainFeed);
			DropRateInfoChainFeed dropRateInfoChainFeed2 = ratesInfo.With(1f);
			dropRateInfoChainFeed2.AddCondition(new Conditions.NotDropExtraGel());
			this.ruleForNormal.ReportDroprates(drops, dropRateInfoChainFeed2);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050A8 RID: 20648
		public IItemDropRule ruleForNormal;

		// Token: 0x040050A9 RID: 20649
		public IItemDropRule ruleForExtraGel;

		// Token: 0x040050AA RID: 20650
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
