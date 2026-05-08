using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000391 RID: 913
	public class MoonLordProgressBar : IBigProgressBar
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x0057FF9C File Offset: 0x0057E19C
		public MoonLordProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0057FFEC File Offset: 0x0057E1EC
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if ((!npc.active || this.IsInBadAI(npc)) && !this.TryFindingAnotherMoonLordPiece(ref info))
			{
				return false;
			}
			int num = 0;
			NPCSpawnParams matchingSpawnParams = npc.GetMatchingSpawnParams();
			this._referenceDummy.SetDefaults(398, matchingSpawnParams);
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(396, matchingSpawnParams);
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(397, matchingSpawnParams);
			num += this._referenceDummy.lifeMax;
			num += this._referenceDummy.lifeMax;
			float num2 = 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIds.Contains(npc2.type) && !this.IsInBadAI(npc2))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00580114 File Offset: 0x0057E314
		private bool IsInBadAI(NPC npc)
		{
			return (npc.type == 398 && (npc.ai[0] == 2f || npc.ai[0] == -1f)) || (npc.type == 398 && npc.localAI[3] == 0f) || (npc.ai[0] == -2f || npc.ai[0] == -3f);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00580190 File Offset: 0x0057E390
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[396];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x005801E4 File Offset: 0x0057E3E4
		private bool TryFindingAnotherMoonLordPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && this.ValidIds.Contains(npc.type) && !this.IsInBadAI(npc))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040052D7 RID: 21207
		private BigProgressBarCache _cache;

		// Token: 0x040052D8 RID: 21208
		private NPC _referenceDummy;

		// Token: 0x040052D9 RID: 21209
		private HashSet<int> ValidIds = new HashSet<int> { 396, 397, 398 };
	}
}
