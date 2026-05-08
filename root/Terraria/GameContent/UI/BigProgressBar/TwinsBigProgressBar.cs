using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000394 RID: 916
	public class TwinsBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x00580258 File Offset: 0x0057E458
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
			int num = ((npc.type == 126) ? 125 : 126);
			int num2 = npc.lifeMax;
			int num3 = npc.life;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && npc2.type == num)
				{
					num2 += npc2.lifeMax;
					num3 += npc2.life;
					break;
				}
			}
			this._cache.SetLife((float)num3, (float)num2);
			this._headIndex = npc.GetBossHeadTextureIndex();
			return true;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00580318 File Offset: 0x0057E518
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0000357B File Offset: 0x0000177B
		public TwinsBigProgressBar()
		{
		}

		// Token: 0x040052DE RID: 21214
		private BigProgressBarCache _cache;

		// Token: 0x040052DF RID: 21215
		private int _headIndex;
	}
}
