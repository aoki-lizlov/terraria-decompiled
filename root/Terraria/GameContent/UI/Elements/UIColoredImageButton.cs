using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003EE RID: 1006
	public class UIColoredImageButton : UIElement
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x005AB9EC File Offset: 0x005A9BEC
		public UIColoredImageButton(Asset<Texture2D> texture, bool isSmall = false)
		{
			this._color = Color.White;
			this._texture = texture;
			if (isSmall)
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanel", 1);
			}
			else
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			}
			this.Width.Set((float)this._backPanelTexture.Width(), 0f);
			this.Height.Set((float)this._backPanelTexture.Height(), 0f);
			this._backPanelHighlightTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			if (isSmall)
			{
				this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanelBorder", 1);
				return;
			}
			this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x005ABAD8 File Offset: 0x005A9CD8
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x005ABB24 File Offset: 0x005A9D24
		public void SetImageWithoutSettingSize(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x005ABB30 File Offset: 0x005A9D30
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			spriteBatch.Draw(this._backPanelTexture.Value, vector, null, Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive), 0f, this._backPanelTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			Color white = Color.White;
			if (this._hovered)
			{
				spriteBatch.Draw(this._backPanelBorderTexture.Value, vector, null, Color.White, 0f, this._backPanelBorderTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			if (this._selected)
			{
				spriteBatch.Draw(this._backPanelHighlightTexture.Value, vector, null, Color.White, 0f, this._backPanelHighlightTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			if (this._middleTexture != null)
			{
				spriteBatch.Draw(this._middleTexture.Value, vector, null, Color.White, 0f, this._middleTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			if (this._texture != null)
			{
				spriteBatch.Draw(this._texture.Value, vector, null, this._color, 0f, this._texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x005ABD07 File Offset: 0x005A9F07
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x005ABD2C File Offset: 0x005A9F2C
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x005ABD5A File Offset: 0x005A9F5A
		public void SetColor(Color color)
		{
			this._color = color;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x005ABD63 File Offset: 0x005A9F63
		public void SetMiddleTexture(Asset<Texture2D> texAsset)
		{
			this._middleTexture = texAsset;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x005ABD6C File Offset: 0x005A9F6C
		public void SetSelected(bool selected)
		{
			this._selected = selected;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x005ABD75 File Offset: 0x005A9F75
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x040055AF RID: 21935
		private Asset<Texture2D> _backPanelTexture;

		// Token: 0x040055B0 RID: 21936
		private Asset<Texture2D> _texture;

		// Token: 0x040055B1 RID: 21937
		private Asset<Texture2D> _middleTexture;

		// Token: 0x040055B2 RID: 21938
		private Asset<Texture2D> _backPanelHighlightTexture;

		// Token: 0x040055B3 RID: 21939
		private Asset<Texture2D> _backPanelBorderTexture;

		// Token: 0x040055B4 RID: 21940
		private Color _color;

		// Token: 0x040055B5 RID: 21941
		private float _visibilityActive = 1f;

		// Token: 0x040055B6 RID: 21942
		private float _visibilityInactive = 0.4f;

		// Token: 0x040055B7 RID: 21943
		private bool _selected;

		// Token: 0x040055B8 RID: 21944
		private bool _hovered;
	}
}
