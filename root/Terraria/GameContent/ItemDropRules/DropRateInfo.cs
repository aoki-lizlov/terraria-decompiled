using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F3 RID: 755
	public struct DropRateInfo
	{
		// Token: 0x0600268B RID: 9867 RVA: 0x0055F3E8 File Offset: 0x0055D5E8
		public DropRateInfo(int itemId, int stackMin, int stackMax, float dropRate, List<IItemDropRuleCondition> conditions = null)
		{
			this.itemId = itemId;
			this.stackMin = stackMin;
			this.stackMax = stackMax;
			this.dropRate = dropRate;
			this.conditions = null;
			if (conditions != null && conditions.Count > 0)
			{
				this.conditions = new List<IItemDropRuleCondition>(conditions);
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x0055F434 File Offset: 0x0055D634
		public void AddCondition(IItemDropRuleCondition condition)
		{
			if (this.conditions == null)
			{
				this.conditions = new List<IItemDropRuleCondition>();
			}
			this.conditions.Add(condition);
		}

		// Token: 0x04005091 RID: 20625
		public int itemId;

		// Token: 0x04005092 RID: 20626
		public int stackMin;

		// Token: 0x04005093 RID: 20627
		public int stackMax;

		// Token: 0x04005094 RID: 20628
		public float dropRate;

		// Token: 0x04005095 RID: 20629
		public List<IItemDropRuleCondition> conditions;
	}
}
