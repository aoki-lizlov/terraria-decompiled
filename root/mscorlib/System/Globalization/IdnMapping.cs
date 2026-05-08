using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000A02 RID: 2562
	public sealed class IdnMapping
	{
		// Token: 0x06005F62 RID: 24418 RVA: 0x0014B164 File Offset: 0x00149364
		public IdnMapping()
		{
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06005F63 RID: 24419 RVA: 0x0014B177 File Offset: 0x00149377
		// (set) Token: 0x06005F64 RID: 24420 RVA: 0x0014B17F File Offset: 0x0014937F
		public bool AllowUnassigned
		{
			get
			{
				return this.allow_unassigned;
			}
			set
			{
				this.allow_unassigned = value;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x0014B188 File Offset: 0x00149388
		// (set) Token: 0x06005F66 RID: 24422 RVA: 0x0014B190 File Offset: 0x00149390
		public bool UseStd3AsciiRules
		{
			get
			{
				return this.use_std3;
			}
			set
			{
				this.use_std3 = value;
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x0014B19C File Offset: 0x0014939C
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.allow_unassigned == idnMapping.allow_unassigned && this.use_std3 == idnMapping.use_std3;
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x0014B1D1 File Offset: 0x001493D1
		public override int GetHashCode()
		{
			return (this.allow_unassigned ? 2 : 0) + (this.use_std3 ? 1 : 0);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x0014B1EC File Offset: 0x001493EC
		public string GetAscii(string unicode)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, 0, unicode.Length);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x0014B20A File Offset: 0x0014940A
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x0014B22C File Offset: 0x0014942C
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument unicode string");
			}
			return this.Convert(unicode, index, count, true);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x0014B27C File Offset: 0x0014947C
		private string Convert(string input, int index, int count, bool toAscii)
		{
			string text = input.Substring(index, count);
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] >= '\u0080')
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					break;
				}
			}
			string[] array = text.Split(new char[] { '.', '。', '．', '｡' });
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length != 0 || j + 1 != array.Length)
				{
					if (toAscii)
					{
						array[j] = this.ToAscii(array[j], num);
					}
					else
					{
						array[j] = this.ToUnicode(array[j], num);
					}
				}
				num += array[j].Length;
			}
			return string.Join(".", array);
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x0014B33C File Offset: 0x0014953C
		private string ToAscii(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < ' ' || s[i] == '\u007f')
				{
					throw new ArgumentException(string.Format("Not allowed character was found, at {0}", offset + i));
				}
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (this.use_std3)
			{
				this.VerifyStd3AsciiRules(s, offset);
			}
			int j = 0;
			while (j < s.Length)
			{
				if (s[j] >= '\u0080')
				{
					if (s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("The input string must not start with ACE (xn--), at {0}", offset + j));
					}
					s = this.puny.Encode(s, offset);
					s = "xn--" + s;
					break;
				}
				else
				{
					j++;
				}
			}
			this.VerifyLength(s, offset);
			return s;
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x0014B41E File Offset: 0x0014961E
		private void VerifyLength(string s, int offset)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException(string.Format("A label in the input string resulted in an invalid zero-length string, at {0}", offset));
			}
			if (s.Length > 63)
			{
				throw new ArgumentException(string.Format("A label in the input string exceeded the length in ASCII representation, at {0}", offset));
			}
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x0014B460 File Offset: 0x00149660
		private string NamePrep(string s, int offset)
		{
			s = s.Normalize(NormalizationForm.FormKC);
			this.VerifyProhibitedCharacters(s, offset);
			if (!this.allow_unassigned)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (char.GetUnicodeCategory(s, i) == UnicodeCategory.OtherNotAssigned)
					{
						throw new ArgumentException(string.Format("Use of unassigned Unicode characer is prohibited in this IdnMapping, at {0}", offset + i));
					}
				}
			}
			return s;
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x0014B4BC File Offset: 0x001496BC
		private void VerifyProhibitedCharacters(string s, int offset)
		{
			int i = 0;
			while (i < s.Length)
			{
				switch (char.GetUnicodeCategory(s, i))
				{
				case UnicodeCategory.SpaceSeparator:
					if (s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Format:
					goto IL_006E;
				case UnicodeCategory.Control:
					if (s[i] == '\0' || s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_0111;
				default:
					goto IL_006E;
				}
				IL_0129:
				i++;
				continue;
				IL_0111:
				throw new ArgumentException(string.Format("Not allowed character was in the input string, at {0}", offset + i));
				IL_006E:
				char c = s[i];
				if (('\ufddf' <= c && c <= '\ufdef') || (c & '\uffff') == '\ufffe' || ('\ufff9' <= c && c <= '\ufffd') || ('⿰' <= c && c <= '⿻') || ('\u202a' <= c && c <= '\u202e') || ('\u206a' <= c && c <= '\u206f'))
				{
					goto IL_0111;
				}
				if (c <= '\u200e')
				{
					if (c != '\u0340' && c != '\u0341' && c != '\u200e')
					{
						goto IL_0129;
					}
					goto IL_0111;
				}
				else
				{
					if (c == '\u200f' || c == '\u2028' || c == '\u2029')
					{
						goto IL_0111;
					}
					goto IL_0129;
				}
			}
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x0014B604 File Offset: 0x00149804
		private void VerifyStd3AsciiRules(string s, int offset)
		{
			if (s.Length > 0 && s[0] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at head of a sequence in STD3 mode, found at {0}", offset));
			}
			if (s.Length > 0 && s[s.Length - 1] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at tail of a sequence in STD3 mode, found at {0}", offset + s.Length - 1));
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c != '-' && (c <= '/' || (':' <= c && c <= '@') || ('[' <= c && c <= '`') || ('{' <= c && c <= '\u007f')))
				{
					throw new ArgumentException(string.Format("Not allowed character in STD3 mode, found at {0}", offset + i));
				}
			}
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x0014B6CE File Offset: 0x001498CE
		public string GetUnicode(string ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, 0, ascii.Length);
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x0014B6EC File Offset: 0x001498EC
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x0014B70C File Offset: 0x0014990C
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument ascii string");
			}
			return this.Convert(ascii, index, count, false);
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x0014B75C File Offset: 0x0014995C
		private string ToUnicode(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (!s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			string text = s;
			s = s.Substring(4);
			s = this.puny.Decode(s, offset);
			string text2 = s;
			s = this.ToAscii(s, offset);
			if (string.Compare(text, s, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(string.Format("ToUnicode() failed at verifying the result, at label part from {0}", offset));
			}
			return text2;
		}

		// Token: 0x04003983 RID: 14723
		private bool allow_unassigned;

		// Token: 0x04003984 RID: 14724
		private bool use_std3;

		// Token: 0x04003985 RID: 14725
		private Punycode puny = new Punycode();
	}
}
