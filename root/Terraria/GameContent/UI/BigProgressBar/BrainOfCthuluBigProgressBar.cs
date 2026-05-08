using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200038F RID: 911
	public class BrainOfCthuluBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029E5 RID: 10725 RVA: 0x0057FC9F File Offset: 0x0057DE9F
		public BrainOfCthuluBigProgressBar()
		{
			this._creeperForReference = new NPC();
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0057FCB4 File Offset: 0x0057DEB4
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
			int brainOfCthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
			this._creeperForReference.SetDefaults(267, npc.GetMatchingSpawnParams());
			int num = this._creeperForReference.lifeMax * brainOfCthuluCreepersCount;
			float num2 = 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && npc2.type == this._creeperForReference.type)
				{
					num2 += (float)npc2.life;
				}
			}
			float num3 = (float)npc.life + num2;
			int num4 = npc.lifeMax + num;
			this._cache.SetLife(num3, (float)num4);
			return true;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0057FD8C File Offset: 0x0057DF8C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[266];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x040052D2 RID: 21202
		private BigProgressBarCache _cache;

		// Token: 0x040052D3 RID: 21203
		private NPC _creeperForReference;
	}
}
