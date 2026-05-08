using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
	// Token: 0x02000253 RID: 595
	public class PlayerHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
	{
		// Token: 0x0600233E RID: 9022 RVA: 0x0053D296 File Offset: 0x0053B496
		public void UsePlayer(Player player)
		{
			this._player = player;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0053D2A0 File Offset: 0x0053B4A0
		internal override void DrawTheContent(SpriteBatch spriteBatch)
		{
			if (this._player == null)
			{
				return;
			}
			if (this._player.ShouldNotDraw)
			{
				return;
			}
			this._drawData.Clear();
			this._dust.Clear();
			this._gore.Clear();
			PlayerDrawHeadSet playerDrawHeadSet = default(PlayerDrawHeadSet);
			playerDrawHeadSet.BoringSetup(this._player, this._drawData, this._dust, this._gore, (float)(this.width / 2), (float)(this.height / 2), 1f, 1f);
			PlayerDrawHeadLayers.DrawPlayer_00_BackHelmet(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_01_FaceSkin(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_02_DrawArmorWithFullHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_03_HelmetHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_04_HatsWithFullHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_05_TallHats(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_06_NormalHats(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_07_JustHair(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_08_FaceAcc(ref playerDrawHeadSet);
			PlayerDrawHeadLayers.DrawPlayer_RenderAllLayers(ref playerDrawHeadSet);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0053D36E File Offset: 0x0053B56E
		public PlayerHeadDrawRenderTargetContent()
		{
		}

		// Token: 0x04004D5C RID: 19804
		private Player _player;

		// Token: 0x04004D5D RID: 19805
		private readonly List<DrawData> _drawData = new List<DrawData>();

		// Token: 0x04004D5E RID: 19806
		private readonly List<int> _dust = new List<int>();

		// Token: 0x04004D5F RID: 19807
		private readonly List<int> _gore = new List<int>();
	}
}
