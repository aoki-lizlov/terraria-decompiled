using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000312 RID: 786
	public static class Chains
	{
		// Token: 0x06002710 RID: 10000 RVA: 0x00560EA4 File Offset: 0x0055F0A4
		public static void ReportDroprates(List<IItemDropRuleChainAttempt> ChainedRules, float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			foreach (IItemDropRuleChainAttempt itemDropRuleChainAttempt in ChainedRules)
			{
				itemDropRuleChainAttempt.ReportDroprates(personalDropRate, drops, ratesInfo);
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00560EF4 File Offset: 0x0055F0F4
		public static IItemDropRule OnFailedRoll(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfFailedRandomRoll(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x00560F09 File Offset: 0x0055F109
		public static IItemDropRule OnSuccess(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfSucceeded(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00560F1E File Offset: 0x0055F11E
		public static IItemDropRule OnFailedConditions(this IItemDropRule rule, IItemDropRule ruleToChain, bool hideLootReport = false)
		{
			rule.ChainedRules.Add(new Chains.TryIfDoesntFillConditions(ruleToChain, hideLootReport));
			return ruleToChain;
		}

		// Token: 0x0200082E RID: 2094
		public class TryIfFailedRandomRoll : IItemDropRuleChainAttempt
		{
			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06004341 RID: 17217 RVA: 0x006C0CF0 File Offset: 0x006BEEF0
			// (set) Token: 0x06004342 RID: 17218 RVA: 0x006C0CF8 File Offset: 0x006BEEF8
			public IItemDropRule RuleToChain
			{
				[CompilerGenerated]
				get
				{
					return this.<RuleToChain>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<RuleToChain>k__BackingField = value;
				}
			}

			// Token: 0x06004343 RID: 17219 RVA: 0x006C0D01 File Offset: 0x006BEF01
			public TryIfFailedRandomRoll(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06004344 RID: 17220 RVA: 0x006C0D17 File Offset: 0x006BEF17
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.FailedRandomRoll;
			}

			// Token: 0x06004345 RID: 17221 RVA: 0x006C0D22 File Offset: 0x006BEF22
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(1f - personalDropRate));
			}

			// Token: 0x0400727A RID: 29306
			[CompilerGenerated]
			private IItemDropRule <RuleToChain>k__BackingField;

			// Token: 0x0400727B RID: 29307
			public bool hideLootReport;
		}

		// Token: 0x0200082F RID: 2095
		public class TryIfSucceeded : IItemDropRuleChainAttempt
		{
			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x06004346 RID: 17222 RVA: 0x006C0D47 File Offset: 0x006BEF47
			// (set) Token: 0x06004347 RID: 17223 RVA: 0x006C0D4F File Offset: 0x006BEF4F
			public IItemDropRule RuleToChain
			{
				[CompilerGenerated]
				get
				{
					return this.<RuleToChain>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<RuleToChain>k__BackingField = value;
				}
			}

			// Token: 0x06004348 RID: 17224 RVA: 0x006C0D58 File Offset: 0x006BEF58
			public TryIfSucceeded(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x06004349 RID: 17225 RVA: 0x006C0D6E File Offset: 0x006BEF6E
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.Success;
			}

			// Token: 0x0600434A RID: 17226 RVA: 0x006C0D79 File Offset: 0x006BEF79
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
			}

			// Token: 0x0400727C RID: 29308
			[CompilerGenerated]
			private IItemDropRule <RuleToChain>k__BackingField;

			// Token: 0x0400727D RID: 29309
			public bool hideLootReport;
		}

		// Token: 0x02000830 RID: 2096
		public class TryIfDoesntFillConditions : IItemDropRuleChainAttempt
		{
			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x0600434B RID: 17227 RVA: 0x006C0D98 File Offset: 0x006BEF98
			// (set) Token: 0x0600434C RID: 17228 RVA: 0x006C0DA0 File Offset: 0x006BEFA0
			public IItemDropRule RuleToChain
			{
				[CompilerGenerated]
				get
				{
					return this.<RuleToChain>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<RuleToChain>k__BackingField = value;
				}
			}

			// Token: 0x0600434D RID: 17229 RVA: 0x006C0DA9 File Offset: 0x006BEFA9
			public TryIfDoesntFillConditions(IItemDropRule rule, bool hideLootReport = false)
			{
				this.RuleToChain = rule;
				this.hideLootReport = hideLootReport;
			}

			// Token: 0x0600434E RID: 17230 RVA: 0x006C0DBF File Offset: 0x006BEFBF
			public bool CanChainIntoRule(ItemDropAttemptResult parentResult)
			{
				return parentResult.State == ItemDropAttemptResultState.DoesntFillConditions;
			}

			// Token: 0x0600434F RID: 17231 RVA: 0x006C0DCA File Offset: 0x006BEFCA
			public void ReportDroprates(float personalDropRate, List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
			{
				if (this.hideLootReport)
				{
					return;
				}
				this.RuleToChain.ReportDroprates(drops, ratesInfo.With(personalDropRate));
			}

			// Token: 0x0400727E RID: 29310
			[CompilerGenerated]
			private IItemDropRule <RuleToChain>k__BackingField;

			// Token: 0x0400727F RID: 29311
			public bool hideLootReport;
		}
	}
}
