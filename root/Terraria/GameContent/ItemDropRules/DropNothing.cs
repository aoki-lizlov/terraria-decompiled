using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000303 RID: 771
	public class DropNothing : IItemDropRule
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x0055FD35 File Offset: 0x0055DF35
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x0055FD3D File Offset: 0x0055DF3D
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

		// Token: 0x060026B5 RID: 9909 RVA: 0x0055FD46 File Offset: 0x0055DF46
		public DropNothing()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool CanDrop(DropAttemptInfo info)
		{
			return false;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0055FD5C File Offset: 0x0055DF5C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.DoesntFillConditions
			};
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0055FD7A File Offset: 0x0055DF7A
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050A7 RID: 20647
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
