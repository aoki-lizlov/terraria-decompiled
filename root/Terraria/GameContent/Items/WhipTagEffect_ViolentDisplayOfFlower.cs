using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000471 RID: 1137
	public class WhipTagEffect_ViolentDisplayOfFlower : WhipTagEffect
	{
		// Token: 0x06003309 RID: 13065 RVA: 0x005F32CF File Offset: 0x005F14CF
		public override void OnProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			this.SpawnFlowerExplosionOn(optionalProjectile, npcHit, 40);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x005F32DC File Offset: 0x005F14DC
		private void SpawnFlowerExplosionOn(Projectile projectile, NPC targetNPC, int petalDamage)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 3f;
			int num3 = 0;
			while ((float)num3 < num2)
			{
				float num4 = (float)num3 / num2 * 6.2831855f + num;
				float num5 = (float)((targetNPC.width > targetNPC.height) ? targetNPC.width : targetNPC.height) / 8f;
				Vector2 vector = Vector2.UnitX.RotatedBy((double)num4, default(Vector2)).RotatedByRandom(0.39269909262657166) * num5;
				int num6 = Projectile.NewProjectile(projectile.GetProjectileSource_FromThis(), targetNPC.Center, vector, 1038, petalDamage, 0f, projectile.owner, Main.rand.NextFloat() * -20f, 0f, 0f, null);
				Main.projectile[num6].localNPCImmunity[targetNPC.whoAmI] = 30;
				num3++;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_ViolentDisplayOfFlower()
		{
		}
	}
}
