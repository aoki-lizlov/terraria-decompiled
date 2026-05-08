using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Drawing;
using Terraria.ID;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000473 RID: 1139
	public class WhipTagEffect_DarkHarvest : WhipTagEffect
	{
		// Token: 0x0600330F RID: 13071 RVA: 0x005F34D7 File Offset: 0x005F16D7
		public override void OnTaggedHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			this.SpawnBlackLightning(optionalProjectile, npcHit);
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x005F34E4 File Offset: 0x005F16E4
		private void SpawnBlackLightning(Projectile projectile, NPC npcHit)
		{
			int num = (int)((float)this.TagDamage * ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type]);
			int num2 = Projectile.NewProjectile(projectile.GetProjectileSource_FromThis(), npcHit.Center, Vector2.Zero, 916, num, 0f, projectile.owner, 0f, 0f, 0f, null);
			Main.projectile[num2].localNPCImmunity[npcHit.whoAmI] = -1;
			WhipTagEffect_DarkHarvest.EmitBlackLightningParticles(npcHit);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x005F355C File Offset: 0x005F175C
		private static void EmitBlackLightningParticles(NPC targetNPC)
		{
			ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.BlackLightningHit, new ParticleOrchestraSettings
			{
				PositionInWorld = targetNPC.Center
			}, null);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_DarkHarvest()
		{
		}
	}
}
