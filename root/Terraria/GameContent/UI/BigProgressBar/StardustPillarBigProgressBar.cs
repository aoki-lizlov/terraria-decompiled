using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000399 RID: 921
	public class StardustPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x06002A0B RID: 10763 RVA: 0x005804EC File Offset: 0x0057E6EC
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerStardust;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x005804A8 File Offset: 0x0057E6A8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x005804F4 File Offset: 0x0057E6F4
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerStardust;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x005804BC File Offset: 0x0057E6BC
		public StardustPillarBigProgressBar()
		{
		}
	}
}
