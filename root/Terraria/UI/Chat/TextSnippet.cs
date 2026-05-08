using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI.Chat
{
	// Token: 0x0200010E RID: 270
	public class TextSnippet
	{
		// Token: 0x06001AAB RID: 6827 RVA: 0x004F7BA8 File Offset: 0x004F5DA8
		public TextSnippet(string text = "")
		{
			this.Text = text;
			this.TextOriginal = text;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x004F7BC9 File Offset: 0x004F5DC9
		public TextSnippet(string text, Color color)
		{
			this.Text = text;
			this.TextOriginal = text;
			this.Color = color;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnHover()
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnClick()
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x004F7BF1 File Offset: 0x004F5DF1
		public virtual Color GetVisibleColor()
		{
			return ChatManager.WaveColor(this.Color);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x004F7BFE File Offset: 0x004F5DFE
		public virtual bool UniqueDraw(bool justCheckingSize, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
		{
			size = Vector2.Zero;
			return false;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x004F7C0C File Offset: 0x004F5E0C
		public virtual TextSnippet CopyMorph(string newText)
		{
			TextSnippet textSnippet = (TextSnippet)base.MemberwiseClone();
			textSnippet.Text = newText;
			return textSnippet;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x004F7C20 File Offset: 0x004F5E20
		public override string ToString()
		{
			return "Text: " + this.Text + " | OriginalText: " + this.TextOriginal;
		}

		// Token: 0x0400150B RID: 5387
		public string Text;

		// Token: 0x0400150C RID: 5388
		public string TextOriginal;

		// Token: 0x0400150D RID: 5389
		public Color Color = Color.White;

		// Token: 0x0400150E RID: 5390
		public bool CheckForHover;

		// Token: 0x0400150F RID: 5391
		public bool DeleteWhole;
	}
}
