using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200039B RID: 923
	public class MartianSaucerBigProgressBar : IBigProgressBar
	{
		// Token: 0x06002A13 RID: 10771 RVA: 0x005806BC File Offset: 0x0057E8BC
		public MartianSaucerBigProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00580720 File Offset: 0x0057E920
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active || npc.type != 395)
			{
				if (!this.TryFindingAnotherMartianSaucerPiece(ref info))
				{
					return false;
				}
				npc = Main.npc[info.npcIndexToAimAt];
			}
			int num = 0;
			if (Main.expertMode)
			{
				this._referenceDummy.SetDefaults(395, npc.GetMatchingSpawnParams());
				num += this._referenceDummy.lifeMax;
			}
			this._referenceDummy.SetDefaults(394, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			this._referenceDummy.SetDefaults(393, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			float num2 = 0f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIdsToScanHp.Contains(npc2.type) && (Main.expertMode || npc2.type != 395))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00580864 File Offset: 0x0057EA64
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[395];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, rectangle);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x005808B8 File Offset: 0x0057EAB8
		private bool TryFindingAnotherMartianSaucerPiece(ref BigProgressBarInfo info)
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

		// Token: 0x040052E5 RID: 21221
		private BigProgressBarCache _cache;

		// Token: 0x040052E6 RID: 21222
		private NPC _referenceDummy;

		// Token: 0x040052E7 RID: 21223
		private HashSet<int> ValidIds = new HashSet<int> { 395 };

		// Token: 0x040052E8 RID: 21224
		private HashSet<int> ValidIdsToScanHp = new HashSet<int> { 395, 393, 394 };
	}
}
