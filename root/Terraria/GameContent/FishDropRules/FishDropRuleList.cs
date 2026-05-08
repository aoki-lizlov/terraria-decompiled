using System;
using System.Collections.Generic;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047A RID: 1146
	public class FishDropRuleList
	{
		// Token: 0x06003326 RID: 13094 RVA: 0x005F3C30 File Offset: 0x005F1E30
		public int TryGetItemDropType(FishingContext context)
		{
			int num = 0;
			for (int i = 0; i < this._rules.Count; i++)
			{
				if (this._rules[i].Attempt(context, out num))
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x005F3C70 File Offset: 0x005F1E70
		public void GetDisplayableDrops(FishingContext context, List<FishPossibilityEntry> resultTypes)
		{
			for (int i = 0; i < this._rules.Count; i++)
			{
				FishDropRule fishDropRule = this._rules[i];
				if (fishDropRule.MeetsConditions(context, true))
				{
					int num = 0;
					if (fishDropRule.PossibleItems.Length != 0)
					{
						num = context.Random.NextFromList(fishDropRule.PossibleItems);
					}
					resultTypes.Add(new FishPossibilityEntry
					{
						ItemType = num,
						Frequency = fishDropRule.Rarity.FrequencyOfAppearanceForVisuals
					});
					if (fishDropRule.IsStopper)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x005F3CF8 File Offset: 0x005F1EF8
		public void Add(FishDropRule rule)
		{
			this.Validate(rule);
			this._rules.Add(rule);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x005F3D0D File Offset: 0x005F1F0D
		private void Validate(FishDropRule rule)
		{
			if (rule.ChanceDenominator <= 0)
			{
				throw new ArgumentOutOfRangeException("FishDropRule.ChanceDenominator", "Chance Denominator must be positive non-zero number");
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x005F3D28 File Offset: 0x005F1F28
		public FishDropRuleList()
		{
		}

		// Token: 0x04005897 RID: 22679
		private List<FishDropRule> _rules = new List<FishDropRule>();
	}
}
