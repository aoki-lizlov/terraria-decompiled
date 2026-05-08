using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ReLogic.Text
{
	// Token: 0x02000015 RID: 21
	internal static class StringReaderWrapExtension
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004143 File Offset: 0x00002343
		internal static bool BreaksBetweenMostGlyphs(CultureInfo culture)
		{
			return culture.LCID == StringReaderWrapExtension.SimplifiedChinese.LCID || culture.LCID == StringReaderWrapExtension.TraditionalChinese.LCID;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000416B File Offset: 0x0000236B
		internal static bool IsIgnoredCharacter(char character)
		{
			return character < ' ' && character != '\n';
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000417C File Offset: 0x0000237C
		internal static bool CanBreakBetween(char previousChar, char nextChar, CultureInfo culture)
		{
			if (StringReaderWrapExtension.BreaksBetweenMostGlyphs(culture))
			{
				return (!StringReaderWrapExtension.Numeric[previousChar] || !StringReaderWrapExtension.Numeric[nextChar]) && !StringReaderWrapExtension.InvalidCharactersForLineEnd[previousChar] && !StringReaderWrapExtension.InvalidCharactersForLineStart[nextChar];
			}
			return StringReaderWrapExtension.WordTerminators[previousChar];
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041E5 File Offset: 0x000023E5
		internal static StringReaderWrapExtension.WrapScanMode GetModeForCharacter(char character)
		{
			if (StringReaderWrapExtension.IsIgnoredCharacter(character))
			{
				return StringReaderWrapExtension.WrapScanMode.None;
			}
			if (character == '\n')
			{
				return StringReaderWrapExtension.WrapScanMode.NewLine;
			}
			if (character == ' ' || (character >= '\u2009' && character <= '\u200b'))
			{
				return StringReaderWrapExtension.WrapScanMode.Space;
			}
			return StringReaderWrapExtension.WrapScanMode.Word;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004210 File Offset: 0x00002410
		internal static string ReadUntilBreakable(this StringReader reader, CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char c = (char)reader.Peek();
			StringReaderWrapExtension.WrapScanMode wrapScanMode = StringReaderWrapExtension.WrapScanMode.None;
			while (reader.Peek() > 0)
			{
				if (StringReaderWrapExtension.IsIgnoredCharacter((char)reader.Peek()))
				{
					reader.Read();
				}
				else
				{
					char c2 = c;
					c = (char)reader.Peek();
					StringReaderWrapExtension.WrapScanMode wrapScanMode2 = wrapScanMode;
					wrapScanMode = StringReaderWrapExtension.GetModeForCharacter(c);
					if (!stringBuilder.IsEmpty() && wrapScanMode2 != wrapScanMode)
					{
						return stringBuilder.ToString();
					}
					if (stringBuilder.IsEmpty())
					{
						stringBuilder.Append((char)reader.Read());
					}
					else
					{
						if (StringReaderWrapExtension.CanBreakBetween(c2, c, culture))
						{
							return stringBuilder.ToString();
						}
						stringBuilder.Append((char)reader.Read());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000042B4 File Offset: 0x000024B4
		// Note: this type is marked as 'beforefieldinit'.
		static StringReaderWrapExtension()
		{
		}

		// Token: 0x0400002D RID: 45
		private static readonly StringReaderWrapExtension.CharacterSet InvalidCharactersForLineStart = new StringReaderWrapExtension.CharacterSet("!%),.:;?]}¢°·'\"†‡›℃∶、。〃〆〕〗〞﹚﹜！＂％＇），．：；？！］｝～ \n!),.:;?]}¢·–— '\"• 、。〆〞〕〉》」︰︱︲\ufe33﹐﹑﹒\ufe53﹔﹕﹖﹘﹚﹜！），．：；？︶︸︺︼︾﹀﹂﹗］｜｝､");

		// Token: 0x0400002E RID: 46
		private static readonly StringReaderWrapExtension.CharacterSet InvalidCharactersForLineEnd = new StringReaderWrapExtension.CharacterSet("$(£¥·'\"〈《「『【〔〖〝﹙﹛＄（．［｛￡￥([{£¥'\"‵〈《「『〔〝\ufe34﹙﹛（｛︵︷︹︻︽︿﹁﹃\ufe4f");

		// Token: 0x0400002F RID: 47
		private static readonly StringReaderWrapExtension.CharacterSet WordTerminators = new StringReaderWrapExtension.CharacterSet("！，。、：？");

		// Token: 0x04000030 RID: 48
		private static readonly StringReaderWrapExtension.CharacterSet Numeric = new StringReaderWrapExtension.CharacterSet("0123456789,.");

		// Token: 0x04000031 RID: 49
		private static readonly CultureInfo SimplifiedChinese = new CultureInfo("zh-Hans");

		// Token: 0x04000032 RID: 50
		private static readonly CultureInfo TraditionalChinese = new CultureInfo("zh-Hant");

		// Token: 0x020000B4 RID: 180
		private struct CharacterSet
		{
			// Token: 0x0600041B RID: 1051 RVA: 0x0000E080 File Offset: 0x0000C280
			public CharacterSet(string characters)
			{
				this.arr = new bool[65535];
				for (int i = 0; i < characters.Length; i++)
				{
					char c = characters.get_Chars(i);
					this.arr[(int)c] = true;
				}
			}

			// Token: 0x17000080 RID: 128
			public bool this[char c]
			{
				get
				{
					return this.arr[(int)c];
				}
			}

			// Token: 0x04000559 RID: 1369
			private readonly bool[] arr;
		}

		// Token: 0x020000B5 RID: 181
		internal enum WrapScanMode
		{
			// Token: 0x0400055B RID: 1371
			Space,
			// Token: 0x0400055C RID: 1372
			NewLine,
			// Token: 0x0400055D RID: 1373
			Word,
			// Token: 0x0400055E RID: 1374
			None
		}
	}
}
