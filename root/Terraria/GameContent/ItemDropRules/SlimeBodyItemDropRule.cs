using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.ID;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200030A RID: 778
	public class SlimeBodyItemDropRule : IItemDropRule
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x0056047C File Offset: 0x0055E67C
		// (set) Token: 0x060026E2 RID: 9954 RVA: 0x00560484 File Offset: 0x0055E684
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

		// Token: 0x060026E3 RID: 9955 RVA: 0x0056048D File Offset: 0x0055E68D
		public SlimeBodyItemDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x005604A0 File Offset: 0x0055E6A0
		public bool CanDrop(DropAttemptInfo info)
		{
			return NPCID.Sets.SlimeCanContainItems[info.npc.type] && info.npc.ai[1] > 0f && info.npc.ai[1] < (float)ItemID.Count;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x005604E0 File Offset: 0x0055E6E0
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			int num = (int)info.npc.ai[1];
			int num2;
			int num3;
			this.GetDropInfo(num, out num2, out num3);
			CommonCode.DropItemFromNPC(info.npc, num, info.rng.Next(num2, num3 + 1), false);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x00560534 File Offset: 0x0055E734
		public void GetDropInfo(int itemId, out int amountDroppedMinimum, out int amountDroppedMaximum)
		{
			amountDroppedMinimum = 1;
			amountDroppedMaximum = 1;
			if (itemId <= 751)
			{
				if (itemId <= 166)
				{
					if (itemId <= 73)
					{
						switch (itemId)
						{
						case 2:
						case 3:
						case 9:
							goto IL_01B7;
						case 4:
						case 5:
						case 6:
						case 7:
						case 10:
							return;
						case 8:
							amountDroppedMinimum = 5;
							amountDroppedMaximum = 10;
							return;
						case 11:
						case 12:
						case 13:
						case 14:
							break;
						default:
							switch (itemId)
							{
							case 71:
								amountDroppedMinimum = 50;
								amountDroppedMaximum = 99;
								return;
							case 72:
								amountDroppedMinimum = 20;
								amountDroppedMaximum = 99;
								return;
							case 73:
								amountDroppedMinimum = 1;
								amountDroppedMaximum = 2;
								return;
							default:
								return;
							}
							break;
						}
					}
					else
					{
						if (itemId == 147)
						{
							goto IL_01C0;
						}
						if (itemId == 150)
						{
							goto IL_01B7;
						}
						if (itemId != 166)
						{
							return;
						}
						amountDroppedMinimum = 2;
						amountDroppedMaximum = 6;
						return;
					}
				}
				else if (itemId <= 366)
				{
					if (itemId != 174)
					{
						if (itemId == 314)
						{
							goto IL_01C0;
						}
						if (itemId - 364 > 2)
						{
							return;
						}
					}
				}
				else
				{
					if (itemId == 593)
					{
						goto IL_01B7;
					}
					if (itemId - 699 > 3)
					{
						if (itemId != 751)
						{
							return;
						}
						goto IL_01B7;
					}
				}
			}
			else if (itemId <= 3081)
			{
				if (itemId <= 1106)
				{
					if (itemId == 965)
					{
						amountDroppedMinimum = 20;
						amountDroppedMaximum = 45;
						return;
					}
					if (itemId == 1103)
					{
						goto IL_01B7;
					}
					if (itemId - 1104 > 2)
					{
						return;
					}
				}
				else
				{
					if (itemId - 1124 <= 1 || itemId == 1345)
					{
						goto IL_01C0;
					}
					if (itemId != 3081)
					{
						return;
					}
					goto IL_01B7;
				}
			}
			else if (itemId <= 3610)
			{
				if (itemId == 3086)
				{
					goto IL_01B7;
				}
				if (itemId != 3347)
				{
					if (itemId - 3609 > 1)
					{
						return;
					}
					goto IL_01B7;
				}
			}
			else
			{
				if (itemId - 3736 <= 2)
				{
					goto IL_01C0;
				}
				if (itemId - 4343 <= 1)
				{
					amountDroppedMinimum = 2;
					amountDroppedMaximum = 5;
					return;
				}
				if (itemId != 5395)
				{
					return;
				}
				goto IL_01B7;
			}
			amountDroppedMinimum = 3;
			amountDroppedMaximum = 13;
			return;
			IL_01B7:
			amountDroppedMinimum = 10;
			amountDroppedMaximum = 25;
			return;
			IL_01C0:
			amountDroppedMinimum = 2;
			amountDroppedMaximum = 5;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00560707 File Offset: 0x0055E907
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x040050B8 RID: 20664
		[CompilerGenerated]
		private List<IItemDropRuleChainAttempt> <ChainedRules>k__BackingField;
	}
}
