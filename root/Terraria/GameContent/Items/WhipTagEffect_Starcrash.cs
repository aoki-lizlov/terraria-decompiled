using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000472 RID: 1138
	public class WhipTagEffect_Starcrash : WhipTagEffect
	{
		// Token: 0x0600330C RID: 13068 RVA: 0x005F33CA File Offset: 0x005F15CA
		public override void OnProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			this.SpawnMeteorWhipMeteorOn(optionalProjectile, npcHit, calcDamage);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x005F33D8 File Offset: 0x005F15D8
		private void SpawnMeteorWhipMeteorOn(Projectile projectile, NPC targetNPC, int calcDamage)
		{
			int num = 200;
			int num2 = 600;
			int num3 = (int)((float)calcDamage * 1.33f);
			Vector2 vector = new Vector2((float)(-(float)num + Main.rand.Next(num * 2)), (float)(-(float)num2));
			Vector2 vector2 = targetNPC.Center + vector;
			Vector2 vector3 = vector.SafeNormalize(Vector2.Zero) * -12f;
			int num4 = 8;
			int num5 = 35;
			vector2 = targetNPC.Center + new Vector2(0f, (float)(-(float)num4 * num5)).RotatedBy((double)(Main.rand.NextFloatDirection() * 6.2831855f * 0.125f), default(Vector2));
			vector3 = targetNPC.DirectionFrom(vector2) * (float)num4;
			Projectile.NewProjectile(projectile.GetProjectileSource_FromThis(), vector2, vector3, 1037, num3, projectile.knockBack, projectile.owner, (float)Main.rand.Next(3), targetNPC.position.Y, 0f, null);
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_Starcrash()
		{
		}
	}
}
