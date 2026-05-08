using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.UI.Chat
{
	// Token: 0x0200010B RID: 267
	public class ChatMessageContainer
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x004F7024 File Offset: 0x004F5224
		public void SetContents(string text, Color color, int widthLimitInPixels)
		{
			this.OriginalText = text;
			this._color = color;
			this._widthLimitInPixels = widthLimitInPixels;
			this._parsedText = new List<TextSnippet[]>();
			this._timeLeft = 600;
			this.Refresh();
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x004F7057 File Offset: 0x004F5257
		public void OnWidthLimitChanged()
		{
			if (this._widthLimitInPixels == -1)
			{
				this._prepared = false;
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x004F7069 File Offset: 0x004F5269
		public void Update()
		{
			if (this._timeLeft > 0)
			{
				this._timeLeft--;
			}
			this.Refresh();
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x004F7088 File Offset: 0x004F5288
		public TextSnippet[] GetSnippetWithInversedIndex(int snippetIndex)
		{
			int num = this._parsedText.Count - 1 - snippetIndex;
			return this._parsedText[num];
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x004F70B1 File Offset: 0x004F52B1
		public int LineCount
		{
			get
			{
				return this._parsedText.Count;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x004F70BE File Offset: 0x004F52BE
		public bool CanBeShownWhenChatIsClosed
		{
			get
			{
				return this._timeLeft > 0;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x004F70C9 File Offset: 0x004F52C9
		public bool Prepared
		{
			get
			{
				return this._prepared;
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x004F70D4 File Offset: 0x004F52D4
		private void Refresh()
		{
			if (this._prepared)
			{
				return;
			}
			this._prepared = true;
			int num = this._widthLimitInPixels;
			if (num == -1)
			{
				num = Main.ChatLineWidthLimit;
			}
			List<List<TextSnippet>> list = Utils.WordwrapStringSmart(this.OriginalText, this._color, FontAssets.MouseText.Value, (float)num, 10);
			this._parsedText.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				this._parsedText.Add(list[i].ToArray());
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0000357B File Offset: 0x0000177B
		public ChatMessageContainer()
		{
		}

		// Token: 0x04001501 RID: 5377
		public string OriginalText;

		// Token: 0x04001502 RID: 5378
		private bool _prepared;

		// Token: 0x04001503 RID: 5379
		private List<TextSnippet[]> _parsedText;

		// Token: 0x04001504 RID: 5380
		private Color _color;

		// Token: 0x04001505 RID: 5381
		private int _widthLimitInPixels;

		// Token: 0x04001506 RID: 5382
		private int _timeLeft;
	}
}
