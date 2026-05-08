using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000309 RID: 777
	public class StatueMimicItemDropRule : IItemDropRule
	{
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x005603D6 File Offset: 0x0055E5D6
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x005603DE File Offset: 0x0055E5DE
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

		// Token: 0x060026DD RID: 9949 RVA: 0x005603E7 File Offset: 0x0055E5E7
		public StatueMimicItemDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x005603FA File Offset: 0x0055E5FA
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.ai[1] > 0f && info.npc.ai[1] < (float)ItemID.Count;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00560427 File Offset: 0x0055E627
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0056043C File Offset: 0x0055E63C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			int num = WorldGen.StatueStyleToItem((int)info.npc.ai[1]);
			CommonCode.DropItemFromNPC(info.npc, num, 1, false);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x040050B7 RID: 20663
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
