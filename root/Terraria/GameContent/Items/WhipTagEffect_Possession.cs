using System;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000470 RID: 1136
	public class WhipTagEffect_Possession : WhipTagEffect
	{
		// Token: 0x06003307 RID: 13063 RVA: 0x005F32C3 File Offset: 0x005F14C3
		public override void OnProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			Projectile.SpawnMoonLordWhipProc(optionalProjectile, npcHit, 20, 0);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_Possession()
		{
		}
	}
}
