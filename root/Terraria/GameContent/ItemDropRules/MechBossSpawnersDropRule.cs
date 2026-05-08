using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000308 RID: 776
	public class MechBossSpawnersDropRule : IItemDropRule
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00560211 File Offset: 0x0055E411
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00560219 File Offset: 0x0055E419
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

		// Token: 0x060026D7 RID: 9943 RVA: 0x00560222 File Offset: 0x0055E422
		public MechBossSpawnersDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00560240 File Offset: 0x0055E440
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.value > 0f && Main.hardMode && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3) && !info.IsInSimulation;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0056027C File Offset: 0x0055E47C
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			if (!NPC.downedMechBoss1 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 556, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss2 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 544, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			if (!NPC.downedMechBoss3 && info.player.RollLuck(2500) == 0)
			{
				CommonCode.DropItemFromNPC(info.npc, 557, 1, false);
				return new ItemDropAttemptResult
				{
					State = ItemDropAttemptResultState.Success
				};
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.FailedRandomRoll
			};
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x00560354 File Offset: 0x0055E554
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			ratesInfo.AddCondition(this.dummyCondition);
			float num = 0.0004f;
			float num2 = num * ratesInfo.parentDroprateChance;
			drops.Add(new DropRateInfo(556, 1, 1, num2, ratesInfo.conditions));
			drops.Add(new DropRateInfo(544, 1, 1, num2, ratesInfo.conditions));
			drops.Add(new DropRateInfo(557, 1, 1, num2, ratesInfo.conditions));
			Chains.ReportDroprates(this.ChainedRules, num, drops, ratesInfo);
		}

		// Token: 0x040050B5 RID: 20661
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;

		// Token: 0x040050B6 RID: 20662
		public Conditions.MechanicalBossesDummyCondition dummyCondition = new Conditions.MechanicalBossesDummyCondition();
	}
}
