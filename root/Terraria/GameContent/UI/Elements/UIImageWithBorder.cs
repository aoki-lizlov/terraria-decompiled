using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003C9 RID: 969
	public class UIImageWithBorder : UIImage
	{
		// Token: 0x06002D6C RID: 11628 RVA: 0x005A393F File Offset: 0x005A1B3F
		public UIImageWithBorder(Asset<Texture2D> texture, Asset<Texture2D> borderTexture)
			: base(texture)
		{
			this.SetBorder(borderTexture);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x005A394F File Offset: 0x005A1B4F
		public UIImageWithBorder(Texture2D nonReloadingTexture, Texture2D nonReloadingBorderTexture)
			: base(nonReloadingTexture)
		{
			this.SetBorder(nonReloadingBorderTexture);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x005A3960 File Offset: 0x005A1B60
		public void SetBorder(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
			this._nonReloadingBorderTexture = null;
			this.Width.Set((float)this._borderTexture.Width(), 0f);
			this.Height.Set((float)this._borderTexture.Height(), 0f);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x005A39B4 File Offset: 0x005A1BB4
		public void SetBorder(Texture2D nonReloadingTexture)
		{
			this._borderTexture = null;
			this._nonReloadingBorderTexture = nonReloadingTexture;
			this.Width.Set((float)this._nonReloadingBorderTexture.Width, 0f);
			this.Height.Set((float)this._nonReloadingBorderTexture.Height, 0f);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x005A3A08 File Offset: 0x005A1C08
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D texture2D = null;
			if (this._borderTexture != null)
			{
				texture2D = this._borderTexture.Value;
			}
			if (this._nonReloadingBorderTexture != null)
			{
				texture2D = this._nonReloadingBorderTexture;
			}
			if (this.ScaleToFit)
			{
				spriteBatch.Draw(texture2D, dimensions.ToRectangle(), this.Color);
				return;
			}
			Vector2 vector = texture2D.Size();
			Vector2 vector2 = dimensions.Position() + vector * (1f - this.ImageScale) / 2f + vector * this.NormalizedOrigin;
			if (this.RemoveFloatingPointsFromDrawPosition)
			{
				vector2 = vector2.Floor();
			}
			spriteBatch.Draw(texture2D, vector2, null, this.Color, this.Rotation, vector * this.NormalizedOrigin, this.ImageScale, SpriteEffects.None, 0f);
		}

		// Token: 0x040054C0 RID: 21696
		private Asset<Texture2D> _borderTexture;

		// Token: 0x040054C1 RID: 21697
		private Texture2D _nonReloadingBorderTexture;
	}
}
