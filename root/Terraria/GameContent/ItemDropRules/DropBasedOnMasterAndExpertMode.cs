using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000307 RID: 775
	public class DropBasedOnMasterAndExpertMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0056009B File Offset: 0x0055E29B
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x005600A3 File Offset: 0x0055E2A3
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

		// Token: 0x060026D0 RID: 9936 RVA: 0x005600AC File Offset: 0x0055E2AC
		public DropBasedOnMasterAndExpertMode(IItemDropRule ruleForDefault, IItemDropRule ruleForExpertMode, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForExpertmode = ruleForExpertMode;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x005600D4 File Offset: 0x0055E2D4
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsMasterMode)
			{
				return this.ruleForMasterMode.CanDrop(info);
			}
			if (info.IsExpertMode)
			{
				return this.ruleForExpertmode.CanDrop(info);
			}
			return this.ruleForDefault.CanDrop(info);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0056010C File Offset: 0x0055E30C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0056012A File Offset: 0x0055E32A
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsMasterMode)
			{
				return resolveAction(this.ruleForMasterMode, info);
			}
			if (info.IsExpertMode)
			{
				return resolveAction(this.ruleForExpertmode, info);
			}
			return resolveAction(this.ruleForDefault, info);
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x00560168 File Offset: 0x0055E368
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(new Conditions.IsMasterMode());
			this.ruleForMasterMode.ReportDroprates(drops, dropRateInfoChainFeed);
			DropRateInfoChainFeed dropRateInfoChainFeed2 = ratesInfo.With(1f);
			dropRateInfoChainFeed2.AddCondition(new Conditions.NotMasterMode());
			dropRateInfoChainFeed2.AddCondition(new Conditions.IsExpert());
			this.ruleForExpertmode.ReportDroprates(drops, dropRateInfoChainFeed2);
			DropRateInfoChainFeed dropRateInfoChainFeed3 = ratesInfo.With(1f);
			dropRateInfoChainFeed3.AddCondition(new Conditions.NotMasterMode());
			dropRateInfoChainFeed3.AddCondition(new Conditions.NotExpert());
			this.ruleForDefault.ReportDroprates(drops, dropRateInfoChainFeed3);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050B1 RID: 20657
		public IItemDropRule ruleForDefault;

		// Token: 0x040050B2 RID: 20658
		public IItemDropRule ruleForExpertmode;

		// Token: 0x040050B3 RID: 20659
		public IItemDropRule ruleForMasterMode;

		// Token: 0x040050B4 RID: 20660
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
