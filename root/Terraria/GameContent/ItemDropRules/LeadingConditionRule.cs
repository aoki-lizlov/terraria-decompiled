using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030C RID: 780
	public class LeadingConditionRule : IItemDropRule
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x005607B2 File Offset: 0x0055E9B2
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x005607BA File Offset: 0x0055E9BA
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

		// Token: 0x060026ED RID: 9965 RVA: 0x005607C3 File Offset: 0x0055E9C3
		public LeadingConditionRule(IItemDropRuleCondition condition)
		{
			this.condition = condition;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x005607DD File Offset: 0x0055E9DD
		public bool CanDrop(DropAttemptInfo info)
		{
			return this.condition.CanDrop(info);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x005607EB File Offset: 0x0055E9EB
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			ratesInfo.AddCondition(this.condition);
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0056080C File Offset: 0x0055EA0C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x040050BA RID: 20666
		public IItemDropRuleCondition condition;

		// Token: 0x040050BB RID: 20667
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
