using System;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x02000481 RID: 1153
	public class FishingConditions
	{
		// Token: 0x06003353 RID: 13139 RVA: 0x0000357B File Offset: 0x0000177B
		public FishingConditions()
		{
		}

		// Token: 0x02000978 RID: 2424
		public class QuestFishCondition : AFishingCondition
		{
			// Token: 0x06004938 RID: 18744 RVA: 0x006D0CB9 File Offset: 0x006CEEB9
			public override bool Matches(FishingContext context)
			{
				return context.Fisher.questFish == this.CheckedType;
			}

			// Token: 0x06004939 RID: 18745 RVA: 0x006D0CCE File Offset: 0x006CEECE
			public QuestFishCondition()
			{
			}

			// Token: 0x04007618 RID: 30232
			public int CheckedType;
		}

		// Token: 0x02000979 RID: 2425
		public class QuestFishConditionRemix : AFishingCondition
		{
			// Token: 0x0600493A RID: 18746 RVA: 0x006D0CD6 File Offset: 0x006CEED6
			public override bool Matches(FishingContext context)
			{
				return context.Fisher.questFish == this.CheckedType && Main.remixWorld;
			}

			// Token: 0x0600493B RID: 18747 RVA: 0x006D0CCE File Offset: 0x006CEECE
			public QuestFishConditionRemix()
			{
			}

			// Token: 0x04007619 RID: 30233
			public int CheckedType;
		}
	}
}
