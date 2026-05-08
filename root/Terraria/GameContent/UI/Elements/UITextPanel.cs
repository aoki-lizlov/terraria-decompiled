using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000406 RID: 1030
	public class UITextPanel<T> : UIPanel
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x005B22F4 File Offset: 0x005B04F4
		public bool IsLarge
		{
			get
			{
				return this._isLarge;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x005B22FC File Offset: 0x005B04FC
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x005B2304 File Offset: 0x005B0504
		public bool DrawPanel
		{
			get
			{
				return this._drawPanel;
			}
			set
			{
				this._drawPanel = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x005B230D File Offset: 0x005B050D
		// (set) Token: 0x06002F56 RID: 12118 RVA: 0x005B2315 File Offset: 0x005B0515
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x005B231E File Offset: 0x005B051E
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x005B2326 File Offset: 0x005B0526
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x005B234C File Offset: 0x005B054C
		// (set) Token: 0x06002F5A RID: 12122 RVA: 0x005B2354 File Offset: 0x005B0554
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x005B235D File Offset: 0x005B055D
		protected DynamicSpriteFont Font
		{
			get
			{
				if (!this._isLarge)
				{
					return FontAssets.MouseText.Value;
				}
				return FontAssets.DeathText.Value;
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x005B237C File Offset: 0x005B057C
		public UITextPanel(T text, float textScale = 1f, bool large = false)
		{
			this.SetText(text, textScale, large);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x005B23CB File Offset: 0x005B05CB
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x005B23EB File Offset: 0x005B05EB
		public void SetText(T text)
		{
			this.SetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x005B2400 File Offset: 0x005B0600
		public virtual void SetText(T text, float textScale, bool large)
		{
			this._text = text;
			this._textScale = textScale;
			this._isLarge = large;
			this._textSize = new Vector2(this.Font.MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
			this.MinWidth.Set(this._textSize.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(this._textSize.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x005B24B1 File Offset: 0x005B06B1
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				base.DrawSelf(spriteBatch);
			}
			this.DrawText(spriteBatch);
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x005B24CC File Offset: 0x005B06CC
		protected virtual Vector2 TextDrawPosition
		{
			get
			{
				CalculatedStyle innerDimensions = base.GetInnerDimensions();
				Vector2 vector = innerDimensions.Position();
				vector.X += (innerDimensions.Width - this._textSize.X) * this.TextHAlign;
				if (this._isLarge)
				{
					vector.Y -= 10f * this._textScale * this._textScale;
				}
				else
				{
					vector.Y -= 2f * this._textScale;
				}
				return vector;
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x005B254C File Offset: 0x005B074C
		protected void DrawText(SpriteBatch spriteBatch)
		{
			string text = this.Text;
			if (this.HideContents)
			{
				if (this._asterisks == null || this._asterisks.Length != text.Length)
				{
					this._asterisks = new string('*', text.Length);
				}
				text = this._asterisks;
			}
			this.DrawText(spriteBatch, text, this.TextDrawPosition, this._color);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x005B25B4 File Offset: 0x005B07B4
		protected void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
		{
			if (this._isLarge)
			{
				Utils.DrawBorderStringBig(spriteBatch, text, position, color, this._textScale, 0f, 0f, -1);
				return;
			}
			Utils.DrawBorderString(spriteBatch, text, position, color, this._textScale, 0f, 0f, -1);
		}

		// Token: 0x0400565C RID: 22108
		protected T _text;

		// Token: 0x0400565D RID: 22109
		protected float _textScale = 1f;

		// Token: 0x0400565E RID: 22110
		protected Vector2 _textSize = Vector2.Zero;

		// Token: 0x0400565F RID: 22111
		protected bool _isLarge;

		// Token: 0x04005660 RID: 22112
		protected Color _color = Color.White;

		// Token: 0x04005661 RID: 22113
		protected bool _drawPanel = true;

		// Token: 0x04005662 RID: 22114
		public float TextHAlign = 0.5f;

		// Token: 0x04005663 RID: 22115
		public bool HideContents;

		// Token: 0x04005664 RID: 22116
		private string _asterisks;
	}
}
