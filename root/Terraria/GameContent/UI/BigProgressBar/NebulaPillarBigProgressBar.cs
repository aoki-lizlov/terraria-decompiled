using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000398 RID: 920
	public class NebulaPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x06002A07 RID: 10759 RVA: 0x005804D8 File Offset: 0x0057E6D8
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerNebula;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x005804A8 File Offset: 0x0057E6A8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x005804E0 File Offset: 0x0057E6E0
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerNebula;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x005804BC File Offset: 0x0057E6BC
		public NebulaPillarBigProgressBar()
		{
		}
	}
}
