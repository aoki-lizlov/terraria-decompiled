using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Localization.IME;
using ReLogic.OS;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F9 RID: 1017
	internal class UITextBox : UITextPanel<string>
	{
		// Token: 0x06002EC9 RID: 11977 RVA: 0x005AE49D File Offset: 0x005AC69D
		public UITextBox(string text, float textScale = 1f, bool large = false)
			: base(text, textScale, large)
		{
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x005AE4B7 File Offset: 0x005AC6B7
		public void Write(string text)
		{
			base.SetText(base.Text.Insert(this._cursor, text));
			this._cursor += text.Length;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x005AE4E4 File Offset: 0x005AC6E4
		public override void SetText(string text, float textScale, bool large)
		{
			text = Utils.TrimUserString(text ?? "", this._maxLength);
			base.SetText(text, textScale, large);
			this._cursor = Math.Min(base.Text.Length, this._cursor);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x005AE522 File Offset: 0x005AC722
		public void SetTextMaxLength(int maxLength)
		{
			this._maxLength = maxLength;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x005AE52B File Offset: 0x005AC72B
		public void Backspace()
		{
			if (this._cursor == 0)
			{
				return;
			}
			base.SetText(Utils.TrimLastCharacter(base.Text));
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x005AE547 File Offset: 0x005AC747
		public void CursorLeft()
		{
			if (this._cursor == 0)
			{
				return;
			}
			this._cursor--;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x005AE560 File Offset: 0x005AC760
		public void CursorRight()
		{
			if (this._cursor < base.Text.Length)
			{
				this._cursor++;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x005AE584 File Offset: 0x005AC784
		protected override Vector2 TextDrawPosition
		{
			get
			{
				Vector2 textDrawPosition = base.TextDrawPosition;
				if (this.ShowInputTicker)
				{
					string compositionString = Platform.Get<IImeService>().CompositionString;
					if (!string.IsNullOrEmpty(compositionString))
					{
						textDrawPosition.X -= base.Font.MeasureString(compositionString).X * base.TextScale * this.TextHAlign;
					}
				}
				return textDrawPosition;
			}
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x005AE5E0 File Offset: 0x005AC7E0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.HideSelf)
			{
				return;
			}
			this._cursor = base.Text.Length;
			base.DrawSelf(spriteBatch);
			if (!this.ShowInputTicker)
			{
				return;
			}
			Vector2 textDrawPosition = this.TextDrawPosition;
			string compositionString = Platform.Get<IImeService>().CompositionString;
			if (!string.IsNullOrEmpty(compositionString))
			{
				textDrawPosition.X += base.Font.MeasureString(compositionString).X * base.TextScale;
				base.DrawText(spriteBatch, compositionString, this.TextDrawPosition + new Vector2(base.TextSize.X, 0f), Main.imeCompositionStringColor);
			}
			this._frameCount++;
			if ((this._frameCount %= 40) > 20)
			{
				return;
			}
			textDrawPosition.X += base.Font.MeasureString(base.Text.Substring(0, this._cursor)).X * base.TextScale;
			textDrawPosition.X += 6f - (base.IsLarge ? 8f : 4f) * base.TextScale;
			if (base.IsLarge)
			{
				textDrawPosition.Y += 2f * base.TextScale;
			}
			base.DrawText(spriteBatch, "|", textDrawPosition, base.TextColor);
		}

		// Token: 0x040055F2 RID: 22002
		private int _cursor;

		// Token: 0x040055F3 RID: 22003
		private int _frameCount;

		// Token: 0x040055F4 RID: 22004
		private int _maxLength = 20;

		// Token: 0x040055F5 RID: 22005
		public bool ShowInputTicker = true;

		// Token: 0x040055F6 RID: 22006
		public bool HideSelf;
	}
}
