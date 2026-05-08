using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ReLogic.Text
{
	// Token: 0x02000014 RID: 20
	public class WrappedTextBuilder
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public WrappedTextBuilder(IFontMetrics font, CultureInfo culture, float maxWidth, float firstLineOffset = 0f)
		{
			this._font = font;
			this._maxWidth = maxWidth;
			this._culture = culture;
			this._workingLineWidth = firstLineOffset;
			this._firstLine = true;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003E14 File Offset: 0x00002014
		private void CommitWorkingLine()
		{
			if (!this._firstLine)
			{
				this._completedText.Append('\n');
			}
			this._workingLineWidth = 0f;
			this._completedText.Append(this._workingLine);
			this._workingLine.Clear();
			this._firstLine = false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E67 File Offset: 0x00002067
		private void FinishPartialLine()
		{
			if (this._workingLineWidth > 0f)
			{
				this.CommitWorkingLine();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003E7C File Offset: 0x0000207C
		private void Append(WrappedTextBuilder.NonBreakingText textToken)
		{
			float num;
			if (this._workingLineWidth == 0f)
			{
				num = textToken.WidthOnNewLine;
			}
			else
			{
				num = this._workingLineWidth + this._font.CharacterSpacing + textToken.Width;
			}
			if (num <= this._maxWidth)
			{
				this._workingLine.Append(textToken.Text);
				this._workingLineWidth = num;
				return;
			}
			this.FinishPartialLine();
			if (textToken.IsWhitespace)
			{
				return;
			}
			if (textToken.WidthOnNewLine <= this._maxWidth)
			{
				this._workingLine.Append(textToken.Text);
				this._workingLineWidth = textToken.WidthOnNewLine;
				return;
			}
			this.AppendWithHardBreaks(textToken.Text);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F24 File Offset: 0x00002124
		private void AppendWithHardBreaks(string text)
		{
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				GlyphMetrics characterMetrics = this._font.GetCharacterMetrics(c);
				float num;
				if (this._workingLine.IsEmpty())
				{
					num = characterMetrics.KernedWidthOnNewLine;
				}
				else
				{
					num = this._workingLineWidth + this._font.CharacterSpacing + characterMetrics.KernedWidth;
				}
				if (num <= this._maxWidth)
				{
					this._workingLine.Append(c);
					this._workingLineWidth = num;
				}
				else if (this._workingLine.Length > 1 && !StringReaderWrapExtension.BreaksBetweenMostGlyphs(this._culture))
				{
					this._workingLineWidth += this._font.CharacterSpacing + this._font.GetCharacterMetrics('-').KernedWidth;
					while (this._workingLine.Length > 1 && this._workingLineWidth > this._maxWidth)
					{
						this._workingLineWidth -= this._font.CharacterSpacing + this._font.GetCharacterMetrics(this._workingLine.get_Chars(this._workingLine.Length - 1)).KernedWidth;
						this._workingLine.Remove(this._workingLine.Length - 1, 1);
						i--;
					}
					this._workingLine.Append('-');
					this.FinishPartialLine();
					i--;
				}
				else
				{
					this.FinishPartialLine();
					this._workingLine.Append(c);
					this._workingLineWidth = characterMetrics.KernedWidthOnNewLine;
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000040BC File Offset: 0x000022BC
		public string Build(string text)
		{
			StringReader stringReader = new StringReader(text);
			this._completedText.EnsureCapacity(this._completedText.Capacity + text.Length);
			while (stringReader.Peek() > 0)
			{
				if ((ushort)stringReader.Peek() == 10)
				{
					stringReader.Read();
					this.CommitWorkingLine();
				}
				else
				{
					string text2 = stringReader.ReadUntilBreakable(this._culture);
					this.Append(new WrappedTextBuilder.NonBreakingText(this._font, text2));
				}
			}
			this.CommitWorkingLine();
			return this._completedText.ToString();
		}

		// Token: 0x04000026 RID: 38
		private readonly IFontMetrics _font;

		// Token: 0x04000027 RID: 39
		private readonly CultureInfo _culture;

		// Token: 0x04000028 RID: 40
		private readonly float _maxWidth;

		// Token: 0x04000029 RID: 41
		private readonly StringBuilder _completedText = new StringBuilder();

		// Token: 0x0400002A RID: 42
		private readonly StringBuilder _workingLine = new StringBuilder();

		// Token: 0x0400002B RID: 43
		private float _workingLineWidth;

		// Token: 0x0400002C RID: 44
		private bool _firstLine;

		// Token: 0x020000B3 RID: 179
		private struct NonBreakingText
		{
			// Token: 0x0600041A RID: 1050 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
			public NonBreakingText(IFontMetrics font, string text)
			{
				this.Text = text;
				this.IsWhitespace = true;
				float num = 0f;
				float num2 = 0f;
				this._font = font;
				for (int i = 0; i < text.Length; i++)
				{
					GlyphMetrics characterMetrics = font.GetCharacterMetrics(text.get_Chars(i));
					if (i == 0)
					{
						num2 = characterMetrics.KernedWidthOnNewLine - characterMetrics.KernedWidth;
					}
					else
					{
						num += font.CharacterSpacing;
					}
					num += characterMetrics.KernedWidth;
					if (text.get_Chars(i) != ' ')
					{
						this.IsWhitespace = false;
					}
				}
				this.Width = num;
				this.WidthOnNewLine = num + num2;
			}

			// Token: 0x04000554 RID: 1364
			public readonly string Text;

			// Token: 0x04000555 RID: 1365
			public readonly float Width;

			// Token: 0x04000556 RID: 1366
			public readonly float WidthOnNewLine;

			// Token: 0x04000557 RID: 1367
			public readonly bool IsWhitespace;

			// Token: 0x04000558 RID: 1368
			private IFontMetrics _font;
		}
	}
}
