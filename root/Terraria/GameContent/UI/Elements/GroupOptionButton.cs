using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E8 RID: 1000
	public class GroupOptionButton<T> : UIElement, IGroupOptionButton
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x005AA714 File Offset: 0x005A8914
		public T OptionValue
		{
			get
			{
				return this._myOption;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x005AA71C File Offset: 0x005A891C
		public bool IsSelected
		{
			get
			{
				return EqualityComparer<T>.Default.Equals(this._currentOption, this._myOption);
			}
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x005AA734 File Offset: 0x005A8934
		public GroupOptionButton(T option, LocalizedText title, LocalizedText description, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this._borderColor = Color.White;
			this._currentOption = option;
			this._myOption = option;
			this.Description = description;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
			}
			this._color = Colors.InventoryDefaultColor;
			if (title != null)
			{
				UIText uitext = new UIText(title, textSize, false)
				{
					HAlign = titleAlignmentX,
					VAlign = 0.5f,
					Width = StyleDimension.FromPixelsAndPercent(-titleWidthReduction, 1f),
					Top = StyleDimension.FromPixels(0f)
				};
				uitext.TextColor = textColor;
				base.Append(uitext);
				this._title = uitext;
			}
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x005AA8A0 File Offset: 0x005A8AA0
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uitext = new UIText(text, textSize, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(0f)
			};
			uitext.TextColor = color;
			base.Append(uitext);
			this._title = uitext;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x005AA91C File Offset: 0x005A8B1C
		public void SetTextWithoutLocalization(string text, float textSize, Color color, float hAlign, float left)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uitext = new UIText(text, textSize, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(0f),
				IgnoresMouseInteraction = true
			};
			uitext.TextOriginX = hAlign;
			uitext.Left.Pixels = left;
			uitext.TextColor = color;
			base.Append(uitext);
			this._title = uitext;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x005AA9B1 File Offset: 0x005A8BB1
		public void SetCurrentOption(T option)
		{
			this._currentOption = option;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x005AA9BA File Offset: 0x005A8BBA
		public Texture2D Icon
		{
			get
			{
				if (this._iconTexture == null)
				{
					return null;
				}
				return this._iconTexture.Value;
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x005AA9D1 File Offset: 0x005A8BD1
		public void SetIcon(string iconTexturePath)
		{
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
				return;
			}
			this._iconTexture = null;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x005AA9F0 File Offset: 0x005A8BF0
		public void SetIconFrame(Rectangle region)
		{
			this._iconFrame = new Rectangle?(region);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x005AA9FE File Offset: 0x005A8BFE
		// (set) Token: 0x06002E65 RID: 11877 RVA: 0x005AAA06 File Offset: 0x005A8C06
		public float IconScale
		{
			get
			{
				return this._iconScale;
			}
			set
			{
				this._iconScale = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x005AAA0F File Offset: 0x005A8C0F
		// (set) Token: 0x06002E67 RID: 11879 RVA: 0x005AAA17 File Offset: 0x005A8C17
		public Vector2 IconOffset
		{
			get
			{
				return this._iconOffset;
			}
			set
			{
				this._iconOffset = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x005AAA20 File Offset: 0x005A8C20
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x005AAA28 File Offset: 0x005A8C28
		public Color IconColor
		{
			get
			{
				return this._iconColor;
			}
			set
			{
				this._iconColor = value;
			}
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x005AAA34 File Offset: 0x005A8C34
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
			float num = this._opacity;
			bool isSelected = this.IsSelected;
			if (this._UseOverrideColors)
			{
				color = (isSelected ? this._overridePickedColor : this._overrideUnpickedColor);
				num = (isSelected ? this._overrideOpacityPicked : this._overrideOpacityUnpicked);
			}
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, this.FadeFromBlack) * num);
			if (isSelected && this.ShowHighlightWhenSelected)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.Value, (int)dimensions.X + this.InnerHighlightRim, (int)dimensions.Y + this.InnerHighlightRim, (int)dimensions.Width - this.InnerHighlightRim * 2, (int)dimensions.Height - this.InnerHighlightRim * 2, 10, 10, 10, 10, Color.Lerp(color, Color.White, this._whiteLerp) * num);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, this._borderColor);
			}
			if (this._iconTexture != null)
			{
				Color color2 = this.IconColor;
				if (!this._hovered && !isSelected)
				{
					color2 = Color.Lerp(color, color2, this._whiteLerp) * num;
				}
				spriteBatch.Draw(this._iconTexture.Value, new Vector2(dimensions.X + 1f, dimensions.Y + 1f) + this._iconOffset, this._iconFrame, color2, 0f, Vector2.Zero, this._iconScale, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x005A3DC5 File Offset: 0x005A1FC5
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x005AAC4F File Offset: 0x005A8E4F
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x005AAC5F File Offset: 0x005A8E5F
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x005AAC6F File Offset: 0x005A8E6F
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x005AAC7F File Offset: 0x005A8E7F
		public void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked)
		{
			this._UseOverrideColors = true;
			this._overridePickedColor = pickedColor;
			this._overrideUnpickedColor = unpickedColor;
			this._overrideOpacityPicked = opacityPicked;
			this._overrideOpacityUnpicked = opacityNotPicked;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x005AACA5 File Offset: 0x005A8EA5
		public void SetBorderColor(Color color)
		{
			this._borderColor = color;
		}

		// Token: 0x0400556B RID: 21867
		private T _currentOption;

		// Token: 0x0400556C RID: 21868
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x0400556D RID: 21869
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x0400556E RID: 21870
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x0400556F RID: 21871
		private Asset<Texture2D> _iconTexture;

		// Token: 0x04005570 RID: 21872
		private readonly T _myOption;

		// Token: 0x04005571 RID: 21873
		private Color _color;

		// Token: 0x04005572 RID: 21874
		private Color _borderColor;

		// Token: 0x04005573 RID: 21875
		public float FadeFromBlack = 1f;

		// Token: 0x04005574 RID: 21876
		public int InnerHighlightRim = 7;

		// Token: 0x04005575 RID: 21877
		private float _whiteLerp = 0.7f;

		// Token: 0x04005576 RID: 21878
		private float _opacity = 0.7f;

		// Token: 0x04005577 RID: 21879
		private bool _hovered;

		// Token: 0x04005578 RID: 21880
		private bool _soundedHover;

		// Token: 0x04005579 RID: 21881
		public bool ShowHighlightWhenSelected = true;

		// Token: 0x0400557A RID: 21882
		private bool _UseOverrideColors;

		// Token: 0x0400557B RID: 21883
		private Color _overrideUnpickedColor = Color.White;

		// Token: 0x0400557C RID: 21884
		private Color _overridePickedColor = Color.White;

		// Token: 0x0400557D RID: 21885
		private float _overrideOpacityPicked;

		// Token: 0x0400557E RID: 21886
		private float _overrideOpacityUnpicked;

		// Token: 0x0400557F RID: 21887
		public readonly LocalizedText Description;

		// Token: 0x04005580 RID: 21888
		private UIText _title;

		// Token: 0x04005581 RID: 21889
		private float _iconScale = 1f;

		// Token: 0x04005582 RID: 21890
		private Vector2 _iconOffset;

		// Token: 0x04005583 RID: 21891
		private Rectangle? _iconFrame;

		// Token: 0x04005584 RID: 21892
		private Color _iconColor = Color.White;
	}
}
