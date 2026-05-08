using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000310 RID: 784
	public class FromOptionsWithoutRepeatsDropRule : IItemDropRule
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x00560C11 File Offset: 0x0055EE11
		// (set) Token: 0x06002704 RID: 9988 RVA: 0x00560C19 File Offset: 0x0055EE19
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

		// Token: 0x06002705 RID: 9989 RVA: 0x00560C22 File Offset: 0x0055EE22
		public FromOptionsWithoutRepeatsDropRule(int dropCount, params int[] options)
		{
			this.dropCount = dropCount;
			this.dropIds = options;
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00560C50 File Offset: 0x0055EE50
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			this._temporaryAvailableItems.Clear();
			this._temporaryAvailableItems.AddRange(this.dropIds);
			int num = 0;
			while (num < this.dropCount && this._temporaryAvailableItems.Count > 0)
			{
				int num2 = info.rng.Next(this._temporaryAvailableItems.Count);
				CommonCode.DropItemFromNPC(info.npc, this._temporaryAvailableItems[num2], 1, false);
				this._temporaryAvailableItems.RemoveAt(num2);
				num++;
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x00560CE8 File Offset: 0x0055EEE8
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			float parentDroprateChance = ratesInfo.parentDroprateChance;
			int num = this.dropIds.Length;
			float num2 = 1f;
			int num3 = 0;
			while (num3 < this.dropCount && num > 0)
			{
				num2 *= (float)(num - 1) / (float)num;
				num3++;
				num--;
			}
			float num4 = (1f - num2) * parentDroprateChance;
			for (int i = 0; i < this.dropIds.Length; i++)
			{
				drops.Add(new DropRateInfo(this.dropIds[i], 1, 1, num4, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050C7 RID: 20679
		public int[] dropIds;

		// Token: 0x040050C8 RID: 20680
		public int dropCount;

		// Token: 0x040050C9 RID: 20681
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;

		// Token: 0x040050CA RID: 20682
		private List<int> _temporaryAvailableItems = new List<int>();
	}
}
