using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000315 RID: 789
	public class ItemDropResolver
	{
		// Token: 0x0600274E RID: 10062 RVA: 0x00566CFC File Offset: 0x00564EFC
		public ItemDropResolver(ItemDropDatabase database)
		{
			this._database = database;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x00566D0C File Offset: 0x00564F0C
		public void TryDropping(DropAttemptInfo info)
		{
			List<IItemDropRule> rulesForNPCID = this._database.GetRulesForNPCID(info.npc.netID, true);
			for (int i = 0; i < rulesForNPCID.Count; i++)
			{
				this.ResolveRule(rulesForNPCID[i], info);
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00566D54 File Offset: 0x00564F54
		private ItemDropAttemptResult ResolveRule(IItemDropRule rule, DropAttemptInfo info)
		{
			if (!rule.CanDrop(info))
			{
				ItemDropAttemptResult itemDropAttemptResult = new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.DoesntFillConditions
				};
				this.ResolveRuleChains(rule, info, itemDropAttemptResult);
				return itemDropAttemptResult;
			}
			INestedItemDropRule nestedItemDropRule = rule as INestedItemDropRule;
			ItemDropAttemptResult itemDropAttemptResult2;
			if (nestedItemDropRule != null)
			{
				itemDropAttemptResult2 = nestedItemDropRule.TryDroppingItem(info, new ItemDropRuleResolveAction(this.ResolveRule));
			}
			else
			{
				itemDropAttemptResult2 = rule.TryDroppingItem(info);
			}
			this.ResolveRuleChains(rule, info, itemDropAttemptResult2);
			return itemDropAttemptResult2;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x00566DB9 File Offset: 0x00564FB9
		private void ResolveRuleChains(IItemDropRule rule, DropAttemptInfo info, ItemDropAttemptResult parentResult)
		{
			this.ResolveRuleChains(ref info, ref parentResult, rule.ChainedRules);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00566DCC File Offset: 0x00564FCC
		private void ResolveRuleChains(ref DropAttemptInfo info, ref ItemDropAttemptResult parentResult, List<IItemDropRuleChainAttempt> ruleChains)
		{
			if (ruleChains == null)
			{
				return;
			}
			for (int i = 0; i < ruleChains.Count; i++)
			{
				IItemDropRuleChainAttempt itemDropRuleChainAttempt = ruleChains[i];
				if (itemDropRuleChainAttempt.CanChainIntoRule(parentResult))
				{
					this.ResolveRule(itemDropRuleChainAttempt.RuleToChain, info);
				}
			}
		}

		// Token: 0x040050D2 RID: 20690
		private ItemDropDatabase _database;
	}
}
