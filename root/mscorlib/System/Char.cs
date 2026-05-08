using System;
using System.Globalization;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public readonly struct Char : IComparable, IComparable<char>, IEquatable<char>, IConvertible
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00019EE4 File Offset: 0x000180E4
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00019EF1 File Offset: 0x000180F1
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00019EFB File Offset: 0x000180FB
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.s_categoryForLatin1[(int)ch];
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00019F04 File Offset: 0x00018104
		public override int GetHashCode()
		{
			return (int)(this | ((int)this << 16));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00019F0E File Offset: 0x0001810E
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019F24 File Offset: 0x00018124
		[NonVersionable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019F2B File Offset: 0x0001812B
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException("Object must be of type Char.");
			}
			return (int)(this - (char)value);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019F4E File Offset: 0x0001814E
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00019F54 File Offset: 0x00018154
		public override string ToString()
		{
			return char.ToString(this);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00019F54 File Offset: 0x00018154
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00019F5D File Offset: 0x0001815D
		public static string ToString(char c)
		{
			return string.CreateFromChar(c);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00019F65 File Offset: 0x00018165
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException("String must be exactly one character long.");
			}
			return s[0];
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019F90 File Offset: 0x00018190
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00019FAF File Offset: 0x000181AF
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00019FD2 File Offset: 0x000181D2
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00019FDB File Offset: 0x000181DB
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A01B File Offset: 0x0001821B
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || c - '\t' <= '\u0004' || c == '\u00a0' || c == '\u0085';
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001A03B File Offset: 0x0001823B
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001A052 File Offset: 0x00018252
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A087 File Offset: 0x00018287
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A0BC File Offset: 0x000182BC
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A0C8 File Offset: 0x000182C8
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001A0E9 File Offset: 0x000182E9
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A0F6 File Offset: 0x000182F6
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001A117 File Offset: 0x00018317
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001A133 File Offset: 0x00018333
		public static char ToUpper(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(c);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001A145 File Offset: 0x00018345
		public static char ToUpperInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToUpper(c);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001A157 File Offset: 0x00018357
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001A173 File Offset: 0x00018373
		public static char ToLower(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(c);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001A185 File Offset: 0x00018385
		public static char ToLowerInvariant(char c)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToLower(c);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001A197 File Offset: 0x00018397
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001A19A File Offset: 0x0001839A
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Boolean"));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001A1B5 File Offset: 0x000183B5
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001A1B9 File Offset: 0x000183B9
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001A1C2 File Offset: 0x000183C2
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001A1CB File Offset: 0x000183CB
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001A1D4 File Offset: 0x000183D4
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001A1DD File Offset: 0x000183DD
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001A1E6 File Offset: 0x000183E6
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001A1EF File Offset: 0x000183EF
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001A1F8 File Offset: 0x000183F8
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001A201 File Offset: 0x00018401
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Single"));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001A21C File Offset: 0x0001841C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Double"));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001A237 File Offset: 0x00018437
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "Decimal"));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001A252 File Offset: 0x00018452
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Char", "DateTime"));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001A26D File Offset: 0x0001846D
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001A27D File Offset: 0x0001847D
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001A29C File Offset: 0x0001849C
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001A2F4 File Offset: 0x000184F4
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001A350 File Offset: 0x00018550
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001A420 File Offset: 0x00018620
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001A48B File Offset: 0x0001868B
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001A496 File Offset: 0x00018696
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001A4D0 File Offset: 0x000186D0
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001A540 File Offset: 0x00018740
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001A597 File Offset: 0x00018797
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001A5A3 File Offset: 0x000187A3
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001A5B4 File Offset: 0x000187B4
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001A5D0 File Offset: 0x000187D0
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001A622 File Offset: 0x00018822
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001A639 File Offset: 0x00018839
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001A669 File Offset: 0x00018869
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001A675 File Offset: 0x00018875
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001A698 File Offset: 0x00018898
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001A75C File Offset: 0x0001895C
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001A7AD File Offset: 0x000189AD
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.GetUnicodeCategory((int)c);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001A815 File Offset: 0x00018A15
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001A81D File Offset: 0x00018A1D
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001A848 File Offset: 0x00018A48
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001A85F File Offset: 0x00018A5F
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001A893 File Offset: 0x00018A93
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001A8AA File Offset: 0x00018AAA
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001A935 File Offset: 0x00018B35
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A960 File Offset: 0x00018B60
		public unsafe static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).");
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			uint num = 0U;
			char* ptr = (char*)(&num);
			*ptr = (char)(utf32 / 1024 + 55296);
			ptr[1] = (char)(utf32 % 1024 + 56320);
			return new string(ptr, 0, 2);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", "A valid high surrogate character is between 0xd800 and 0xdbff, inclusive.");
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", "A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive.");
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001AA3C File Offset: 0x00018C3C
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(SR.Format("Found a low surrogate char without a preceding high surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(SR.Format("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(SR.Format("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", index), "s");
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001AB2B File Offset: 0x00018D2B
		// Note: this type is marked as 'beforefieldinit'.
		static Char()
		{
		}

		// Token: 0x04000EFB RID: 3835
		private readonly char m_value;

		// Token: 0x04000EFC RID: 3836
		public const char MaxValue = '\uffff';

		// Token: 0x04000EFD RID: 3837
		public const char MinValue = '\0';

		// Token: 0x04000EFE RID: 3838
		private static readonly byte[] s_categoryForLatin1 = new byte[]
		{
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 11, 24, 24, 24, 26, 24, 24, 24,
			20, 21, 24, 25, 24, 19, 24, 24, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 24, 24,
			25, 25, 25, 24, 24, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 20, 24, 21, 27, 18, 27, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 20, 25, 21, 25, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			11, 24, 26, 26, 26, 26, 28, 28, 27, 28,
			1, 22, 25, 19, 28, 27, 28, 25, 10, 10,
			27, 1, 28, 24, 27, 10, 1, 23, 10, 10,
			10, 24, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
			0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 25, 1, 1,
			1, 1, 1, 1, 1, 1
		};

		// Token: 0x04000EFF RID: 3839
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x04000F00 RID: 3840
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x04000F01 RID: 3841
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x04000F02 RID: 3842
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x04000F03 RID: 3843
		internal const int LOW_SURROGATE_END = 57343;
	}
}
