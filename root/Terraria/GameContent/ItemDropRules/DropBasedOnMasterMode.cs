using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000306 RID: 774
	public class DropBasedOnMasterMode : IItemDropRule, INestedItemDropRule
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0055FF97 File Offset: 0x0055E197
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x0055FF9F File Offset: 0x0055E19F
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

		// Token: 0x060026C9 RID: 9929 RVA: 0x0055FFA8 File Offset: 0x0055E1A8
		public DropBasedOnMasterMode(IItemDropRule ruleForDefault, IItemDropRule ruleForMasterMode)
		{
			this.ruleForDefault = ruleForDefault;
			this.ruleForMasterMode = ruleForMasterMode;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0055FFC9 File Offset: 0x0055E1C9
		public bool CanDrop(DropAttemptInfo info)
		{
			if (info.IsMasterMode)
			{
				return this.ruleForMasterMode.CanDrop(info);
			}
			return this.ruleForDefault.CanDrop(info);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0055FFEC File Offset: 0x0055E1EC
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DidNotRunCode
			};
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0056000A File Offset: 0x0055E20A
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info, ItemDropRuleResolveAction resolveAction)
		{
			if (info.IsMasterMode)
			{
				return resolveAction(this.ruleForMasterMode, info);
			}
			return resolveAction(this.ruleForDefault, info);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00560030 File Offset: 0x0055E230
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			DropRateInfoChainFeed dropRateInfoChainFeed = ratesInfo.With(1f);
			dropRateInfoChainFeed.AddCondition(new Conditions.IsMasterMode());
			this.ruleForMasterMode.ReportDroprates(drops, dropRateInfoChainFeed);
			DropRateInfoChainFeed dropRateInfoChainFeed2 = ratesInfo.With(1f);
			dropRateInfoChainFeed2.AddCondition(new Conditions.NotMasterMode());
			this.ruleForDefault.ReportDroprates(drops, dropRateInfoChainFeed2);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050AE RID: 20654
		public IItemDropRule ruleForDefault;

		// Token: 0x040050AF RID: 20655
		public IItemDropRule ruleForMasterMode;

		// Token: 0x040050B0 RID: 20656
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
