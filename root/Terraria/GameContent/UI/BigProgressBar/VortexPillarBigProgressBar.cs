using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000397 RID: 919
	public class VortexPillarBigProgressBar : LunarPillarBigProgessBar
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x005804C4 File Offset: 0x0057E6C4
		internal override float GetCurrentShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerVortex;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x005804A8 File Offset: 0x0057E6A8
		internal override float GetMaxShieldValue()
		{
			return (float)NPC.ShieldStrengthTowerMax;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x005804CC File Offset: 0x0057E6CC
		internal override bool IsPlayerInCombatArea()
		{
			return Main.LocalPlayer.ZoneTowerVortex;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x005804BC File Offset: 0x0057E6BC
		public VortexPillarBigProgressBar()
		{
		}
	}
}
