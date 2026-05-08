using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000215 RID: 533
	internal class ReturnGatePlayerRenderer : IPlayerRenderer
	{
		// Token: 0x060021A1 RID: 8609 RVA: 0x00530E50 File Offset: 0x0052F050
		public void DrawPlayers(Camera camera, IEnumerable<Player> players)
		{
			foreach (Player player in players)
			{
				this.DrawReturnGateInWorld(camera, player);
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00530E9C File Offset: 0x0052F09C
		public void DrawPlayerHead(Camera camera, Player drawPlayer, Vector2 position, float alpha = 1f, float scale = 1f, Color borderColor = default(Color))
		{
			this.DrawReturnGateInMap(camera, drawPlayer);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00530EA6 File Offset: 0x0052F0A6
		public void DrawPlayer(Camera camera, Player drawPlayer, Vector2 position, float rotation, Vector2 rotationOrigin, float shadow = 0f, float scale = 1f)
		{
			this.DrawReturnGateInWorld(camera, drawPlayer);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00009E46 File Offset: 0x00008046
		private void DrawReturnGateInMap(Camera camera, Player player)
		{
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00530EB0 File Offset: 0x0052F0B0
		private void DrawReturnGateInWorld(Camera camera, Player player)
		{
			Rectangle empty = Rectangle.Empty;
			if (!PotionOfReturnHelper.TryGetGateHitbox(player, out empty))
			{
				return;
			}
			AHoverInteractionChecker.HoverStatus hoverStatus = AHoverInteractionChecker.HoverStatus.NotSelectable;
			if (player == Main.LocalPlayer)
			{
				this._interactionChecker.AttemptInteraction(player, empty);
			}
			if (Main.SmartInteractPotionOfReturn)
			{
				hoverStatus = AHoverInteractionChecker.HoverStatus.Selected;
			}
			int num = (int)hoverStatus;
			if (player.PotionOfReturnOriginalUsePosition == null)
			{
				return;
			}
			SpriteBatch spriteBatch = camera.SpriteBatch;
			SamplerState sampler = camera.Sampler;
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, DepthStencilState.None, camera.Rasterizer, null, camera.GameViewMatrix.TransformationMatrix);
			float num2 = ((player.whoAmI == Main.myPlayer) ? 1f : 0.1f);
			Vector2 value = player.PotionOfReturnOriginalUsePosition.Value;
			Vector2 vector = new Vector2(0f, -21f);
			Vector2 vector2 = value + vector;
			Vector2 vector3 = empty.Center.ToVector2();
			PotionOfReturnGateHelper potionOfReturnGateHelper = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.ExitPoint, vector2, num2);
			PotionOfReturnGateHelper potionOfReturnGateHelper2 = new PotionOfReturnGateHelper(PotionOfReturnGateHelper.GateType.EntryPoint, vector3, num2);
			if (!Main.gamePaused)
			{
				potionOfReturnGateHelper.Update();
				potionOfReturnGateHelper2.Update();
			}
			this._voidLensData.Clear();
			potionOfReturnGateHelper.DrawToDrawData(this._voidLensData, 0);
			potionOfReturnGateHelper2.DrawToDrawData(this._voidLensData, num);
			foreach (DrawData drawData in this._voidLensData)
			{
				drawData.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00009E46 File Offset: 0x00008046
		public void PrepareDrawForFrame(Player drawPlayer)
		{
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00531028 File Offset: 0x0052F228
		public ReturnGatePlayerRenderer()
		{
		}

		// Token: 0x04004C2D RID: 19501
		private List<DrawData> _voidLensData = new List<DrawData>();

		// Token: 0x04004C2E RID: 19502
		private PotionOfReturnGateInteractionChecker _interactionChecker = new PotionOfReturnGateInteractionChecker();
	}
}
