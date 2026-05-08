using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200038D RID: 909
	public class DeerclopsBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029DE RID: 10718 RVA: 0x0057FA5C File Offset: 0x0057DC5C
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
			if (!NPC.IsDeerclopsHostile())
			{
				return false;
			}
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0057FAD0 File Offset: 0x0057DCD0
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0000357B File Offset: 0x0000177B
		public DeerclopsBigProgressBar()
		{
		}

		// Token: 0x040052CE RID: 21198
		private BigProgressBarCache _cache;

		// Token: 0x040052CF RID: 21199
		private int _headIndex;
	}
}
