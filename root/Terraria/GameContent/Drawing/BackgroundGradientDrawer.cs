using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000439 RID: 1081
	public class BackgroundGradientDrawer
	{
		// Token: 0x060030BD RID: 12477 RVA: 0x005BC777 File Offset: 0x005BA977
		public BackgroundGradientDrawer(Color gradientColor, GetBackgroundDrawWeightMethod weightGetter, BackgroundArrayGetterMethod textureGetter, params int[] textureIndexesToCheck)
		{
			this._color = gradientColor;
			this._weightGetter = weightGetter;
			this._textureGetter = textureGetter;
			this._textureIndexesToCheck = textureIndexesToCheck;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x005BC79C File Offset: 0x005BA99C
		public void Draw()
		{
			if (!Main.BackgroundEnabled)
			{
				return;
			}
			float num = this._weightGetter();
			if (num <= 0f)
			{
				return;
			}
			if (!this.ShouldDrawForTextures())
			{
				return;
			}
			if (!Main.ShouldDrawSurfaceBackground())
			{
				return;
			}
			if (BackgroundGradientDrawer._sunflareGradientDitherTexture == null)
			{
				BackgroundGradientDrawer._sunflareGradientDitherTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/colorgradientdither", 1);
			}
			SpriteBatch spriteBatch = Main.spriteBatch;
			Color color = new Color(this._color.ToVector3() * Main.ColorOfSurfaceBackgroundsBase.ToVector3());
			spriteBatch.Draw(BackgroundGradientDrawer._sunflareGradientDitherTexture.Value, BackgroundGradientDrawer.GetGradientRect(), null, color * num, 0f, Vector2.Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x005BC850 File Offset: 0x005BAA50
		private static Rectangle GetGradientRect()
		{
			int num = 400;
			int num2 = Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)) - num;
			return new Rectangle(0, num2, Main.screenWidth, Main.screenHeight + num);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x005BC8B0 File Offset: 0x005BAAB0
		private bool ShouldDrawForTextures()
		{
			IEnumerable<int> enumerable = this._textureGetter();
			foreach (int num in this._textureIndexesToCheck)
			{
				foreach (int num2 in enumerable)
				{
					if (num == num2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040056FE RID: 22270
		private Color _color;

		// Token: 0x040056FF RID: 22271
		private GetBackgroundDrawWeightMethod _weightGetter;

		// Token: 0x04005700 RID: 22272
		private BackgroundArrayGetterMethod _textureGetter;

		// Token: 0x04005701 RID: 22273
		private int[] _textureIndexesToCheck;

		// Token: 0x04005702 RID: 22274
		private static Asset<Texture2D> _sunflareGradientDitherTexture;
	}
}
