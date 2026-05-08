using System;

namespace Terraria.GameContent.Items
{
	// Token: 0x0200046D RID: 1133
	public abstract class UniqueTagEffect
	{
		// Token: 0x060032F4 RID: 13044 RVA: 0x000379E9 File Offset: 0x00035BE9
		public virtual bool CanApplyTagToNPC(int npcType)
		{
			return true;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnRemovedFromPlayer(Player owner)
		{
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnSetToPlayer(Player owner)
		{
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnTagAppliedToNPC(Player owner, NPC npc)
		{
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000379E9 File Offset: 0x00035BE9
		public virtual bool CanRunHitEffects(Player owner, Projectile optionalProjectile, NPC npcHit)
		{
			return true;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void ModifyTaggedHit(Player owner, Projectile optionalProjectile, NPC npcHit, ref int damageDealt, ref bool crit)
		{
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void ModifyProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, ref int damageDealt, ref bool crit)
		{
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnTaggedHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnProcHit(Player owner, Projectile optionalProjectile, NPC npcHit, int calcDamage)
		{
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x0000357B File Offset: 0x0000177B
		protected UniqueTagEffect()
		{
		}

		// Token: 0x0400587A RID: 22650
		public bool NetSync;

		// Token: 0x0400587B RID: 22651
		public bool SyncProcs;

		// Token: 0x0400587C RID: 22652
		public int TagDuration;
	}
}
