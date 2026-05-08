using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000390 RID: 912
	public class GolemHeadProgressBar : IBigProgressBar
	{
		// Token: 0x060029E8 RID: 10728 RVA: 0x0057FDDD File Offset: 0x0057DFDD
		public GolemHeadProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0057FE14 File Offset: 0x0057E014
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active && !this.TryFindingAnotherGolemPiece(ref info))
			{
				return false;
			}
			int num = 0;
			this._referenceDummy.SetDefaults(245, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(246, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			float num2 = 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIds.Contains(npc2.type))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0057FEFC File Offset: 0x0057E0FC
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[246];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0057FF50 File Offset: 0x0057E150
		private bool TryFindingAnotherGolemPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && this.ValidIds.Contains(npc.type))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040052D4 RID: 21204
		private BigProgressBarCache _cache;

		// Token: 0x040052D5 RID: 21205
		private NPC _referenceDummy;

		// Token: 0x040052D6 RID: 21206
		private HashSet<int> ValidIds = new HashSet<int> { 246, 245 };
	}
}
