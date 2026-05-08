using System;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x02000480 RID: 1152
	public abstract class FishRarityCondition
	{
		// Token: 0x06003351 RID: 13137
		public abstract bool Matches(FishingContext context);

		// Token: 0x06003352 RID: 13138 RVA: 0x0000357B File Offset: 0x0000177B
		protected FishRarityCondition()
		{
		}

		// Token: 0x040058CB RID: 22731
		public float FrequencyOfAppearanceForVisuals;

		// Token: 0x040058CC RID: 22732
		public bool HackedIsAny;
	}
}
