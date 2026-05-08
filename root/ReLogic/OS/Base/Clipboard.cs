using System;
using System.Text;

namespace ReLogic.OS.Base
{
	// Token: 0x02000077 RID: 119
	internal abstract class Clipboard : IClipboard
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		public string Value
		{
			get
			{
				return Clipboard.SanitizeClipboardText(this.GetClipboard(), false);
			}
			set
			{
				this.SetClipboard(value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public string MultiLineValue
		{
			get
			{
				return Clipboard.SanitizeClipboardText(this.GetClipboard(), true);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A908 File Offset: 0x00008B08
		private static string SanitizeClipboardText(string clipboardText, bool allowNewLine)
		{
			StringBuilder stringBuilder = new StringBuilder(clipboardText.Length);
			for (int i = 0; i < clipboardText.Length; i++)
			{
				if ((clipboardText.get_Chars(i) >= ' ' && clipboardText.get_Chars(i) != '\u007f') || (allowNewLine && clipboardText.get_Chars(i) == '\n'))
				{
					stringBuilder.Append(clipboardText.get_Chars(i));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600028F RID: 655
		protected abstract string GetClipboard();

		// Token: 0x06000290 RID: 656
		protected abstract void SetClipboard(string text);

		// Token: 0x06000291 RID: 657 RVA: 0x0000448A File Offset: 0x0000268A
		protected Clipboard()
		{
		}
	}
}
