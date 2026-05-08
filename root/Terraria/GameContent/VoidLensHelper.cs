using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000272 RID: 626
	public struct VoidLensHelper
	{
		// Token: 0x06002422 RID: 9250 RVA: 0x0054AA5F File Offset: 0x00548C5F
		public VoidLensHelper(Projectile proj)
		{
			this._position = proj.Center;
			this._opacity = proj.Opacity;
			this._frameNumber = proj.frame;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0054AA88 File Offset: 0x00548C88
		public VoidLensHelper(Vector2 worldPosition, float opacity)
		{
			worldPosition.Y -= 2f;
			this._position = worldPosition;
			this._opacity = opacity;
			this._frameNumber = (int)(((float)Main.tileFrameCounter[491] + this._position.X + this._position.Y) % 40f) / 5;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0054AAE6 File Offset: 0x00548CE6
		public void Update()
		{
			Lighting.AddLight(this._position, 0.4f, 0.2f, 0.9f);
			this.SpawnVoidLensDust();
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0054AB08 File Offset: 0x00548D08
		public void SpawnVoidLensDust()
		{
			if (Main.rand.Next(3) == 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
					vector *= new Vector2(0.5f, 1f);
					Dust dust = Dust.NewDustDirect(this._position - vector * 30f, 0, 0, Utils.SelectRandom<int>(Main.rand, new int[] { 86, 88 }), 0f, 0f, 0, default(Color), 1f);
					dust.noGravity = true;
					dust.noLightEmittance = true;
					dust.position = this._position - vector.SafeNormalize(Vector2.Zero) * (float)Main.rand.Next(10, 21);
					dust.velocity = vector.RotatedBy(1.5707963705062866, default(Vector2)) * 2f;
					dust.scale = 0.5f + Main.rand.NextFloat();
					dust.fadeIn = 0.5f;
					dust.customData = this;
					dust.position += dust.velocity * 10f;
					dust.velocity *= -1f;
					return;
				}
				Vector2 vector2 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
				vector2 *= new Vector2(0.5f, 1f);
				Dust dust2 = Dust.NewDustDirect(this._position - vector2 * 30f, 0, 0, Utils.SelectRandom<int>(Main.rand, new int[] { 86, 88 }), 0f, 0f, 0, default(Color), 1f);
				dust2.noGravity = true;
				dust2.noLightEmittance = true;
				dust2.position = this._position - vector2.SafeNormalize(Vector2.Zero) * (float)Main.rand.Next(5, 10);
				dust2.velocity = vector2.RotatedBy(-1.5707963705062866, default(Vector2)) * 3f;
				dust2.scale = 0.5f + Main.rand.NextFloat();
				dust2.fadeIn = 0.5f;
				dust2.customData = this;
				dust2.position += dust2.velocity * 10f;
				dust2.velocity *= -1f;
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0054ADDC File Offset: 0x00548FDC
		public void DrawToDrawData(List<DrawData> drawDataList, int selectionMode)
		{
			Main.instance.LoadProjectile(734);
			Asset<Texture2D> asset = TextureAssets.Projectile[734];
			Rectangle rectangle = asset.Frame(1, 8, 0, this._frameNumber, 0, 0);
			Color color = Lighting.GetColor(this._position.ToTileCoordinates());
			color = Color.Lerp(color, Color.White, 0.5f);
			color *= this._opacity;
			DrawData drawData = new DrawData(asset.Value, this._position - Main.screenPosition, new Rectangle?(rectangle), color, 0f, rectangle.Size() / 2f, 1f, SpriteEffects.None, 0f);
			drawDataList.Add(drawData);
			for (float num = 0f; num < 1f; num += 0.34f)
			{
				DrawData drawData2 = drawData;
				drawData2.color = new Color(127, 50, 127, 0) * this._opacity;
				drawData2.scale *= 1.1f;
				float x = (Main.GlobalTimeWrappedHourly / 5f * 6.2831855f).ToRotationVector2().X;
				drawData2.color *= x * 0.1f + 0.3f;
				drawData2.position += ((Main.GlobalTimeWrappedHourly / 5f + num) * 6.2831855f).ToRotationVector2() * (x * 1f + 2f);
				drawDataList.Add(drawData2);
			}
			if (selectionMode != 0)
			{
				int num2 = (int)((color.R + color.G + color.B) / 3);
				if (num2 > 10)
				{
					Color selectionGlowColor = Colors.GetSelectionGlowColor(selectionMode == 2, num2);
					drawData = new DrawData(TextureAssets.Extra[93].Value, this._position - Main.screenPosition, new Rectangle?(rectangle), selectionGlowColor, 0f, rectangle.Size() / 2f, 1f, SpriteEffects.None, 0f);
					drawDataList.Add(drawData);
				}
			}
		}

		// Token: 0x04004DDB RID: 19931
		private readonly Vector2 _position;

		// Token: 0x04004DDC RID: 19932
		private readonly float _opacity;

		// Token: 0x04004DDD RID: 19933
		private readonly int _frameNumber;
	}
}
