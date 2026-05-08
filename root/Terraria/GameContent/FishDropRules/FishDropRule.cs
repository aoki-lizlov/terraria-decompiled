using System;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x02000478 RID: 1144
	public class FishDropRule
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x005F3B50 File Offset: 0x005F1D50
		public bool IsStopper
		{
			get
			{
				return this.PossibleItems.Length == 0 || (this.Rarity.HackedIsAny && this.ChanceDenominator == this.ChanceNumerator);
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x005F3B7C File Offset: 0x005F1D7C
		public bool Attempt(FishingContext context, out int resultItemType)
		{
			resultItemType = 0;
			if (!this.MeetsConditions(context, false))
			{
				return false;
			}
			if (context.Random.Next(this.ChanceDenominator) >= this.ChanceNumerator)
			{
				return false;
			}
			if (!this.Rarity.Matches(context))
			{
				return false;
			}
			if (this.PossibleItems != null && this.PossibleItems.Length != 0)
			{
				resultItemType = context.Random.NextFromList(this.PossibleItems);
			}
			return true;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x005F3BE8 File Offset: 0x005F1DE8
		public bool MeetsConditions(FishingContext context, bool forDisplay)
		{
			AFishingCondition[] conditions = this.Conditions;
			for (int i = 0; i < conditions.Length; i++)
			{
				if (!conditions[i].Matches(context))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x005F3C18 File Offset: 0x005F1E18
		public FishDropRule()
		{
		}

		// Token: 0x04005890 RID: 22672
		public int[] PossibleItems;

		// Token: 0x04005891 RID: 22673
		public int ChanceNumerator = 1;

		// Token: 0x04005892 RID: 22674
		public int ChanceDenominator = 1;

		// Token: 0x04005893 RID: 22675
		public AFishingCondition[] Conditions;

		// Token: 0x04005894 RID: 22676
		public FishRarityCondition Rarity;
	}
}
