using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200038C RID: 908
	public class CommonBossBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029DB RID: 10715 RVA: 0x0057F9A4 File Offset: 0x0057DBA4
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
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0057FA10 File Offset: 0x0057DC10
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0000357B File Offset: 0x0000177B
		public CommonBossBigProgressBar()
		{
		}

		// Token: 0x040052CC RID: 21196
		private BigProgressBarCache _cache;

		// Token: 0x040052CD RID: 21197
		private int _headIndex;
	}
}
