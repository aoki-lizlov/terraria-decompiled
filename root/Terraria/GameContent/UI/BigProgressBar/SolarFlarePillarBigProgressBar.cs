using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000396 RID: 918
	public class SolarFlarePillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x005804A0 File Offset: 0x0057E6A0
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerSolar;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x005804A8 File Offset: 0x0057E6A8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x005804B0 File Offset: 0x0057E6B0
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerSolar;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x005804BC File Offset: 0x0057E6BC
		public SolarFlarePillarBigProgressBar()
		{
		}
	}
}
