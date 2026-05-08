using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003DD RID: 989
	public class UIBestiaryInfoLine<T> : UIElement, IManuallyOrderedUIElement
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x005A7B8A File Offset: 0x005A5D8A
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x005A7B92 File Offset: 0x005A5D92
		public int OrderInUIList
		{
			[CompilerGenerated]
			get
			{
				return this.<OrderInUIList>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OrderInUIList>k__BackingField = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x005A7B9B File Offset: 0x005A5D9B
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x005A7BA3 File Offset: 0x005A5DA3
		public float TextScale
		{
			get
			{
				return this._textScale;
			}
			set
			{
				this._textScale = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x005A7BAC File Offset: 0x005A5DAC
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x005A7BB4 File Offset: 0x005A5DB4
		public string Text
		{
			get
			{
				if (this._text != null)
				{
					return this._text.ToString();
				}
				return "";
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x005A7BDA File Offset: 0x005A5DDA
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x005A7BE2 File Offset: 0x005A5DE2
		public Color TextColor
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x005A7BEB File Offset: 0x005A5DEB
		public UIBestiaryInfoLine(T text, float textScale = 1f)
		{
			this.SetText(text, textScale);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x005A7C1C File Offset: 0x005A5E1C
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale);
			base.Recalculate();
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x005A7C36 File Offset: 0x005A5E36
		public void SetText(T text)
		{
			this.SetText(text, this._textScale);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x005A7C48 File Offset: 0x005A5E48
		public virtual void SetText(T text, float textScale)
		{
			Vector2 vector = new Vector2(FontAssets.MouseText.Value.MeasureString(text.ToString()).X, 16f) * textScale;
			this._text = text;
			this._textScale = textScale;
			this._textSize = vector;
			this.MinWidth.Set(vector.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(vector.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x005A7CE4 File Offset: 0x005A5EE4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 vector = innerDimensions.Position();
			vector.Y -= 2f * this._textScale;
			vector.X += (innerDimensions.Width - this._textSize.X) * 0.5f;
			Utils.DrawBorderString(spriteBatch, this.Text, vector, this._color, this._textScale, 0f, 0f, -1);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x005A7D60 File Offset: 0x005A5F60
		public override int CompareTo(object obj)
		{
			IManuallyOrderedUIElement manuallyOrderedUIElement = obj as IManuallyOrderedUIElement;
			if (manuallyOrderedUIElement != null)
			{
				return this.OrderInUIList.CompareTo(manuallyOrderedUIElement.OrderInUIList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x0400551B RID: 21787
		private T _text;

		// Token: 0x0400551C RID: 21788
		private float _textScale = 1f;

		// Token: 0x0400551D RID: 21789
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x0400551E RID: 21790
		private Color _color = Color.White;

		// Token: 0x0400551F RID: 21791
		[CompilerGenerated]
		private int <OrderInUIList>k__BackingField;
	}
}
