using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x0200024F RID: 591
	public class HairstyleUnlocksHelper
	{
		// Token: 0x06002328 RID: 9000 RVA: 0x0053CC4C File Offset: 0x0053AE4C
		public void UpdateUnlocks()
		{
			if (!this.ListWarrantsRemake())
			{
				return;
			}
			this.RebuildList();
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0053CC60 File Offset: 0x0053AE60
		private bool ListWarrantsRemake()
		{
			bool flag = NPC.downedMartians && !Main.gameMenu;
			bool flag2 = NPC.downedMoonlord && !Main.gameMenu;
			bool flag3 = NPC.downedPlantBoss && !Main.gameMenu;
			bool flag4 = Main.hairWindow && !Main.gameMenu;
			bool gameMenu = Main.gameMenu;
			bool flag5 = false;
			if (this._defeatedMartians != flag || this._defeatedMoonlord != flag2 || this._defeatedPlantera != flag3 || this._isAtStylist != flag4 || this._isAtCharacterCreation != gameMenu)
			{
				flag5 = true;
			}
			this._defeatedMartians = flag;
			this._defeatedMoonlord = flag2;
			this._defeatedPlantera = flag3;
			this._isAtStylist = flag4;
			this._isAtCharacterCreation = gameMenu;
			return flag5;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0053CD1C File Offset: 0x0053AF1C
		private void RebuildList()
		{
			List<int> availableHairstyles = this.AvailableHairstyles;
			availableHairstyles.Clear();
			if (this._isAtCharacterCreation || this._isAtStylist)
			{
				for (int i = 0; i < 51; i++)
				{
					availableHairstyles.Add(i);
				}
				availableHairstyles.Add(136);
				availableHairstyles.Add(137);
				availableHairstyles.Add(138);
				availableHairstyles.Add(139);
				availableHairstyles.Add(140);
				availableHairstyles.Add(141);
				availableHairstyles.Add(142);
				availableHairstyles.Add(143);
				availableHairstyles.Add(144);
				availableHairstyles.Add(147);
				availableHairstyles.Add(148);
				availableHairstyles.Add(149);
				availableHairstyles.Add(150);
				availableHairstyles.Add(151);
				availableHairstyles.Add(154);
				availableHairstyles.Add(155);
				availableHairstyles.Add(157);
				availableHairstyles.Add(158);
				availableHairstyles.Add(161);
				for (int j = 51; j < 123; j++)
				{
					availableHairstyles.Add(j);
				}
				availableHairstyles.Add(134);
				availableHairstyles.Add(135);
				availableHairstyles.Add(146);
				availableHairstyles.Add(152);
				availableHairstyles.Add(153);
				availableHairstyles.Add(156);
				availableHairstyles.Add(159);
				availableHairstyles.Add(165);
				availableHairstyles.Add(160);
				for (int k = 166; k < 228; k++)
				{
					availableHairstyles.Add(k);
				}
			}
			if (this._isAtStylist)
			{
				if (this._defeatedPlantera)
				{
					availableHairstyles.Add(162);
					availableHairstyles.Add(164);
					availableHairstyles.Add(163);
					availableHairstyles.Add(145);
				}
				if (this._defeatedMartians)
				{
					availableHairstyles.AddRange(new int[] { 132, 131, 130, 129, 128, 127, 126, 125, 124, 123 });
				}
				if (this._defeatedMartians && this._defeatedMoonlord)
				{
					availableHairstyles.Add(133);
				}
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0053CF38 File Offset: 0x0053B138
		public HairstyleUnlocksHelper()
		{
		}

		// Token: 0x04004D4F RID: 19791
		public List<int> AvailableHairstyles = new List<int>();

		// Token: 0x04004D50 RID: 19792
		private bool _defeatedMartians;

		// Token: 0x04004D51 RID: 19793
		private bool _defeatedMoonlord;

		// Token: 0x04004D52 RID: 19794
		private bool _defeatedPlantera;

		// Token: 0x04004D53 RID: 19795
		private bool _isAtStylist;

		// Token: 0x04004D54 RID: 19796
		private bool _isAtCharacterCreation;
	}
}
