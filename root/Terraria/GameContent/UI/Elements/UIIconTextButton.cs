using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003CA RID: 970
	public class UIIconTextButton : UIElement
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x005A3AEC File Offset: 0x005A1CEC
		public UIIconTextButton(LocalizedText title, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._hoverColor = Color.White;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
			}
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			if (title != null)
			{
				this.SetText(title, textSize, textColor);
			}
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x005A3BBC File Offset: 0x005A1DBC
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uitext = new UIText(text, textSize, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(0f),
				Left = StyleDimension.FromPixelsAndPercent(10f, 0f),
				IgnoresMouseInteraction = true
			};
			uitext.TextColor = color;
			base.Append(uitext);
			this._title = uitext;
			if (this._iconTexture != null)
			{
				this.Width.Set(this._title.GetDimensions().Width + (float)this._iconTexture.Width() + 26f, 0f);
				this.Height.Set(Math.Max(this._title.GetDimensions().Height, (float)this._iconTexture.Height()) + 16f, 0f);
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x005A3CB0 File Offset: 0x005A1EB0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._hovered)
			{
				if (!this._soundedHover)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._soundedHover = true;
			}
			else
			{
				this._soundedHover = false;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			Color color = this._color;
			float opacity = this._opacity;
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, this.FadeFromBlack) * opacity);
			if (this._iconTexture != null)
			{
				Color color2 = Color.Lerp(color, Color.White, this._whiteLerp) * opacity;
				spriteBatch.Draw(this._iconTexture.Value, new Vector2(dimensions.X + dimensions.Width - (float)this._iconTexture.Width() - 5f, dimensions.Center().Y - (float)(this._iconTexture.Height() / 2)), color2);
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x005A3DC5 File Offset: 0x005A1FC5
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x005A3DE3 File Offset: 0x005A1FE3
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColor(Color.Lerp(Colors.InventoryDefaultColor, Color.White, this._whiteLerp), 0.7f);
			this._hovered = true;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x005A3E13 File Offset: 0x005A2013
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			this._hovered = false;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x005A3E43 File Offset: 0x005A2043
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x005A3E53 File Offset: 0x005A2053
		public void SetHoverColor(Color color)
		{
			this._hoverColor = color;
		}

		// Token: 0x040054C2 RID: 21698
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040054C3 RID: 21699
		private readonly Asset<Texture2D> _hoveredTexture;

		// Token: 0x040054C4 RID: 21700
		private readonly Asset<Texture2D> _iconTexture;

		// Token: 0x040054C5 RID: 21701
		private Color _color;

		// Token: 0x040054C6 RID: 21702
		private Color _hoverColor;

		// Token: 0x040054C7 RID: 21703
		public float FadeFromBlack = 1f;

		// Token: 0x040054C8 RID: 21704
		private float _whiteLerp = 0.7f;

		// Token: 0x040054C9 RID: 21705
		private float _opacity = 0.7f;

		// Token: 0x040054CA RID: 21706
		private bool _hovered;

		// Token: 0x040054CB RID: 21707
		private bool _soundedHover;

		// Token: 0x040054CC RID: 21708
		private UIText _title;
	}
}
