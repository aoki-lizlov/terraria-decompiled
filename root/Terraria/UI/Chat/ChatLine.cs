using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
	// Token: 0x0200010A RID: 266
	public class ChatLine
	{
		// Token: 0x06001A87 RID: 6791 RVA: 0x004F6F74 File Offset: 0x004F5174
		public void UpdateTimeLeft()
		{
			if (this.showTime > 0)
			{
				this.showTime--;
			}
			if (this.needsParsing)
			{
				this.needsParsing = false;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x004F6F9C File Offset: 0x004F519C
		public void Copy(ChatLine other)
		{
			this.needsParsing = other.needsParsing;
			this.parsingPixelLimit = other.parsingPixelLimit;
			this.originalText = other.originalText;
			this.parsedText = other.parsedText;
			this.showTime = other.showTime;
			this.color = other.color;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x004F6FF1 File Offset: 0x004F51F1
		public void FlagAsNeedsReprocessing()
		{
			this.needsParsing = true;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x004F6FFA File Offset: 0x004F51FA
		public ChatLine()
		{
		}

		// Token: 0x040014FB RID: 5371
		public Color color = Color.White;

		// Token: 0x040014FC RID: 5372
		public int showTime;

		// Token: 0x040014FD RID: 5373
		public string originalText = "";

		// Token: 0x040014FE RID: 5374
		public TextSnippet[] parsedText = new TextSnippet[0];

		// Token: 0x040014FF RID: 5375
		private int? parsingPixelLimit;

		// Token: 0x04001500 RID: 5376
		private bool needsParsing;
	}
}
