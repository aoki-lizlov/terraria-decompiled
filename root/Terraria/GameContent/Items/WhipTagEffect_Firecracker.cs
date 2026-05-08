using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Items
{
	// Token: 0x02000474 RID: 1140
	public class WhipTagEffect_Firecracker : WhipTagEffect
	{
		// Token: 0x06003313 RID: 13075 RVA: 0x005F358F File Offset: 0x005F178F
		public override void ModifyProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, ref int damageDealt, ref bool crit)
		{
			base.ModifyProcHit(owner, optionalProjectile, npcHit, ref damageDealt, ref crit);
			damageDealt += (int)((float)damageDealt * WhipTagEffect_Firecracker.ProcDamageMultiplier);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x005F35B0 File Offset: 0x005F17B0
		public override void OnProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
			WhipTagEffect_Firecracker.CreateExplosion(optionalProjectile, npcHit, (int)((float)calcDamage * WhipTagEffect_Firecracker.ProcDamageMultiplier));
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x005F35C4 File Offset: 0x005F17C4
		private static void CreateExplosion(Projectile projectile, NPC npcHit, int procDamage)
		{
			int num = Projectile.NewProjectile(projectile.GetProjectileSource_FromThis(), npcHit.Center, Vector2.Zero, 918, procDamage, 0f, projectile.owner, 0f, 0f, 0f, null);
			Main.projectile[num].localNPCImmunity[npcHit.whoAmI] = -1;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x005F32BB File Offset: 0x005F14BB
		public WhipTagEffect_Firecracker()
		{
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x005F361D File Offset: 0x005F181D
		// Note: this type is marked as 'beforefieldinit'.
		static WhipTagEffect_Firecracker()
		{
		}

		// Token: 0x04005883 RID: 22659
		private static float ProcDamageMultiplier = 1.75f;
	}
}
