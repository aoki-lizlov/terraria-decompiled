using System;
using Terraria.GameContent.Drawing;

namespace Terraria.GameContent.Items
{
	// Token: 0x0200046F RID: 1135
	public class WhipTagEffect_Kaleidoscope : WhipTagEffect
	{
		// Token: 0x06003305 RID: 13061 RVA: 0x005F3288 File Offset: 0x005F1488
		public override void OnTaggedHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.RainbowRodHit, new ParticleOrchestraSettings
			{
				PositionInWorld = optionalProjectile.Center
			}, null);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_Kaleidoscope()
		{
		}
	}
}
