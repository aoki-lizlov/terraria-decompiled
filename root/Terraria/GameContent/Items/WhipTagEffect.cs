using System;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Items
{
	// Token: 0x0200046E RID: 1134
	public class WhipTagEffect : UniqueTagEffect
	{
		// Token: 0x060032FE RID: 13054 RVA: 0x005F317D File Offset: 0x005F137D
		public WhipTagEffect()
		{
			this.TagDuration = 240;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x005F3190 File Offset: 0x005F1390
		public override bool CanApplyTagToNPC(int npcType)
		{
			NPCDebuffImmunityData npcdebuffImmunityData;
			return !NPCID.Sets.DebuffImmunitySets.TryGetValue(npcType, out npcdebuffImmunityData) || npcdebuffImmunityData == null || !npcdebuffImmunityData.ImmuneToWhips;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x005F31BC File Offset: 0x005F13BC
		public override void OnRemovedFromPlayer(Player player)
		{
			if (player == Main.LocalPlayer)
			{
				player.ClearBuff(this.PlayerBuffId);
			}
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x005F31D2 File Offset: 0x005F13D2
		public override void OnTagAppliedToNPC(Player player, NPC npc)
		{
			if (player == Main.LocalPlayer)
			{
				this.AddTheBuff(player);
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x005F31E3 File Offset: 0x005F13E3
		protected void AddTheBuff(Player player)
		{
			if (this.PlayerBuffAppliedManually)
			{
				return;
			}
			if (this.PlayerBuffId <= 0)
			{
				return;
			}
			player.AddBuff(this.PlayerBuffId, this.PlayerBuffTime, false);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x005F320B File Offset: 0x005F140B
		public override void ModifyTaggedHit(Player owner, Projectile optionalProjectile, NPC npcHit, ref int damageDealt, ref bool crit)
		{
			if (optionalProjectile != null)
			{
				damageDealt += (int)((float)(this.TagDamage + optionalProjectile.bonusTagDamage) * ProjectileID.Sets.SummonTagDamageMultiplier[optionalProjectile.type]);
			}
			if (Main.rand.Next(100) < this.CritChance)
			{
				crit = true;
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x005F324B File Offset: 0x005F144B
		public override bool CanRunHitEffects(Player owner, Projectile optionalProjectile, NPC npcHit)
		{
			return optionalProjectile != null && optionalProjectile.OwnedBySomeone && (optionalProjectile.minion || ProjectileID.Sets.MinionShot[optionalProjectile.type] || optionalProjectile.sentry || ProjectileID.Sets.SentryShot[optionalProjectile.type]);
		}

		// Token: 0x0400587D RID: 22653
		public int PlayerBuffId;

		// Token: 0x0400587E RID: 22654
		public int PlayerBuffTime;

		// Token: 0x0400587F RID: 22655
		public bool PlayerBuffAppliedManually;

		// Token: 0x04005880 RID: 22656
		public int CritChance;

		// Token: 0x04005881 RID: 22657
		public int TagDamage;

		// Token: 0x04005882 RID: 22658
		private const int generalWhipMarkDuration = 240;
	}
}
