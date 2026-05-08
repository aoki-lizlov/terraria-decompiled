using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000395 RID: 917
	public abstract class LunarPillarBigProgessBar : IBigProgressBar
	{
		// Token: 0x060029F9 RID: 10745 RVA: 0x00580364 File Offset: 0x0057E564
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active)
			{
				return false;
			}
			int bossHeadTextureIndex = npc.GetBossHeadTextureIndex();
			if (bossHeadTextureIndex == -1)
			{
				return false;
			}
			if (!this.IsPlayerInCombatArea())
			{
				return false;
			}
			if (npc.ai[2] == 1f)
			{
				return false;
			}
			Utils.Clamp<float>((float)npc.life / (float)npc.lifeMax, 0f, 1f);
			float num = (float)((int)MathHelper.Clamp(this.GetCurrentShieldValue(), 0f, this.GetMaxShieldValue())) / this.GetMaxShieldValue();
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._cache.SetShield(this.GetCurrentShieldValue(), this.GetMaxShieldValue());
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00580440 File Offset: 0x0057E640
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle, this._cache.ShieldCurrent, this._cache.ShieldMax);
		}

		// Token: 0x060029FB RID: 10747
		internal abstract float GetCurrentShieldValue();

		// Token: 0x060029FC RID: 10748
		internal abstract float GetMaxShieldValue();

		// Token: 0x060029FD RID: 10749
		internal abstract bool IsPlayerInCombatArea();

		// Token: 0x060029FE RID: 10750 RVA: 0x0000357B File Offset: 0x0000177B
		protected LunarPillarBigProgessBar()
		{
		}

		// Token: 0x040052E0 RID: 21216
		private BigProgressBarCache _cache;

		// Token: 0x040052E1 RID: 21217
		private int _headIndex;
	}
}
