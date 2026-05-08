using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000EC RID: 236
	internal ref struct __DTString
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00026476 File Offset: 0x00024676
		internal int Length
		{
			get
			{
				return this.Value.Length;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00026483 File Offset: 0x00024683
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi, bool checkDigitToken)
		{
			this = new __DTString(str, dtfi);
			this.m_checkDigitToken = checkDigitToken;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00026494 File Offset: 0x00024694
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi)
		{
			this.Index = -1;
			this.Value = str;
			this.m_current = '\0';
			if (dtfi != null)
			{
				this.m_info = dtfi.CompareInfo;
				this.m_checkDigitToken = (dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > DateTimeFormatFlags.None;
				return;
			}
			this.m_info = CultureInfo.CurrentCulture.CompareInfo;
			this.m_checkDigitToken = false;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000264EF File Offset: 0x000246EF
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000264F7 File Offset: 0x000246F7
		internal unsafe bool GetNext()
		{
			this.Index++;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00026530 File Offset: 0x00024730
		internal bool AtEnd()
		{
			return this.Index >= this.Length;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00026543 File Offset: 0x00024743
		internal unsafe bool Advance(int count)
		{
			this.Index += count;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002657C File Offset: 0x0002477C
		internal unsafe void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
		{
			tokenValue = 0;
			if (this.Index >= this.Length)
			{
				tokenType = TokenType.EndOfString;
				return;
			}
			tokenType = TokenType.UnknownToken;
			IL_0019:
			while (!DateTimeParse.IsDigit(this.m_current))
			{
				if (char.IsWhiteSpace(this.m_current))
				{
					for (;;)
					{
						int num = this.Index + 1;
						this.Index = num;
						if (num >= this.Length)
						{
							break;
						}
						this.m_current = (char)(*this.Value[this.Index]);
						if (!char.IsWhiteSpace(this.m_current))
						{
							goto IL_0019;
						}
					}
					tokenType = TokenType.EndOfString;
					return;
				}
				dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
				return;
			}
			tokenValue = (int)(this.m_current - '0');
			int index = this.Index;
			for (;;)
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					break;
				}
				this.m_current = (char)(*this.Value[this.Index]);
				int num2 = (int)(this.m_current - '0');
				if (num2 < 0 || num2 > 9)
				{
					break;
				}
				tokenValue = tokenValue * 10 + num2;
			}
			if (this.Index - index > 8)
			{
				tokenType = TokenType.NumberToken;
				tokenValue = -1;
			}
			else if (this.Index - index < 3)
			{
				tokenType = TokenType.NumberToken;
			}
			else
			{
				tokenType = TokenType.YearNumberToken;
			}
			if (!this.m_checkDigitToken)
			{
				return;
			}
			int index2 = this.Index;
			char current = this.m_current;
			this.Index = index;
			this.m_current = (char)(*this.Value[this.Index]);
			TokenType tokenType2;
			int num3;
			if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType2, out num3, ref this))
			{
				tokenType = tokenType2;
				tokenValue = num3;
				return;
			}
			this.Index = index2;
			this.m_current = current;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00026700 File Offset: 0x00024900
		internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
		{
			indexBeforeSeparator = this.Index;
			charBeforeSeparator = this.m_current;
			if (!this.SkipWhiteSpaceCurrent())
			{
				return TokenType.SEP_End;
			}
			TokenType tokenType;
			if (!DateTimeParse.IsDigit(this.m_current))
			{
				int num;
				if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out tokenType, out num, ref this))
				{
					tokenType = TokenType.SEP_Space;
				}
			}
			else
			{
				tokenType = TokenType.SEP_Space;
			}
			return tokenType;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00026759 File Offset: 0x00024959
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool MatchSpecifiedWord(string target)
		{
			return this.Index + target.Length <= this.Length && this.m_info.Compare(this.Value.Slice(this.Index, target.Length), target, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002679C File Offset: 0x0002499C
		internal unsafe bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
		{
			int num = this.Value.Length - this.Index;
			matchLength = target.Length;
			if (matchLength > num || this.m_info.Compare(this.Value.Slice(this.Index, matchLength), target, CompareOptions.IgnoreCase) != 0)
			{
				int num2 = 0;
				int num3 = this.Index;
				int num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
				if (num4 == -1)
				{
					return false;
				}
				for (;;)
				{
					int num5 = num4 - num2;
					if (num3 >= this.Value.Length - num5)
					{
						break;
					}
					if (num5 == 0)
					{
						matchLength--;
					}
					else
					{
						if (!char.IsWhiteSpace((char)(*this.Value[num3 + num5])))
						{
							return false;
						}
						if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num5), target.AsSpan(num2, num5)) != 0)
						{
							return false;
						}
						num3 = num3 + num5 + 1;
					}
					num2 = num4 + 1;
					while (num3 < this.Value.Length && char.IsWhiteSpace((char)(*this.Value[num3])))
					{
						num3++;
						matchLength++;
					}
					if ((num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) < 0)
					{
						goto Block_8;
					}
				}
				return false;
				Block_8:
				if (num2 < target.Length)
				{
					int num6 = target.Length - num2;
					if (num3 > this.Value.Length - num6)
					{
						return false;
					}
					if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num6), target.AsSpan(num2, num6)) != 0)
					{
						return false;
					}
				}
			}
			if (checkWordBoundary)
			{
				int num7 = this.Index + matchLength;
				if (num7 < this.Value.Length && char.IsLetter((char)(*this.Value[num7])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002693C File Offset: 0x00024B3C
		internal bool Match(string str)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (str.Length > this.Value.Length - this.Index)
			{
				return false;
			}
			if (this.m_info.Compare(this.Value.Slice(this.Index, str.Length), str, CompareOptions.Ordinal) == 0)
			{
				this.Index += str.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000269C4 File Offset: 0x00024BC4
		internal unsafe bool Match(char ch)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (*this.Value[this.Index] == (ushort)ch)
			{
				this.m_current = ch;
				return true;
			}
			this.Index--;
			return false;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00026A1C File Offset: 0x00024C1C
		internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
		{
			int num = -1;
			for (int i = 0; i < words.Length; i++)
			{
				string text = words[i];
				int length = text.Length;
				if (this.MatchSpecifiedWords(text, false, ref length) && length > maxMatchStrLen)
				{
					maxMatchStrLen = length;
					num = i;
				}
			}
			return num;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00026A5C File Offset: 0x00024C5C
		internal unsafe int GetRepeatCount()
		{
			char c = (char)(*this.Value[this.Index]);
			int num = this.Index + 1;
			while (num < this.Length && *this.Value[num] == (ushort)c)
			{
				num++;
			}
			int num2 = num - this.Index;
			this.Index = num - 1;
			return num2;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00026AB8 File Offset: 0x00024CB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool GetNextDigit()
		{
			int num = this.Index + 1;
			this.Index = num;
			return num < this.Length && DateTimeParse.IsDigit((char)(*this.Value[this.Index]));
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00026AF7 File Offset: 0x00024CF7
		internal unsafe char GetChar()
		{
			return (char)(*this.Value[this.Index]);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00026B0B File Offset: 0x00024D0B
		internal unsafe int GetDigit()
		{
			return (int)(*this.Value[this.Index] - 48);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00026B22 File Offset: 0x00024D22
		internal unsafe void SkipWhiteSpaces()
		{
			while (this.Index + 1 < this.Length)
			{
				if (!char.IsWhiteSpace((char)(*this.Value[this.Index + 1])))
				{
					return;
				}
				this.Index++;
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00026B60 File Offset: 0x00024D60
		internal unsafe bool SkipWhiteSpaceCurrent()
		{
			if (this.Index >= this.Length)
			{
				return false;
			}
			if (!char.IsWhiteSpace(this.m_current))
			{
				return true;
			}
			do
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					return false;
				}
				this.m_current = (char)(*this.Value[this.Index]);
			}
			while (char.IsWhiteSpace(this.m_current));
			return true;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00026BD0 File Offset: 0x00024DD0
		internal unsafe void TrimTail()
		{
			int num = this.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*this.Value[num])))
			{
				num--;
			}
			this.Value = this.Value.Slice(0, num + 1);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00026C1C File Offset: 0x00024E1C
		internal unsafe void RemoveTrailingInQuoteSpaces()
		{
			int num = this.Length - 1;
			if (num <= 1)
			{
				return;
			}
			char c = (char)(*this.Value[num]);
			if ((c == '\'' || c == '"') && char.IsWhiteSpace((char)(*this.Value[num - 1])))
			{
				num--;
				while (num >= 1 && char.IsWhiteSpace((char)(*this.Value[num - 1])))
				{
					num--;
				}
				Span<char> span = new char[num + 1];
				*span[num] = c;
				this.Value.Slice(0, num).CopyTo(span);
				this.Value = span;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00026CC4 File Offset: 0x00024EC4
		internal unsafe void RemoveLeadingInQuoteSpaces()
		{
			if (this.Length <= 2)
			{
				return;
			}
			int num = 0;
			char c = (char)(*this.Value[num]);
			if (c != '\'')
			{
				if (c != '"')
				{
					return;
				}
			}
			while (num + 1 < this.Length && char.IsWhiteSpace((char)(*this.Value[num + 1])))
			{
				num++;
			}
			if (num != 0)
			{
				Span<char> span = new char[this.Value.Length - num];
				*span[0] = c;
				this.Value.Slice(num + 1).CopyTo(span.Slice(1));
				this.Value = span;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00026D6C File Offset: 0x00024F6C
		internal unsafe DTSubString GetSubString()
		{
			DTSubString dtsubString = default(DTSubString);
			dtsubString.index = this.Index;
			dtsubString.s = this.Value;
			while (this.Index + dtsubString.length < this.Length)
			{
				char c = (char)(*this.Value[this.Index + dtsubString.length]);
				DTSubStringType dtsubStringType;
				if (c >= '0' && c <= '9')
				{
					dtsubStringType = DTSubStringType.Number;
				}
				else
				{
					dtsubStringType = DTSubStringType.Other;
				}
				if (dtsubString.length == 0)
				{
					dtsubString.type = dtsubStringType;
				}
				else if (dtsubString.type != dtsubStringType)
				{
					break;
				}
				dtsubString.length++;
				if (dtsubStringType != DTSubStringType.Number)
				{
					break;
				}
				if (dtsubString.length > 8)
				{
					dtsubString.type = DTSubStringType.Invalid;
					return dtsubString;
				}
				int num = (int)(c - '0');
				dtsubString.value = dtsubString.value * 10 + num;
			}
			if (dtsubString.length == 0)
			{
				dtsubString.type = DTSubStringType.End;
				return dtsubString;
			}
			return dtsubString;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00026E47 File Offset: 0x00025047
		internal unsafe void ConsumeSubString(DTSubString sub)
		{
			this.Index = sub.index + sub.length;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00026E82 File Offset: 0x00025082
		// Note: this type is marked as 'beforefieldinit'.
		static __DTString()
		{
		}

		// Token: 0x04000FC4 RID: 4036
		internal ReadOnlySpan<char> Value;

		// Token: 0x04000FC5 RID: 4037
		internal int Index;

		// Token: 0x04000FC6 RID: 4038
		internal char m_current;

		// Token: 0x04000FC7 RID: 4039
		private CompareInfo m_info;

		// Token: 0x04000FC8 RID: 4040
		private bool m_checkDigitToken;

		// Token: 0x04000FC9 RID: 4041
		private static readonly char[] WhiteSpaceChecks = new char[] { ' ', '\u00a0' };
	}
}
