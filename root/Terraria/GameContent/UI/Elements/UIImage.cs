using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003FE RID: 1022
	public class UIImage : UIElement
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x005B0B09 File Offset: 0x005AED09
		public Asset<Texture2D> Texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x005B0B11 File Offset: 0x005AED11
		protected UIImage()
		{
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x005B0B48 File Offset: 0x005AED48
		public UIImage(Asset<Texture2D> texture)
		{
			this.SetImage(texture);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x005B0B86 File Offset: 0x005AED86
		public UIImage(Texture2D nonReloadingTexture)
		{
			this.SetImage(nonReloadingTexture);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x005B0BC4 File Offset: 0x005AEDC4
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this._nonReloadingTexture = null;
			if (this.AllowResizingDimensions)
			{
				this.Width.Set((float)this._texture.Width(), 0f);
				this.Height.Set((float)this._texture.Height(), 0f);
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x005B0C20 File Offset: 0x005AEE20
		public void SetImage(Texture2D nonReloadingTexture)
		{
			this._texture = null;
			this._nonReloadingTexture = nonReloadingTexture;
			if (this.AllowResizingDimensions)
			{
				this.Width.Set((float)this._nonReloadingTexture.Width, 0f);
				this.Height.Set((float)this._nonReloadingTexture.Height, 0f);
			}
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x005B0C7C File Offset: 0x005AEE7C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D texture2D = null;
			if (this._texture != null)
			{
				texture2D = this._texture.Value;
			}
			if (this._nonReloadingTexture != null)
			{
				texture2D = this._nonReloadingTexture;
			}
			if (this.ScaleToFit)
			{
				spriteBatch.Draw(texture2D, dimensions.ToRectangle(), this.Frame, this.Color);
				return;
			}
			Vector2 vector = texture2D.Size();
			Vector2 vector2 = new Vector2(dimensions.Width, dimensions.Height);
			if (this.UseTextureSizeForOrigin)
			{
				vector2 = vector;
			}
			Vector2 vector3 = dimensions.Position() + vector2 * (1f - this.ImageScale) / 2f + vector2 * this.NormalizedOrigin;
			if (this.RemoveFloatingPointsFromDrawPosition)
			{
				vector3 = vector3.Floor();
			}
			spriteBatch.Draw(texture2D, vector3, this.Frame, this.Color, this.Rotation, vector * this.NormalizedOrigin, this.ImageScale, SpriteEffects.None, 0f);
		}

		// Token: 0x04005622 RID: 22050
		private Asset<Texture2D> _texture;

		// Token: 0x04005623 RID: 22051
		public float ImageScale = 1f;

		// Token: 0x04005624 RID: 22052
		public float Rotation;

		// Token: 0x04005625 RID: 22053
		public bool ScaleToFit;

		// Token: 0x04005626 RID: 22054
		public bool AllowResizingDimensions = true;

		// Token: 0x04005627 RID: 22055
		public Color Color = Color.White;

		// Token: 0x04005628 RID: 22056
		public Vector2 NormalizedOrigin = Vector2.Zero;

		// Token: 0x04005629 RID: 22057
		public Rectangle? Frame;

		// Token: 0x0400562A RID: 22058
		public bool RemoveFloatingPointsFromDrawPosition;

		// Token: 0x0400562B RID: 22059
		public bool UseTextureSizeForOrigin = true;

		// Token: 0x0400562C RID: 22060
		private Texture2D _nonReloadingTexture;
	}
}
