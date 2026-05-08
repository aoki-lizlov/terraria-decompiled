using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BE RID: 958
	public struct PlayerStatsSnapshot
	{
		// Token: 0x06002D1D RID: 11549 RVA: 0x005A23B4 File Offset: 0x005A05B4
		public PlayerStatsSnapshot(Player player)
		{
			this.Life = player.statLife;
			this.Mana = player.statMana;
			this.LifeMax = player.statLifeMax2;
			this.ManaMax = player.statManaMax2;
			float num = 20f;
			int num2 = player.statLifeMax / 20;
			int num3 = (player.statLifeMax - 400) / 5;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 0)
			{
				num2 = player.statLifeMax / (20 + num3 / 4);
				num = (float)player.statLifeMax / 20f;
			}
			int num4 = player.statLifeMax2 - player.statLifeMax;
			if (num2 > 0)
			{
				num += (float)(num4 / num2);
			}
			this.LifeFruitCount = num3;
			this.LifePerSegment = num;
			this.ManaPerSegment = 20f;
		}

		// Token: 0x0400548B RID: 21643
		public int Life;

		// Token: 0x0400548C RID: 21644
		public int LifeMax;

		// Token: 0x0400548D RID: 21645
		public int LifeFruitCount;

		// Token: 0x0400548E RID: 21646
		public float LifePerSegment;

		// Token: 0x0400548F RID: 21647
		public int Mana;

		// Token: 0x04005490 RID: 21648
		public int ManaMax;

		// Token: 0x04005491 RID: 21649
		public float ManaPerSegment;
	}
}
